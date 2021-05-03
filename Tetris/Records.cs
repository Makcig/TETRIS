using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Tetris
{
    struct recs                         // Tietue DataGridView varten
    {
        public string position;
        public string name;
        public string score;
        
        public string Position
        {
            get { return position; }
        }
        public string Name
        { 
            get { return name; } 
        }
        public string Score
        { 
            get { return score; } 
        }

    }
    public partial class Records : Form
    {
        public static string folder;
        recs[] recs_;
        public Records()
        {
            InitializeComponent();
            UpdateDataGrivView();
            KeyDown += new KeyEventHandler(keyfunc);
        }
        private void keyfunc(object sender, KeyEventArgs e)     // Lisätään näppäimistö eventin
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        Close();
                    }
                    break;
            }
        }
        private void UpdateDataGrivView()
        {
            StreamReader sr = new StreamReader(folder);             // Ladataan tiedosto
            recs_ = new recs[10];                                   // Luo tietue
            for (int i = 0; i < recs_.Length; i++)
            {
                recs_[i] = new recs();
                recs_[i].position = (i+1).ToString();               // Lisätään paikka
                recs_[i].name = sr.ReadLine();                      // Latadaan tiedot tietueen
                recs_[i].score = sr.ReadLine();
            }
            sr.Close();
            for(int i=0;i<recs_.Length;i++)
            {
                dgTable.DataSource = null;
                dgTable.DataSource = recs_;                         // Synkronoidaan tietue ja DataGridView

            }
            dgTable.CellBorderStyle = DataGridViewCellBorderStyle.None; // DataGridin ulkonäön muokkaus
            dgTable.DefaultCellStyle.BackColor = Color.Black;
            dgTable.DefaultCellStyle.ForeColor = Color.Green;
            dgTable.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dgTable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgTable.RowHeadersVisible = false;
            dgTable.ColumnHeadersVisible = false;
            dgTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgTable.RowsDefaultCellStyle.SelectionBackColor = Color.BlueViolet;
            dgTable.Columns[0].Width = 27;
            dgTable.Columns[1].Width = 150;
            dgTable.Columns[2].Width = 80;
        }
    }
}
