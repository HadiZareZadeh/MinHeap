using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinHeap___SJ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            graphics = CreateGraphics();
        }
        Graphics graphics;

        int[] array = new int[1000];
        int n = 0;

        private void MakeMinHeap(int[] a, int n, int i)
        {
            int s = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;
            if (l < n && a[l] > a[s])
                s = l;
            if (r < n && a[r] > a[s])
                s = r;
            if (s != i)
            {
                int temp = a[i];
                a[i] = a[s];
                a[s] = temp;
                MakeMinHeap(a, n, s);
            }
        }
        
        private void heapSort(int[] a, int n)
        {
            for (int i = (n / 2) - 1; i >= 0; i--)
                MakeMinHeap(a, n, i);
            
            for (int i = n - 1; i >= 0; i--)
            {
                int temp = a[0];
                a[0] = a[i];
                a[i] = temp;
                MakeMinHeap(a, i, 0);
            }
        }

        List<PointF> points;
        int x, y;
        private void PrintGraph()
        {
            points = new List<PointF>();
            int b = 1;
            int i = 0;
            while(i < n)
            {
                for (int j = 0; j < b && i < n; j++, i++)
                {
                    graphics.FillEllipse(Brushes.LightSeaGreen, x + (2 * x * j) - (10), y, 20, 20);
                    graphics.DrawEllipse(Pens.Black, x + (2 * x * j) - (10), y, 20, 20);
                    graphics.DrawString(array[i].ToString(), Font, Brushes.Black, x + (2 * x * j) + 6 - (10), y + 6);
                    points.Add(new PointF(x + (2 * x * j) - (10), y));
                }
                b *= 2;
                y += 40;
                x /= 2;
            }

            for(i = 1; i < n; i++)
            {
                PointF p1 = points[(i - 1) / 2];
                PointF p2 = points[i];

                p1.Y += 20;
                p1.X += 10;

                p2.X += 10;

                graphics.DrawLine(Pens.Black, p1, p2);
            }
        }

        private void Log()
        {
            txtLog.Clear();
            for(int i = 0; i < n; i++)
            {
                txtLog.Text += array[i] + " ";
            }
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            x = Width / 2;
            y = 10;
            int number = Convert.ToInt32(txtNumber.Text);
            array[n] = number;
            n++;
            heapSort(array, n);
            Log();
            graphics.Clear(BackColor);
            PrintGraph();
            txtNumber.Clear();
        }
    }
}
