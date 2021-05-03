using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Tetris
{
    public partial class Tetris : Form
    {

        public static string player;
        int minRec;           
        StreamReader sr;
        string folder = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "TetrisTop.txt"); // Ennätyslistan tiedosto samassa kansiossa kun peli
        List<string> lista = new List<string>();
        ShapeMove shape;
        int size;
        int linesRemoved;
        int score;
        bool pause = false;
        int interval;
        int[,] grid = new int[16, 9];                       // Aluen koko

        public Tetris()
        {
            InitializeComponent();
            GetRec();    
            KeyDown += new KeyEventHandler(keyfunc);       // Lisätän näppäden eventit
            timer1.Tick += new EventHandler(Update);     
            Action();
        }

        private void keyfunc(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)                  // Näppäimet
            {
                case Keys.Space:                // Tauko välilyönillä
                    if (!pause)
                    {
                        timer1.Stop();
                        pause = true;
                        lbPause.Visible = true;
                    }
                    else
                    {
                        timer1.Start();
                        pause = false;
                        lbPause.Visible = false;
                    }
                    break;
                case Keys.Up:                   // Käännös(↑)
                    if (!pause)
                    {
                        if (!IsIntersects())
                        {
                            ResetArea();
                            shape.RotateShape();
                            Merge();
                            Invalidate();
                        }
                    }
                    break;
                case Keys.Down:                 // Tiputus(↓)
                    if (!pause)
                        timer1.Interval = 30;
                    break;
                case Keys.Right:                // Siirto oikealle(→)
                    if (!pause)
                    {
                        if (!CollideHor(1))
                        {
                            ResetArea();
                            shape.MoveRight();
                            Merge();
                            Invalidate();
                        }
                    }
                    break;
                case Keys.Left:                 // Siirto vasemmalle(←)
                    if (!pause)
                    {
                        if (!CollideHor(-1))
                        {
                            ResetArea();
                            shape.MoveLeft();
                            Merge();
                            Invalidate();
                        }
                    }
                    break;
                case Keys.Escape:               // Pelin sammutus(ESC)
                    {
                        Application.Exit();
                    }
                    break;
                case Keys.F2:                   // Uudelleen aloitus(F2)
                    {
                        if (pause)
                        {
                            lbPause.Visible = false;
                            pause = false;
                        }
                        ResetArea();
                        Restart();              
                        Action();
                    }
                    break;
                case Keys.F10:                  // Ennätyslistan avaus(F10)
                    {
                        timer1.Stop();
                        pause = true;
                        lbPause.Visible = true;
                        Records.folder = folder;
                        Records rec_window = new Records();
                        rec_window.StartPosition = FormStartPosition.CenterParent;
                        rec_window.ShowDialog();
                    }
                    break;
                case Keys.F1:                  // Pelin sääntöjen avaus(F1)
                    {
                        timer1.Stop();
                        pause = true;
                        lbPause.Visible = true;
                        Rules form2 = new Rules();
                        form2.StartPosition = FormStartPosition.CenterParent;
                        form2.ShowDialog();
                    }
                    break;
            }
        }

        public void Action()
        {
            shape = new ShapeMove(3,0);
            size = 20;                  // Neliön koko
            interval = 300;             // Pelin nopeus
            score = 0;                  // Ennätyksen tyhjennys
            linesRemoved = 0;
            lbScore.Text = "Score: " + score;
            lbLines.Text = "Lines: " + linesRemoved;
            timer1.Interval = interval;
            timer1.Start();
        }

        public void Restart()                  // Aluen tyhjennys
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (grid[i, j] != 0)        
                        grid[i, j] = grid[i - 1, j];
                }
            }
        }
        private void Update(object sender, EventArgs e)
        {
            ResetArea();
            if (!Collide())             // Jos muodon alhalla on tilaa, se siirty alaspäin
            {
                shape.MoveDown();
            }
            else                        // Jos ei ole, laitetaan uusi muoto
            {
                Merge();
                SliceMap();
                timer1.Interval = interval;
                shape.ResetShape(3, 0);

                if (Collide())          // Jos muodot menee yli yläreina, peli loppu
                {
                    for (int i = 0; i < 16; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            grid[i, j] = 0;
                        }
                    }
                    timer1.Stop();
                    if(score > minRec)  // Jos pisteet on enemmän kun pienin ennätys
                    {
                        Champions.SalutLocationX = Location.X;     
                        Champions.SalutLocationY = Location.Y;
                        Champions form2 = new Champions();  
                        form2.StartPosition = FormStartPosition.CenterParent;
                        if (form2.ShowDialog() == DialogResult.OK)  // Jos käyttäjä halua pelata uudestaan
                        {
                            Focus();
                            SaveRec();                              // Tallennetaan ennätys
                            GetRec();                               // Päivitetään ennätyslista
                            Action();                               // Aloita peli uudestaan
                        }
                        else if (form2.DialogResult == DialogResult.No)
                        {
                            Focus();
                            SaveRec();                      // Jos käyttäjä ei halua jatkaa, peli tallentaa ennätykset
                            Application.Exit();             // ja lopettaa pelin
                        }
                        else                                // Jos paina ESC
                            Application.Exit();
                    }
                    else if(score <= minRec)    // Jos ennätystä ei tullut, tulee Game Over ikkuna
                    {
                        GameOver form2 = new GameOver();
                        form2.StartPosition = FormStartPosition.CenterParent;
                        if (form2.ShowDialog() == DialogResult.OK)
                        {                                           // Jos käyttäjä halua pelata uudestaan
                            ResetArea();
                            Action();
                        }
                    }
                }
            }
            Merge();
            Invalidate();
        }
        public void ShowNextShape(Graphics e)       // Näytetään mikä muoto tulee seuraavaksi
        {
            for (int i = 0; i < shape.sizeNextMatrix; i++)
            {
                for (int j = 0; j < shape.sizeNextMatrix; j++)
                {
                    if (shape.nextMatrix[i, j] == 1)
                    {
                        e.FillRectangle(Brushes.Blue, new Rectangle(260 + j * size, 190 + i * size, size, size));
                    }
                    if (shape.nextMatrix[i, j] == 2)
                    {
                        e.FillRectangle(Brushes.Red, new Rectangle(270 + j * size, 200 + i * size, size, size));
                    }
                    if (shape.nextMatrix[i, j] == 3)
                    {
                        e.FillRectangle(Brushes.Yellow, new Rectangle(270 + j * size, 200 + i * size, size, size));
                    }
                    if (shape.nextMatrix[i, j] == 4)
                    {
                        e.FillRectangle(Brushes.Violet, new Rectangle(270 + j * size, 200 + i * size, size, size));
                    }
                    if (shape.nextMatrix[i, j] == 5)
                    {
                        e.FillRectangle(Brushes.Purple, new Rectangle(280 + j * size, 200 + i * size, size, size));
                    }
                    if (shape.nextMatrix[i, j] == 6)
                    {
                        e.FillRectangle(Brushes.Aqua, new Rectangle(270 + j * size, 200 + i * size, size, size));
                    }
                    if (shape.nextMatrix[i, j] == 7)
                    {
                        e.FillRectangle(Brushes.Green, new Rectangle(290 + j * size, 215 + i * size, size, size));
                    }
                    if (shape.nextMatrix[i, j] == 8)
                    {
                        e.FillRectangle(Brushes.White, new Rectangle(270 + j * size, 200 + i * size, size, size));
                    }
                }
            }
        }

        public void SliceMap()      // Täysien rivien poisto
        {
            int curRemovedLines = 0;
            for (int i = 0; i < 16; i++)
            {
                int count = 0;
                for (int j = 0; j < 9; j++)
                {
                    if (grid[i, j] != 0)
                        count++;
                }
                if (count == 9)
                {
                    curRemovedLines++;
                    for (int k = i; k >= 1; k--)
                    {
                        for (int o = 0; o < 9; o++)
                        {
                            grid[k, o] = grid[k - 1, o];
                        }
                    }
                }
            }
            for (int i = 0; i < curRemovedLines; i++)   // Pisteiden lisäys
            {
                score += 10;
                if (curRemovedLines > 1)                // Jos poistettu enemmän kun 1 rivi
                {
                    score += 10 * (i + 1);
                }
            }
            linesRemoved += curRemovedLines;

            if (linesRemoved % 10 == 0)     // Pelin nopeutaminen, kun 10 riviä poistettu
            {
                if (interval > 100)          
                    interval -= 3;
            }

            lbScore.Text = "Score: " + score;
            lbLines.Text = "Lines: " + linesRemoved;
        }

        public void Merge()         // Aluen ja muotojen synkronointi
        {
            for(int i = shape.y; i < shape.y + shape.sizeMatrix; i++)
            {
                for (int j = shape.x; j < shape.x + shape.sizeMatrix; j++)
                {
                    if (shape.matrix[i - shape.y, j - shape.x] != 0)
                    grid[i, j] = shape.matrix[i - shape.y, j - shape.x];
                }
            }
        }
        public bool IsIntersects()      // Tarkistus että muoto käänöksestä ei mee reunan ulkopuolelle
        {
            for (int i = shape.y; i < shape.y + shape.sizeMatrix; i++)
            {
                for (int j = shape.x; j < shape.x + shape.sizeMatrix; j++)
                {
                    if (j >= 1 && j <= 7)
                    {
                        if (grid[i, j] != 0 && shape.matrix[i - shape.y, j - shape.x] == 0)
                            return true;
                    }
                }
            }
            return false;
        }

        public void ResetArea() // Aluen päivitys, tyhjentää ruutut missä ei ole muotoja
        {
            for (int i = shape.y; i < shape.y + shape.sizeMatrix; i++)
            {
                for (int j = shape.x; j < shape.x + shape.sizeMatrix; j++)
                {
                    if (i >= 0 && j >= 0 && i < 16 && j < 9)
                    {
                        if (shape.matrix[i - shape.y, j - shape.x] != 0)
                        {
                            grid[i, j] = 0;
                        }
                    }
                }
            }
        }

        public bool Collide()       // Muoto pysäyty kun koske aluen alareunan
        {
            for (int i = shape.y + shape.sizeMatrix - 1; i >= shape.y; i--)
            {
                for (int j = shape.x; j < shape.x + shape.sizeMatrix; j++)
                {
                    if (shape.matrix[i - shape.y, j - shape.x] != 0)
                    {
                        if (i + 1 == 16)
                            return true;
                        if (grid[i + 1, j] != 0)
                            return true;
                    }
                }
            }
            return false;
        }

        public bool CollideHor(int dir)     // Estää muotoen siirto sivujen ulkopuolelle
        {
            for (int i = shape.y; i < shape.y + shape.sizeMatrix; i++)
            {
                for (int j = shape.x; j < shape.x + shape.sizeMatrix; j++)
                {
                    if (shape.matrix[i - shape.y, j - shape.x] != 0)
                    {
                        if (j + 1 * dir > 8 || j + 1 * dir < 0)
                            return true;

                        if (grid[i, j + 1 * dir] != 0)
                        {
                            if (j - shape.x + 1 * dir >= shape.sizeMatrix || j - shape.x + 1 * dir < 0)
                            {
                                return true;
                            }
                            if (shape.matrix[i - shape.y, j - shape.x + 1 * dir] == 0)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        public void DrawMap(Graphics e)     // Muotojen maalaus pelialuella
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if(grid[i, j] == 1)
                    {
                        e.FillRectangle(Brushes.Blue, new Rectangle(30 + j * size + 1, 50 + i * size + 1, size, size));
                    }
                    if (grid[i, j] == 2)
                    {
                        e.FillRectangle(Brushes.Red, new Rectangle(30 + j * size + 1, 50 + i * size + 1, size, size));
                    }
                    if (grid[i, j] == 3)
                    {
                        e.FillRectangle(Brushes.Yellow, new Rectangle(30 + j * size + 1, 50 + i * size + 1, size, size));
                    }
                    if (grid[i, j] == 4)
                    {
                        e.FillRectangle(Brushes.Violet, new Rectangle(30 + j * size + 1, 50 + i * size + 1, size, size));
                    }
                    if (grid[i, j] == 5)
                    {
                        e.FillRectangle(Brushes.Purple, new Rectangle(30 + j * size + 1, 50 + i * size + 1, size, size));
                    }
                    if (grid[i, j] == 6)
                    {
                        e.FillRectangle(Brushes.Aqua, new Rectangle(30 + j * size + 1, 50 + i * size + 1, size, size));
                    }
                    if (grid[i, j] == 7)
                    {
                        e.FillRectangle(Brushes.Green, new Rectangle(30 + j * size + 1, 50 + i * size + 1, size, size));
                    }
                    if (grid[i, j] == 8)
                    {
                        e.FillRectangle(Brushes.White, new Rectangle(30 + j * size + 1, 50 + i * size + 1, size, size));
                    }
                }
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)  // Graafinen eventti
        {
            DrawMap(e.Graphics);
            ShowNextShape(e.Graphics);
        }

        private void GetRec()           // Ennätyslistan lataus tiedostosta
        {
            bool tryAgain = true;
            while (tryAgain)            // Jos tule virhe, ohjelma poista tiedosto ja käy funktio uudestaan
            {
                try
                {
                    if (File.Exists(folder))
                    {
                        sr = new StreamReader(folder);      // Lue tiedosto
                    }
                    else                // Luo uusi tiedosto, jos tiedostoa ei ole olemassa
                    {
                        StreamWriter sw = new StreamWriter(folder);
                        sw.Write("Max&Juki\n1000\nPlayer2\n900\nPlayer3\n800\nPlayer4\n700\nPlayer5\n" +
                                 "600\nPlayer6\n500\nPlayer7\n400\nPlayer8\n300\nPlayer9\n200\nPlayer10\n100");
                        sw.Close();
                        sr = new StreamReader(folder);
                    }

                    for (int i = 0; i < 20; i++)
                    {
                        lista.Add(sr.ReadLine());
                    }
                    minRec = int.Parse(lista[19]);         // Pienin ennätys, että pääse ennätyslistaan
                    lbChampName.Text = lista[0];                
                    lbChampScore.Text = lista[1];          // Paras ennätys     
                    tryAgain = false;                      
                }
                catch              // Jos ennätys tiedosto on rikki, ilmoitetaan poikkeuksesta
                {
                    sr.Close();
                    lista.Clear();
                    MessageBox.Show("TetrisTop.txt is broken, records reset to default\n");
                    File.Delete(folder);
                }
            }
            sr.Close();
        }

        public void SaveRec()                           // Ennätys listan talletus
        {
            {
                for (int i = 1; i <= 19; i += 2)     // Siirtää uusi ennätys listaan
                {
                    if (score > int.Parse(lista[i]))
                    {
                        lista[i] = score.ToString();
                        lista[i - 1] = player;
                        break;
                    }
                }

                StreamWriter sw = new StreamWriter(folder);  // Tallentaa lista tiedostoon
                foreach (string i in lista)
                    sw.WriteLine(i);
                sw.Close();
            }
        }
    }
}
