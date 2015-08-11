using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FormSecretPicture : Form
    {
        private int _vievers = 0;
        public FormSecretPicture()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (_vievers >= 6)
            {
                this.BackgroundImage = Properties.Resources.PicSec;
                _vievers = 0;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.Pic1;
                _vievers++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Pic2;
            _vievers++;
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Pic3;
            _vievers++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _vievers++;
            this.BackgroundImage = Properties.Resources.Pic4;
        }

        private void FormSecretPicture_Load(object sender, EventArgs e)
        {
        }

        private void FormSecretPicture_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void FormSecretPicture_MouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
