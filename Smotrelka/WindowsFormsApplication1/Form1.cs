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
    public partial class Form1 : Form
    {

        Image MemForImage; 
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult _result = MessageBox.Show("Вы действительно хотите выйти из приложения?", "Внимание!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(_result == DialogResult.Yes)
                Application.Exit();
        }

        private void загрузитьJpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImage(true);
        }

        private void загрузитьBmpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImage(false);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            LoadImage(true);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            LoadImage(false);
        }

        private void LoadImage(bool jpg)
        {
            openFileDialog1.InitialDirectory = "c:";

            if (jpg)
                openFileDialog1.Filter = "image (JPEG) files (*.jpg)|*.jpg|All files (*.*)|*.*";
            else
                openFileDialog1.Filter = "image (PNG) files (*.png)|*.png|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try // безопасная попытка 
                {
                    // пытаемся загрузить файл с именем openFileDialog1.FileName - выбранный пользователем файл. 
                    MemForImage = Image.FromFile(openFileDialog1.FileName);
                    // устанавливаем картинку в поле элемента PictureBox 
                    pictureBox1.Image = MemForImage;
                }
                catch (Exception ex) // если попытка загрузки не удалась 
                {
                    // выводим сообщение с причиной ошибки 
                    MessageBox.Show("Не удалось загрузить файл: " + ex.Message);
                }

            }
  
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // создаем новый экземпляр класса Preview, 
            // отвечающего за работу с нашей дополнительной формой 
            // в качестве параметра мы передаем наше загруженное изображение 
            Form PreView = new Preview(MemForImage);
            // затем мы вызываем диалоговое окно 
            PreView.ShowDialog();
        }
    }
}
