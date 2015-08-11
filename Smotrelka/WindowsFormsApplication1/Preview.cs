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
    public partial class Preview : Form
    {
        // объект Image для хранения изображения 
        private Image ToView;
        // модифицируем конструктор окна таким образом, чтобы он получал 
        // в качестве параметра изображение для отображения 

        public Preview(Image view)
        {
            // получаем изображение 
            ToView = view;
            InitializeComponent();

        }

        private void Preview_Load(object sender, EventArgs e)
        {
            // если объект, хранящий изображение не равен null 
            if (ToView != null)
            {
                // устанавливаем новые размеры элемента pictureBox1, 
                // равные ширине (ToView.Width) и высоте (ToView.Height) загружаемого изображения. 
                pictureBox1.Size = new Size(ToView.Width, ToView.Height);
                // устанавливаем изображение для отображения в элементе pictureBox1 
                pictureBox1.Image = ToView;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
