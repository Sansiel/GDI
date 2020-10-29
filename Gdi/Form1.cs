using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Gdi
{
    public partial class Form1 : Form
    {
        int local_x = 300;
        int local_y = 150;
        int local_r = 0;
        float local_s = 1;

        Color start_color;
        Color end_color;
        //System.Threading.Timer timer;
        System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            start_color = label1.ForeColor;
            end_color = Color.Red;
            LoadData();
        }
        
        private void LoadData()
        {
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // sea
            Graphics gs = Graphics.FromImage(bm);
            Point pointSea1 = new Point(0, 0);
            Point pointSea2 = new Point(pictureBox1.Width, 0);
            Point pointSea3 = new Point(pictureBox1.Width, pictureBox1.Height);
            Point pointSea4 = new Point(0, pictureBox1.Height);
            Point[] seaPoints = { pointSea1, pointSea2, pointSea3, pointSea4 };

            //Кисть Градиентная
            Rectangle rectangle1 = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            RectangleF convertedRectangle = rectangle1;
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(convertedRectangle, Color.CadetBlue, Color.Blue, LinearGradientMode.Horizontal);
            gs.FillPolygon(myLinearGradientBrush, seaPoints);

            // boat
            Graphics gb = Graphics.FromImage(bm);
            Point point1 = new Point(0, 30);
            Point point2 = new Point(23, 30);
            Point point3 = new Point(23 , 0);
            Point point4 = new Point(40 , 10);
            Point point5 = new Point(27, 20);
            Point point6 = new Point(27, 30);
            Point point7 = new Point(50, 30);
            Point point8 = new Point(40, 50);
            Point point9 = new Point(10, 50);

            gb.TranslateTransform(local_x, local_y);
            gb.RotateTransform(local_r);
            gb.ScaleTransform(local_s, local_s);

            Point[] boatPoints = { point1, point2, point3, point4, point5, point6, point7, point8, point9 };            
            gb.FillPolygon(Brushes.SaddleBrown, boatPoints);

            //Кисть Штриховая
            HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Horizontal, Color.Black, Color.Brown);
            Point[] boatP = { point1, point2, point7, point8, point9 };
            gb.FillPolygon(myHatchBrush, boatP);

            // flag
            Point pointFlag1 = new Point(25, 0);
            Point pointFlag2 = new Point(40, 10);
            Point pointFlag3 = new Point(27, 20);
            Point pointFlag4 = new Point(25, 20);
            Point[] flagPoints = { pointFlag1, pointFlag2, pointFlag3, pointFlag4 };
            // Ручка
            Pen flagPen = new Pen(Brushes.Black, 2);
            gb.FillPolygon(Brushes.White, flagPoints);
            gb.DrawPolygon(flagPen, boatPoints);

            // ice
            Graphics gi = Graphics.FromImage(bm);
            Point pointIce1 = new Point(50, 60);
            Point pointIce2 = new Point(60, 40);
            Point pointIce3 = new Point(65, 45);
            Point pointIce4 = new Point(70, 30);
            Point pointIce5 = new Point(75, 60);
            Point[] icePoints = { pointIce1, pointIce2, pointIce3, pointIce4, pointIce5 };

            //Кисть Сплошная
            gi.FillPolygon(Brushes.LightBlue, icePoints);
            gi.TranslateTransform(100, 50);
            gi.FillPolygon(Brushes.LightBlue, icePoints);
            gi.TranslateTransform(150, 150);
            gi.FillPolygon(Brushes.LightBlue, icePoints);


            pictureBox1.Image = bm;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.W) local_y -= 5;
            if (e.KeyData == Keys.D) local_x += 5;
            if (e.KeyData == Keys.A) local_x -= 5;
            if (e.KeyData == Keys.S) local_y += 5;

            if (e.KeyData == Keys.Q) local_r += 10;
            if (e.KeyData == Keys.E) local_r -= 10;

            if (e.KeyData == Keys.Z) local_s = local_s / 2;
            if (e.KeyData == Keys.X) local_s = local_s * 2;

            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TimerCallback tm = new TimerCallback(Animo);
            //timer = new System.Threading.Timer(tm, 0, 0, 300);
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += Animo;
            timer.Start();
        }

        //private void Animo(object obj)
        private void Animo(object sender, EventArgs e)
        {
            if (local_x >= 30)
            {
                local_x -= 1;
                LoadData();

                // running string
                if (label1.ForeColor == start_color) label1.ForeColor = end_color;
                else label1.ForeColor = start_color;
                if (label1.Left > -label1.Width) label1.Left -= 10;
                else label1.Left = this.Width;
            }
            else
            {
                //pictureBox1.Image = Image.FromFile("D:\\stop.jfif");
                //timer.Dispose(); Console.WriteLine("stop_timer");
                timer.Stop();
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.W) local_y -= 5;
            if (e.KeyData == Keys.D) local_x += 5;
            if (e.KeyData == Keys.A) local_x -= 5;
            if (e.KeyData == Keys.S) local_y += 5;

            if (e.KeyData == Keys.Q) local_r += 10;
            if (e.KeyData == Keys.E) local_r -= 10;

            if (e.KeyData == Keys.Z) local_s = local_s / 2;
            if (e.KeyData == Keys.X) local_s = local_s * 2;

            LoadData();
        }
    }
}
