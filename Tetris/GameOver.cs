using System.Windows.Forms;

namespace Tetris
{
    public partial class GameOver : Form
    {
        public GameOver()
        {
            InitializeComponent();
        }

        private void TbAnswer2_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar != 'y' && e.KeyChar != 'n')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == 'n')
                {
                    Application.Exit();
                }
                else if(e.KeyChar == 'y')
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }
    }
}
