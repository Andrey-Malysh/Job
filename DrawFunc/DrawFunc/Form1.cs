using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace DrawFunc
{
    public partial class Form1 : Form
    {

        // размеры окна 
        private double ScreenW, ScreenH;

        // отношения сторон окна визуализации 
        // для корректного перевода координат мыши в координаты, 
        // принятые в программе 

        private float devX;
        private float devY;

        // массив, который будет хранить значения x,y точек графика 
        private float[,] GrapValuesArray;
        // количество элементов в массиве 
        private int elements_count = 0;

        // флаг, означающий, что массив с значениями координат графика пока еще не заполнен 
        private bool not_calculate = true;

        // номер ячейки массива, из которой будут взяты координаты для красной точки 
        // для визуализации текущего кадра 
        private int pointPosition = 0;

        // вспомогательные переменные для построения линий от курсора мыши к координатным осям 
        private float lineX, lineY;

        // текущение координаты курсора мыши 
        private float Mcoord_X = 0, Mcoord_Y = 0;

        // Параметр значений по оси обцисст
        private float _minX;
        private float _maxX;

        // Параметр значений по оси ординат
        private float _minY;
        private float _maxY;

        //смещение центра кординат
        private float _CentreCordinatsX;
        private float _CentreCordinatsY;

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
            InitMyParametrs();
        }

        private void InitMyParametrs()
        {
            int _leghtANT = 550;

            AnT.Width = AnT.Height = _leghtANT;

            float _razmer = 1.5f;



            _minX = _minY = -_razmer;
            _maxX = _maxY = _razmer;

            _CentreCordinatsX = 1.5f;
            _CentreCordinatsY = 1.5f;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();

            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            Gl.glClearColor(255, 255, 255, 1);

            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            //if (AnT.Width <= AnT.Height)
            //{
            //    ScreenW = 30.0;
            //    ScreenH = 30.0*(float) AnT.Height/AnT.Width;
            //    Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);v
            //}
            //else
            //{
            //    ScreenW = 30.0*(float) AnT.Width/(float) AnT.Height;
            //    ScreenH = 30.0;
            //    Glu.gluOrtho2D(0.0, 30.0*(float) AnT.Width/AnT.Height, 0.0, 30.0);
            //}

            if (AnT.Width <= AnT.Height)
            {
                ScreenW = (_maxX - _minX)*(float) AnT.Height/AnT.Width;
                ScreenH = (_maxY - _minY);
                Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);
            }
            else
            {
                ScreenW = (_maxX - _minX)*(float)AnT.Width/AnT.Height;
                ScreenH = (_maxY - _minY);
                Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);
            }


            // сохранение коэффициентов, которые нам необходимы для перевода координат указателя 
            // в оконной системе в координаты, 
            // принятые в нашей OpenGL сцене 
            devX = (float) ScreenW/(float) AnT.Width;
            devY = (float) ScreenH/(float) AnT.Height;

            // установка объектно-видовой матрицы 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            // старт счетчика, отвечающего за вызов функции визуализации сцены 
            PointInGrap.Start();

        }

        private void PointInGrap_Tick(object sender, EventArgs e)
        {
            // если мы дошли до последнего элемента массива 
            if (pointPosition == elements_count - 1)
                pointPosition = 0; // переходим к начальному элементу 

            // функция визуализации 
            Draw();

            // переход к следующему элементу массива 
            pointPosition++;
        }

        private void AnT_MouseMove(object sender, MouseEventArgs e)
        {
            // сохраняем координаты мыши 
            Mcoord_X = e.X;
            Mcoord_Y = e.Y;

            // вычисляем параметры для будущей дорисовки линий от указателя мыши к координатным осям. 
            lineX = devX*e.X;
            lineY = (float) (ScreenH - devY*e.Y);
        }

        private void PrintText2D(float x, float y, string text)
        {

            // устанавливаем позицию вывода растровых символов 
            // в переданных координатах x и y. 
            Gl.glRasterPos2f(x, y);

            // в цикле foreach перебираем значения из массива text, 
            // который содержит значение строки для визуализации 
            foreach (char char_for_draw in text)
            {
                // символ C визуализируем с помощью функции glutBitmapCharacter, используя шрифт GLUT_BITMAP_9_BY_15. 
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_9_BY_15, char_for_draw);
            }

        }

        private void functionCalculation()
        {

            // определение локальных переменных X и Y 
            float x = 0, y = 0;

            // инициализация массива, который будет хранить значение 300 точек, 
            // из которых будет состоять график 

            GrapValuesArray = new float[1000, 2];

            // счетчик элементов массива 
            elements_count = 0;

            // вычисления всех значений y для x, принадлежащего промежутку от -15 до 15 с шагом в 0.01f 
            for (x = _minX; x < _maxX; x += 0.1f)
            {
                // вычисление y для текущего x 
                // по формуле y = (float)Math.Sin(x)*3 + 1; 
                // эта строка задает формулу, описывающую график функции для нашего уравнения y = f(x). 
                y = (float) Math.Sin(x);

                // запись координаты x 
                GrapValuesArray[elements_count, 0] = x;
                // запись координаты y 
                GrapValuesArray[elements_count, 1] = y;
                // подсчет элементов 
                elements_count++;

            }

            // изменяем флаг, сигнализировавший о том, что координаты графика не вычислены 
            not_calculate = false;

        }

        float Func(float x)
        {
            return (float)Math.Sin(x/10)/10;
        }

        private void DrawDiagram()
        {

            // проверка флага, сигнализирующего о том, что координаты графика вычислены 
            if (not_calculate)
            {
                // если нет, то вызываем функцию вычисления координат графика 
                functionCalculation();
            }

            // стартуем отрисовку в режиме визуализации точек 
            // объединяемых в линии (GL_LINE_STRIP) 
            Gl.glBegin(Gl.GL_LINE_STRIP);

            // рисуем начальную точку 
            Gl.glVertex2d(GrapValuesArray[0, 0], GrapValuesArray[0, 1]);

            // проходим по массиву с координатами вычисленных точек 
            for (int ax = 1; ax < elements_count; ax+=2)
            {
                // передаем в OpenGL информацию о вершине, участвующей в построении линий 
                Gl.glVertex2d(GrapValuesArray[ax, 0], GrapValuesArray[ax, 1]);
            }

            // завершаем режим рисования 
            Gl.glEnd();

            // устанавливаем размер точек, равный 5 пикселям 
            Gl.glPointSize(5);
            // устанавливаем текущим цветом - красный цвет 
            Gl.glColor3f(255, 0, 0);
            // активируем режим вывода точек (GL_POINTS) 
            Gl.glBegin(Gl.GL_POINTS);
            // выводим красную точку, используя ту ячейку массива, до которой мы дошли (вычисляется в функции обработчике событий таймера) 
            Gl.glVertex2d(GrapValuesArray[pointPosition, 0], GrapValuesArray[pointPosition, 1]);
            // завершаем режим рисования 
            Gl.glEnd();
            // устанавливаем размер точек равный единице 
            Gl.glPointSize(1);

        }


        // функция, управляющая визуализацией сцены 
        private void Draw()
        {

            // очистка буфера цвета и буфера глубины 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // очищение текущей матрицы 
            Gl.glLoadIdentity();

            // установка черного цвета 
            Gl.glColor3f(0, 0, 0);

            // помещаем состояние матрицы в стек матриц 
            Gl.glPushMatrix();

            // выполняем перемещение в пространстве по осям X и Y 
            Gl.glTranslated(_CentreCordinatsX, _CentreCordinatsY, 0);
            //Gl.glTranslated(0, 15, 0);

            // активируем режим рисования (Указанные далее точки будут выводиться как точки GL_POINTS) 
            Gl.glBegin(Gl.GL_POINTS);

            // с помощью прохода вдумя циклами, создаем сетку из точек (-15,15)
            for (int ax = (int) _minX; ax < _maxX; ax++)
            {
                for (int bx = (int) _minY; bx < _maxY; bx++)
                {
                    // вывод точки 
                    Gl.glVertex2d(ax, bx);
                }
            }

            // завершение режима рисования примитивов 
            Gl.glEnd();

            // активируем режим рисования, каждые 2 последовательно вызванные команды glVertex 
            // объединяются в линии 
            Gl.glBegin(Gl.GL_LINES);

            // далее мы рисуем координатные оси и стрелки на их концах 
            Gl.glVertex2d(0, _minX);
            Gl.glVertex2d(0, _maxX);

            Gl.glVertex2d(_minY, 0);
            Gl.glVertex2d(_maxY, 0);

            // вертикальная стрелка 
            Gl.glVertex2d(0, _maxY);
            Gl.glVertex2d(0.2, _maxY - 0.5);
            Gl.glVertex2d(0, _maxY);
            Gl.glVertex2d(-0.2, _maxY - 0.5);

            // горизонтальная трелка 
            Gl.glVertex2d(_maxX, 0);
            Gl.glVertex2d(_maxX - 0.5, 0.2);
            Gl.glVertex2d(_maxX, 0);
            Gl.glVertex2d(_maxX - 0.5, -0.2);

            // завершаем режим рисования 
            Gl.glEnd();

            // выводим подписи осей "x" и "y" 
            PrintText2D(_maxX + (float) 0.5, 0, "x");
            PrintText2D(0.5f, _maxY - (float) 0.5, "y");

            // вызываем функцию рисования графика 
            DrawDiagram();

            // возвращаем матрицу из стека 
            Gl.glPopMatrix();

            // выводим текст со значением координат возле курсора 
            PrintText2D(devX*Mcoord_X + 0.2f, (float) ScreenH - devY*Mcoord_Y + 0.4f,
                "[ x: " + (devX*Mcoord_X - _maxX).ToString() + " ; y: " + ((float) ScreenH - devY*Mcoord_Y - _maxY).ToString() +
                "]");

            label1.Text = (devX * Mcoord_X - _maxX).ToString();
            label2.Text = (_maxY - devY * Mcoord_Y).ToString();
            label3.Text = GrapValuesArray[(int) Mcoord_X, 0].ToString();
            label4.Text = GrapValuesArray[(int) Mcoord_X, 1].ToString();


            // устанавливаем красный цвет 
            Gl.glColor3f(255, 0, 0);

            // включаем режим рисования линий, для того чтобы нарисовать 
            // линии от курсора мыши к координатным осям 
            Gl.glBegin(Gl.GL_LINES);

            Gl.glVertex2d(lineX, _CentreCordinatsX);
            Gl.glVertex2d(lineX, lineY);
            Gl.glVertex2d(_CentreCordinatsY, lineY);
            Gl.glVertex2d(lineX, lineY);

            Gl.glEnd();

            // дожидаемся завершения визуализации кадра 
            Gl.glFlush();

            // сигнал для обновление элемента реализующего визуализацию. 
            AnT.Invalidate();

        }


    }
}
