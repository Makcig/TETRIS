using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Champions : Form
    {
        public static int SalutLocationX;               // Ilotulitus koordinaatit pääikkunalta
        public static int SalutLocationY;               
        Salut form3 = new Salut();
        public Champions()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(keyfunc);

            form3.StartPosition = FormStartPosition.Manual;
            form3.Location = new Point(SalutLocationX, SalutLocationY);
            form3.Show();
        }
        private void keyfunc(object sender, KeyEventArgs e)     
        {
            switch (e.KeyCode)              // Käytössä 'switch' tarvittaessa tulevaan kehitystä varten
            {
                case Keys.Escape:
                    {
                        form3.Hide();
                        Close();
                    }
                    break;
            }
        }

        private bool CheckText(string text, out string errorMsg)    // Tarkistetaan että teksti ei ole tyhjä
        {
            bool ret = true;
            errorMsg = "";
            if (text == string.Empty)
            {
                errorMsg = "This field can't be empty!";
                ret = false;
            }
            return ret;
        }
        private void tbName_Validated(object sender, EventArgs e)   // Validointi tekstikentälle
        {
            lbError.ForeColor = Color.FromArgb(0, 192, 0);
            Tetris.player = tbName.Text;
            lbError.Text = "OK";
            
        }
        private void tbName_Validating(object sender, CancelEventArgs e)    // Jos teksti puuttuu, tulee ilmoitus
        {
            string errorMsg;
            if (!CheckText(tbName.Text, out errorMsg))
            {
                e.Cancel = true;
                lbError.Text = errorMsg;
            }
        }

        private void TbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)        // Jos käyttäjä paina Enter, siiry seuraavan kohtaan
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.Escape)
                e.SuppressKeyPress = true;
        }

        private void TbAnswer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 'y' && e.KeyChar != 'n')
            {
                e.Handled = true;
            }
            if (e.KeyChar == 'n')           // Jos käyttäjä vasta ei('n'), sulje Champions ikkuna
            {                               // tallentaa ennätykset, ja sulje ohjelma
                DialogResult = DialogResult.No;
                form3.Hide();
                Close();
            }
            else if (e.KeyChar == 'y')
            {
                DialogResult = DialogResult.OK;
                form3.Hide();
                Close();
            }
        }
    }
}
