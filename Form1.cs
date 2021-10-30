using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Neuro_01
{
    public partial class Form1 : Form
    {
        Color CurrentColor = Color.Black; //Brushes.DeepSkyBlue
        bool IsPressed = false;
        Point CurrentPoint;
        Point PrevPoint;
        Graphics g1;

        //массивы значений
        int[,] jaggedArray1 = new int[28, 28];
        float[,] jaggedArray2 = new float[28, 28];
       
        // графика на панели 2, крупные квадраты
        Graphics g2;

        public Form1()
        {
            InitializeComponent();
            g1 = panel1.CreateGraphics();
            g2 = panel2.CreateGraphics();



        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            IsPressed = true;
            CurrentPoint = e.Location;



        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            IsPressed = false;
 
            textBox1.Clear();
            string s1 = "";
            string s2 = "";

            string f1 = "";
            string f2 = "";

            for (int i = 0; i < 28; i++)
            {
                s2 = "";
                f2 = "";
                for (int j = 0; j < 28; j++)
                {
                    s2 = s2 + (jaggedArray1[j, i]) + " ";
                    f2 = f2 + $"{jaggedArray2[j, i],4}"  + " ";
                    
        }
                s1 = s1 + s2 + Environment.NewLine;
                f1 = f1 + f2 + Environment.NewLine;
            }
            textBox1.Text = s1;
            textBox2.Text = f1;

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsPressed)
            {
                PrevPoint = CurrentPoint;
                CurrentPoint = e.Location;

                for_paint();

                if ((e.X < 28)&&(e.Y < 28)&&(e.X > 0) && (e.Y > 0))
                {
                    //Записать в масссив целых
                    jaggedArray1[e.X, e.Y] = 1;

                    //Записать в масссив с точкой
                    for (int i = 0; i < 28; i++)
                    {
                        for (int j = 0; j < 28; j++)
                        {
                            if ( (e.X != i) && (e.Y != j) )
                            {
                                float tst = 0.8f / ((i - e.X) * (i - e.X) + (j - e.Y) * (j - e.Y));
                                if (tst > 0.1f)
                                {
                                     jaggedArray2[i, j] = tst;
                                }

                            }

                            if ((e.X == i) && (e.Y == j))
                            {
                                jaggedArray2[i, j] = 1.0f;
                            }
                            
                        }
                    }
   

                }



            }
        }


        private void for_paint()
        {
            Pen blackPen = new Pen(CurrentColor);
            g1.DrawLine(blackPen, PrevPoint, CurrentPoint);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
            textBox1.Clear();
            textBox2.Clear();
            

            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    jaggedArray1[i, j] = 0;
                    jaggedArray2[i, j] = 0;
                }
            }

        }


    }
}
