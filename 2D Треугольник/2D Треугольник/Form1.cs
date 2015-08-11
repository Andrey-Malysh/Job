using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace _2D_Треугольник
{
    public partial class Form1 : Form
    {

        double a = 1, b = 0, c = 0; 
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();

            label1.Text = "A";
            label2.Text = "B";
            label3.Text = "C";

            trackBar1.Minimum = 0;
            trackBar1.Maximum = 1000;
            trackBar1.TickFrequency = 10;

            trackBar2.Minimum = 0;
            trackBar2.Maximum = 1000;
            trackBar2.TickFrequency = 10;

            trackBar3.Minimum = 0;
            trackBar3.Maximum = 1000;
            trackBar3.TickFrequency = 10;

            label4.Text = trackBar1.Value.ToString();
            label5.Text = trackBar2.Value.ToString();
            label6.Text = trackBar3.Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();

            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            Gl.glClearColor(255, 255, 255, 1);

            Gl.glViewport(0,0,AnT.Width,AnT.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);

            Gl.glLoadIdentity();

            if (AnT.Width <= AnT.Height)
                Glu.gluOrtho2D(0, 30, 0, 30*(float)AnT.Height/AnT.Width);
            else
                Glu.gluOrtho2D(0,30*(float)AnT.Width/AnT.Height,0,30);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            // активируем рисование в режиме GL_TRIANGLES, при котором задание 
            // трех вершин с помощью функции glVertex2d или glVertex3d 
            // будет объединяться в трехгранный полигон (треугольник) 

            Gl.glBegin(Gl.GL_TRIANGLES);

            Gl.glColor3d(a, b, c);

            Gl.glVertex2d(5, 5); // Вершина

            Gl.glColor3d(c, a, b);

            Gl.glVertex2d(25, 5); // Вершина

            Gl.glColor3d(b, c, a);

            Gl.glVertex2d(5, 25); // Вершина

            Gl.glEnd();

            Gl.glFlush();

            AnT.Invalidate();

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            a = (double) trackBar1.Value/1000;

            label4.Text = Convert.ToString(a);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            b = (double) trackBar2.Value/1000;

            label5.Text = Convert.ToString(b);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            c = (double) trackBar3.Value/1000;

            label6.Text = Convert.ToString(c);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw();
        }




    }
}
