using System.Windows.Forms;

namespace Tetris
{
    public partial class Salut : Form
    {
        public Salut()
        {
            InitializeComponent();
            KeyUp += new KeyEventHandler(keyfunc);
        }
        private void keyfunc(object sender, KeyEventArgs e)
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
    }
}
