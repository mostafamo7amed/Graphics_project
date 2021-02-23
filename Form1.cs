using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int X1 = 0, X2 = 0, Y1 = 0, Y2 = 0, X, Y;
        bool draw = false, newone = false;
        int dir = 1, dir2 = -1;
        void lineDDA(float xb, float yb, float xe, float ye)
        {
            value.Items.Clear();
            float dx = xe - xb;
            float dy = ye - yb;
            float steps = 0;
            if (Math.Abs(dx) > Math.Abs(dy))
                steps = Math.Abs(dx);
            else
                steps = Math.Abs(dy);
            float xinc = dx / steps;
            float yinc = dy / steps;
            float x = xb, y = yb;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int i = 0; i < steps; i++)
            {
               List<int> test = new List<int>();
                int xx = (int)Math.Round(x);
                int yy = (int)Math.Round(y);
                test.Add(i);
                test.Add(xx);
                test.Add(yy);
                DATA.Add(test);
                draw_point(xx, yy, pictureBox1);
                value.Items.Add("( " + xx + " , " + yy + " )");
                x += xinc;
                y += yinc;
            }
        }
        void Bresenham(int xb, int yb, int xe, int ye)
        {
            values.Items.Clear();
            X1 = xb; X2 = xe; Y1 = yb; Y2 = ye;
            int dx = Math.Abs(xe - xb);
            int dy = Math.Abs(ye - yb);
            int P0 = 2 * dy - dx;
            int steps=0,x=0,y=0;
            if (xb > xe)
            {
                x = xe; y = ye;
                steps = xb;
            } else {
                x = xb; y = yb;
                steps = xe;
            }
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            int i = 0;
            while (x < steps)
            {
                List<int> test = new List<int>();
                test.Add(i);
                test.Add(x);
                test.Add(y);
                test.Add(P0);
                x++;
                if (P0 < 0)
                {
                    P0 += 2 * dy;
                }
                else
                {
                    P0 += 2 * dy - 2 * dx;
                    y++;
                }
                draw_point(x, y, pictureBox2);
                values.Items.Add("( " + x + " , " + y + " )");
                i++;
            }
        }
        void circle(int xc, int yc, int r)
        {
            point.Items.Clear();
            int p0 = 1 - r;
            int x = 0, y = r;
            pictureBox3.Image = new Bitmap(pictureBox3.Width, pictureBox3.Height);

            DATA.Clear();

            int i = 0;
            while (x < y)
            {
                List<int> test = new List<int>();
                test.Add(i);
                test.Add(x);
                test.Add(y);
                test.Add(p0);
                DATA.Add(test);
                i++;
                cir8(x, y, xc, yc);
                point.Items.Add("( " +  x + " , " + y + " )");
                if (p0 < 0)
                {
                    p0 += 2 * x + 1;
                }
                else
                {
                    p0 += 2 * x - 2 * y + 1;
                    y--;
                }
                x++;
            }
        }
        void elipse(int xc, int yc, int aa, int bb)
        {
            points.Items.Clear();
            int a = aa;
            int b = bb;

            int d = b * b - a * a * b + a * a / 4;
            int x = 0;
            int y = b;

            int xcenter = xc;
            int ycenter = yc;
            pictureBox4.Image = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            int i = 0;
            DATA.Clear();
            while (2 * b * b * x < 2 * a * a * y)
            {
                elepse4(x, y, xcenter, ycenter, pictureBox4, d, i);
                i++;
                x++;
                if (d < 0)
                {
                    d += 2 * b * b * x + b * b;

                }
                else
                {
                    y--;

                    d += 2 * b * b * x - 2 * a * a * y + b * b;
                }
            }
            double d2 = b * b * (x + 0.5) * (x + 0.5) + a * a * (y - 1) * (y - 1) - a * a * b * b;
            while (y > 0)
            {  
                y--;
                if (d2 < 0)
                {
                    d2 += 2 * b * b * x - 2 * a * a * y + a * a;
                    x++;
                }
                else
                {
                    d2 -= 2 * a * a * y + a * a;
                }
                elepse4(x, y, xcenter, ycenter, pictureBox4, d, i);
                points.Items.Add("( " + x + " , " + y + " )");
                i++;
            }
        }
        public bool cohen(int x1, int y1, int x2, int y2, int xmin, int ymin, int xmax, int ymax)
        {

            int inside = 0;
            int top = 8;
            int bottom = 4;
            int right = 2;
            int left = 1;

            int code1 = complutecode(x1, y1, xmin, ymin, xmax, ymax);
            int code2 = complutecode(x2, y2, xmin, ymin, xmax, ymax);

            bool flag = false;

            while (true)
            {
                if (code1 == 0 & code2 == 0)
                {
                    flag = true;
                    break;
                }
                else if ((code1 & code2) != 0)
                {
                    break;
                }
                else
                {
                    int codeout;
                    int x = 0, y = 0;

                    if (code1 == 0)
                        codeout = code1;
                    else
                        codeout = code2;
                    if ((codeout & top) == top)
                    {
                        y = xmax;
                        x = x1 + (x2 - x1) * (ymax - y1) / (y2 - y1);
                    }
                    else if ((codeout & bottom) == bottom)
                    {
                        y = ymin;
                        x = x1 + (x2 - x1) * (ymax - y1) / (y2 - y1);
                    }
                    else if ((codeout & right) == right)
                    {
                        x = xmax;
                        y = y1 + (y2 - y1) * (xmax - x1) / (x2 - x1);
                    }
                    else if ((codeout & left) == left)
                    {
                        x = xmin;
                        y = y1 + (y2 - y1) * (xmin - x1) / (x2 - x1);
                    }


                    if (codeout == code1)
                    {
                        x1 = x;
                        y1 = y;
                        code1 = complutecode(x1, y1, xmin, ymin, xmax, ymax);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        code2 = code2 = complutecode(x2, y2, xmin, ymin, xmax, ymax);
                    }
                }
            }
            if (flag == true)
            {
                lineDDA(x1, y1, x2, y2);
                return true;
            }
            else
            {

                return false;
            }
        }
        public int complutecode(int x, int y, int xmin, int ymin, int xmax, int ymax)
        {
            int inside = 0;
            int top = 8;
            int bottom = 4;
            int right = 2;
            int left = 1;

            int code = inside;
            if (x < xmin)
                code = code | left;
            if (y < ymin)
                code = code | bottom;
            if (x > xmax)
                code = code | right;
            if (y > ymax)
                code = code | top;
            return code;
        }
        double[,] matrix33_X_matrix33(double[,] arr1, double[,] arr2)
        {
            double[,] res = new double[3, 3];

            for (int i = 0; i < 9; i++)
            {

                for (int col = 0; col < 3; col++)
                {
                    res[i / 3, i % 3] += arr1[i / 3, col] * arr2[col, i % 3];
                }
               
            }

            return res;
        }
        double[] matrix33_X_matrix31(double[,] arr1, double[] arr2)
        {
            double[] res = new double[3];

            for (int i = 0; i < 9; i++)
            {
                res[i / 3] += arr1[i / 3, i % 3] * arr2[i % 3];
            }

            return res;
        }
        double[,] tras_to_zero(double x, double y)
        {
            double[,] trans_to_zero = { { 1, 0, -1 * x }, { 0, 1, -1 * y }, { 0, 0, 1 } };
            return trans_to_zero;
        }
        public void draw_point(int x, int y, PictureBox ppp)
        {
            try
            {
                if (x < 0 || x > ppp.Width || y < 0 || y > ppp.Height)
                {
                    return;
                }
                ((Bitmap)ppp.Image).SetPixel(x, y, Color.White);
                
            }
            catch (Exception es) {
                Console.Out.WriteLine(es.Message);
            }
        }
       List<List<int>> DATA = new List<List<int>>();
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
      
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (newone == false)
            {
                if (draw == false)
                {
                    X1 = X;
                    Y1 = Y;
                    draw = true;
                }
                else if (draw == true)
                {
                    X2 = X;
                    Y2 = Y;
                    newone = true;
                    draw = false;
                    pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    lineDDA(X1, Y1, X2, Y2);
                }
            }
            else if (newone == true)
            {
                X1 = X;
                Y1 = Y;
                draw = true;
                newone = false;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                float x1 = float.Parse(textBox1.Text);
                float x2 = float.Parse(textBox2.Text);
                float y1 = float.Parse(textBox4.Text);
                float y2 = float.Parse(textBox3.Text);
                X1 = (int)x1; X2 = (int)x2; Y1 = (int)y1; Y2 = (int)y2;
              
                lineDDA(x1, y1, x2, y2);
                
            }
            catch (Exception ez)
            {
                Console.Out.WriteLine(ez.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
           
            int tx = int.Parse(textBox6.Text);
            int ty = int.Parse(textBox7.Text);
            X1 += tx;
            X2 += tx;
            Y1 += ty;
            Y2 += ty;
            lineDDA(X1, Y1, X2, Y2);
        }
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }      
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
          
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;
        }
        private void DDA_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            double ceta = double.Parse(textBox5.Text)*Math.PI/180;
            double[,] rotate ={
                      {Math.Cos(ceta),Math.Sin(ceta)*dir,0},
                      {Math.Sin(ceta)*dir2,Math.Cos(ceta),0},
                      {0,0,1}
                      };
            double[,] p1_zer0 = tras_to_zero((X1 + X2) / 2, (Y1 + Y2) / 2);

            double newddx1 = X1 + p1_zer0[0, 2];
            double newddx2 = X2 + p1_zer0[0, 2];

            double newddy1 = Y1 + p1_zer0[1, 2];
            double newddy2 = Y2 + p1_zer0[1, 2];
            
            double[,] come_back = tras_to_zero((X1 + X2) / -2, (Y1 + Y2) / -2);
            
            double[,] test = matrix33_X_matrix33(come_back, rotate);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);
            
            double []point_1_before={X1,Y1,1};
            double []point_2_before={ X2, Y2,1};
            
            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            lineDDA((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            X1 = (int)point_1_after_rotate[0];
            Y1 = (int)point_1_after_rotate[1];
            X2 = (int)point_2_after_rotate[0];
            Y2 = (int)point_2_after_rotate[1];
        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            double sx = double.Parse(textBox9.Text);
            double sy = double.Parse(textBox8.Text);
            double[,] scall ={
                      {sx,0,0},
                      {0,sy,0},
                      {0,0,1}
                      };
            double[,] p1_zer0 = tras_to_zero((X1 + X2) / 2, (Y1 + Y2) / 2);
            double newddx1 = X1 + p1_zer0[0, 2];
            double newddx2 = X2 + p1_zer0[0, 2];

            double newddy1 = Y1 + p1_zer0[1, 2];
            double newddy2 = Y2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((X1 + X2) / -2, (Y1 + Y2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, scall);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { X1, Y1, 1 };
            double[] point_2_before = { X2, Y2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            lineDDA((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            X1 = (int)point_1_after_rotate[0];
            Y1 = (int)point_1_after_rotate[1];
            X2 = (int)point_2_after_rotate[0];
            Y2 = (int)point_2_after_rotate[1];
        }
        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button10_Click(object sender, EventArgs e)
        {
            int x1 = int.Parse(textBox18.Text);
            int y1 = int.Parse(textBox16.Text);

            int x2 = int.Parse(textBox17.Text);
            int y2 = int.Parse(textBox15.Text);
            Bresenham(x1, y1, x2, y2);

        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (newone == false)
            {
                if (draw == false)
                {
                    X1 = X;
                    Y1 = Y;
                    draw = true;
                }
                else if (draw == true)
                {
                    X2 = X;
                    Y2 = Y;
                    newone = true;
                    draw = false;
                    pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    Bresenham(X1, Y1, X2, Y2);
                    
                }
            }
            else if (newone == true)
            {
                X1 = X;
                Y1 = Y;
                draw = true;
                newone = false;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox27.Text);
            int y = int.Parse(textBox25.Text);
            X1 = x;
            Y1 = y;
            int r = int.Parse(textBox26.Text);
            circle(x, y, r);
        }
        
        void cir8(int x,int y,int xc,int yc)
        {
            draw_point(xc + x, yc + y, pictureBox3);
            draw_point(xc - x, yc + y, pictureBox3);
            draw_point(xc + x, yc - y, pictureBox3);
            draw_point(xc - x, yc - y, pictureBox3);

            draw_point(xc + y, yc + x, pictureBox3);
            draw_point(xc - y, yc + x, pictureBox3);
            draw_point(xc + y, yc - x, pictureBox3);
            draw_point(xc - y, yc - x, pictureBox3);

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int r = int.Parse(textBox26.Text);
            X1 = X;
            Y1 = Y;
            circle(X, Y, r);
            MessageBox.Show(X + "  " + Y);
        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;
        }
        private void button20_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox34.Text);
            int y = int.Parse(textBox32.Text);
            int a = int.Parse(textBox33.Text);
            int b = int.Parse(textBox35.Text);
            elipse(x, y, a, b);
            Y1 = y;
            X1 = x;
        }
        void elepse4(int x, int y, int xcenter, int ycenter,PictureBox ppp,int d,int i)
        {
            draw_point(xcenter + x, ycenter + y, ppp);
            draw_point(xcenter - x, ycenter + y, ppp);
            draw_point(xcenter + x, ycenter - y, ppp);
            draw_point(xcenter - x, ycenter - y, ppp);

            List<int> test = new List<int>();
            test.Add(i);
            test.Add(xcenter + x);
            test.Add(ycenter + y);
            test.Add(d);
            DATA.Add(test);

            List<int> test1 = new List<int>();

            test1.Add(i);
            test1.Add(xcenter - x);
            test1.Add(xcenter + y);
            test1.Add(d);
            DATA.Add(test1);

            List<int> test2 = new List<int>();

            test2.Add(i);
            test2.Add(xcenter + x);
            test2.Add(xcenter - y);
            test2.Add(d);
            DATA.Add(test2);

            List<int> test3 = new List<int>();

            test3.Add(i);
            test3.Add(xcenter - x);
            test3.Add(xcenter - y);
            test3.Add(d);
            DATA.Add(test3);

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            int rx = int.Parse(textBox33.Text);
            int ry = int.Parse(textBox35.Text);
            elipse(X, Y, rx, ry);
            MessageBox.Show(X + "  " + Y);
        }
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            X1 = X = e.X;
            Y1 = Y = e.Y;
        }
        private void textBox33_TextChanged(object sender, EventArgs e)
        {

        }
        private void button11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        
        private void button21_Click(object sender, EventArgs e)
        {
            dir = dir * -1;
            dir2 = dir2 * -1;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            double ceta = double.Parse(textBox14.Text);
            double[,] rotate ={
                      {Math.Cos(ceta),Math.Sin(ceta)*dir,0},
                      {Math.Sin(ceta)*dir2,Math.Cos(ceta),0},
                      {0,0,1}
                      };
            double[,] p1_zer0 = tras_to_zero((X1 + X2) / 2, (Y1 + Y2) / 2);
            double newddx1 = X1 + p1_zer0[0, 2];
            double newddx2 = X2 + p1_zer0[0, 2];

            double newddy1 = Y1 + p1_zer0[1, 2];
            double newddy2 = Y2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((X1 + X2) / -2, (Y1 + Y2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, rotate);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { X1, Y1, 1 };
            double[] point_2_before = { X2, Y2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            Bresenham((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            X1 = (int)point_1_after_rotate[0];
            Y1 = (int)point_1_after_rotate[1];

            X2 = (int)point_2_after_rotate[0];
            Y2 = (int)point_2_after_rotate[1];
        }
        private void button14_Click(object sender, EventArgs e)
        {
            double ceta = double.Parse(textBox23.Text) * Math.PI / 180;
            double[,] rotate ={
                      {Math.Cos(ceta),Math.Sin(ceta)*dir,0},
                      {Math.Sin(ceta)*dir2,Math.Cos(ceta),0},
                      {0,0,1}
                      };
            double[,] p1_zer0 = tras_to_zero((X1 + X2) / 2, (Y1 + Y2) / 2);

            double newddx1 = X1 + p1_zer0[0, 2];
            double newddx2 = X2 + p1_zer0[0, 2];

            double newddy1 = Y1 + p1_zer0[1, 2];
            double newddy2 = Y2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((X1 + X2) / -2, (Y1 + Y2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, rotate);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { X1, Y1, 1 };
            double[] point_2_before = { X2, Y2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            lineDDA((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            X1 = (int)point_1_after_rotate[0];
            Y1 = (int)point_1_after_rotate[1];
            X2 = (int)point_2_after_rotate[0];
            Y2 = (int)point_2_after_rotate[1];
        
        }
        private void button13_Click(object sender, EventArgs e)
        {
            int tx = int.Parse(textBox22.Text);
            int ty = int.Parse(textBox21.Text);
            X1 += tx;
            Y1 += ty;
            circle(X1, Y1, int.Parse(textBox26.Text));
        }
        private void button12_Click(object sender, EventArgs e)
        {
            double sx = double.Parse(textBox20.Text);
            double sy = double.Parse(textBox19.Text);
            double[,] points = new double[DATA.Count, 2];
            double[,] points_in_zero = new double[DATA.Count, 2];
            for (int i = 0; i < DATA.Count; i++)
            {
                points[i, 0] = DATA[i][1];
                points[i, 1] = DATA[i][2];
            }

            for (int i = 0; i < DATA.Count; i++)
            {
                double[,] test = tras_to_zero(points[i, 0], points[i, 1]);
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {
            int tx = int.Parse(textBox30.Text);
            int ty = int.Parse(textBox29.Text);
            X1 += tx;
            Y1 += ty;
            elipse(X1, Y1, int.Parse(textBox33.Text), int.Parse(textBox35.Text));
        }

        private void button19_Click(object sender, EventArgs e)
        {
            double ceta = double.Parse(textBox31.Text) * Math.PI / 180;
            pictureBox4.Image = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            for (int i = 0; i < DATA.Count; i++)
            {
                double[,] rotate ={
                      {Math.Cos(ceta),Math.Sin(ceta)*dir,0},
                      {Math.Sin(ceta)*dir2,Math.Cos(ceta),0},
                      {0,0,1}
                      };
                double[,] p1_zer0 = tras_to_zero(X1, Y1);
                double newddx1 = DATA[i][1] + p1_zer0[0, 2];

                double newddy1 = DATA[i][2] + p1_zer0[1, 2];
                double[,] come_back = tras_to_zero(X1 * (-1), Y1 * (-1));

                double[,] test = matrix33_X_matrix33(come_back, rotate);

                double[,] res = matrix33_X_matrix33(test, p1_zer0);

                double[] point_1_before = { DATA[i][1], DATA[i][2], 1 };

                double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);

               
                draw_point((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], pictureBox4);
                DATA[i][1] = (int)point_1_after_rotate[0];
                DATA[i][2] = (int)point_1_after_rotate[0];
            }
        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            pictureBox1.Image = null;
            value.Items.Clear();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            pictureBox2.Image = null;
            values.Items.Clear();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            textBox23.Clear();
            textBox25.Clear();
            textBox26.Clear();
            textBox27.Clear();
            pictureBox3.Image = null;
            point.Items.Clear();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            textBox24.Clear();
            textBox28.Clear();
            textBox29.Clear();
            textBox30.Clear();
            textBox31.Clear();
            textBox32.Clear();
            textBox33.Clear();
            textBox34.Clear();
            textBox35.Clear();
            pictureBox4.Image = null;
            points.Items.Clear();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            int x1 = int.Parse(textBox1.Text);
            int y1 = int.Parse(textBox4.Text);
            int x2 = int.Parse(textBox2.Text);
            int y2 = int.Parse(textBox3.Text);
            int xmin = int.Parse(textBox36.Text);
            int ymin = int.Parse(textBox37.Text);
            int xmax = int.Parse(textBox38.Text);
            int ymax = int.Parse(textBox39.Text);
            cohen(x1, y1, x2, y2, xmin, ymin, xmax, ymax);
        }
        //Rectangle rec = new Rectangle(50, 50, 100, 100);
        private void button26_Click(object sender, EventArgs e)
        {
            int xmin = int.Parse(textBox36.Text);
            int ymin = int.Parse(textBox37.Text);
            int xmax = int.Parse(textBox38.Text);
            int ymax = int.Parse(textBox39.Text);
           
           


        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           // e.Graphics.FillRectangle(Brushes.White, rec);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int tx = int.Parse(textBox13.Text);
            int ty = int.Parse(textBox12.Text);
            X1 += tx;
            X2 += tx;
            Y1 += ty;
            Y2 += ty;
            Bresenham(X1, Y1, X2, Y2);
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            double sx = double.Parse(textBox11.Text);
            double sy = double.Parse(textBox10.Text);
            double[,] scall ={
                      {sx,0,0},
                      {0,sy,0},
                      {0,0,1}
                      };
           
            double[,] p1_zer0 = tras_to_zero((X1 + X2) / 2, (Y1 + Y2) / 2);
            double newddx1 = X1 + p1_zer0[0, 2];
            double newddx2 = X2 + p1_zer0[0, 2];

            double newddy1 = Y1 + p1_zer0[1, 2];
            double newddy2 = Y2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((X1 + X2) / -2, (Y1 + Y2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, scall);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { X1, Y1, 1 };
            double[] point_2_before = { X2, Y2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            Bresenham((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            X1 = (int)point_1_after_rotate[0];
            Y1 = (int)point_1_after_rotate[1];
            X2 = (int)point_2_after_rotate[0];
            Y2 = (int)point_2_after_rotate[1];
        }

        private void button17_Click(object sender, EventArgs e)
        {
            double sx = double.Parse(textBox28.Text);
            double sy = double.Parse(textBox24.Text);
            double[,] scall ={
                      {sx,0,0},
                      {0,sy,0},
                      {0,0,1}
                      };

            double[,] p1_zer0 = tras_to_zero((X1 + X2) / 2, (Y1 + Y2) / 2);
            double newddx1 = X1 + p1_zer0[0, 2];
            double newddx2 = X2 + p1_zer0[0, 2];

            double newddy1 = Y1 + p1_zer0[1, 2];
            double newddy2 = Y2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((X1 + X2) / -2, (Y1 + Y2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, scall);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { X1, Y1, 1 };
            double[] point_2_before = { X2, Y2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            elipse((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            X1 = (int)point_1_after_rotate[0];
            Y1 = (int)point_1_after_rotate[1];
            X2 = (int)point_2_after_rotate[0];
            Y2 = (int)point_2_after_rotate[1];
        }
        }
    
}
