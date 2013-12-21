using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace NYCountdown
{
    using System.Globalization;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string days = "0", hours = "0", minutes = "0", seconds = "0";

        private void glControl1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Scale(0.25, 0.25, 1);
            double halfwidth = 145*(0.5/91);

            GL.Color3(Color.Red);
            GL.PushMatrix();
            {
                GL.Translate(-8*halfwidth,0,0);
                drawDays(days);
            }
            GL.PopMatrix();

          //  GL.Color3(Color.Orange);
            GL.PushMatrix();
            {
                GL.Translate(-2 * halfwidth, 0, 0);
                drawPair(hours);
            }
            GL.PopMatrix();

          //  GL.Color3(Color.Yellow);
            GL.PushMatrix();
            {
                GL.Translate(1 * halfwidth, 0, 0);
                drawColon();
            }
            GL.PopMatrix();

          //  GL.Color3(Color.GreenYellow);
            GL.PushMatrix();
            {
                GL.Translate(4 * halfwidth, 0, 0);
                drawPair(minutes);
            }
            GL.PopMatrix();

          //  GL.Color3(Color.Cyan);
            GL.PushMatrix();
            {
                GL.Translate(7 * halfwidth, 0, 0);
                drawColon();
            }
            GL.PopMatrix();
            
           // GL.Color3(Color.Blue);
            GL.PushMatrix();
            {
                GL.Translate(10 * halfwidth, 0, 0);
                drawPair(seconds);
            }
            GL.PopMatrix();

            glControl1.SwapBuffers();
        }

        #region segments

        private static void drawSSeg1()
        {
            GL.Begin(BeginMode.Polygon);
            {
                // top corner
                GL.Vertex2(-25*(0.5/91), 90*(0.5/91));
                GL.Vertex2(-14*(0.5/91), 100*(0.5/91));
                GL.Vertex2(25*(0.5/91), 63*(0.5/91));

                // bottom/central corner
                GL.Vertex2(25*(0.5/91), -77*(0.5/91));
                GL.Vertex2(0, -100*(0.5/91));
                GL.Vertex2(-25*(0.5/91), -100*(0.5/91));
            }
            GL.End();
        }

        private static void drawSSeg2()
        {
            GL.Begin(BeginMode.Polygon);
            {
                // left corner
                GL.Vertex2(-68*(0.5/91), -24*(0.5/91));
                GL.Vertex2(-105*(0.5/91), 15*(0.5/91));
                GL.Vertex2(-97*(0.5/91), 24*(0.5/91));

                // right corner
                GL.Vertex2(97*(0.5/91), 24*(0.5/91));
                GL.Vertex2(105*(0.5/91), 15*(0.5/91));
                GL.Vertex2(68*(0.5/91), -24*(0.5/91));
            }
            GL.End();
        }


        private static void drawSSeg3()
        {
            GL.PushMatrix();
            {
                GL.Scale(-1, 1, 1);
                drawSSeg1();
            }
            GL.PopMatrix();
        }

        private static void drawSSeg4()
        {
            GL.PushMatrix();
            {
                GL.Scale(1, -1, 1);
                drawSSeg3();
            }
            GL.PopMatrix();
        }

        private static void drawSSeg5()
        {
            GL.PushMatrix();
            {
                GL.Scale(1, -1, 1);
                drawSSeg2();
            }
            GL.PopMatrix();
        }

        private static void drawSSeg6()
        {
            GL.PushMatrix();
            {
                GL.Scale(1, -1, 1);
                drawSSeg1();
            }
            GL.PopMatrix();
        }

        private static void drawSSeg7()
        {
            GL.Begin(BeginMode.Polygon);
            {
                GL.Vertex2(0.5, 0.0);

                GL.Vertex2(67*(0.5/91), 24*(0.5/91));
                GL.Vertex2(-67*(0.5/91), 24*(0.5/91));

                GL.Vertex2(-0.5, 0);

                GL.Vertex2(-67*(0.5/91), -24*(0.5/91));
                GL.Vertex2(67*(0.5/91), -24*(0.5/91));
            }
            GL.End();
        }

        private static void drawSeg1()
        {
            GL.PushMatrix();
            {
                GL.Translate(-96*(0.5/91), 104*(0.5/91), 0);
                drawSSeg1();
            }
            GL.PopMatrix();
        }

        private static void drawSeg2()
        {
            GL.PushMatrix();
            {
                GL.Translate(0, 195*(0.5/91), 0);
                drawSSeg2();
            }
            GL.PopMatrix();
        }

        private static void drawSeg3()
        {
            GL.PushMatrix();
            {
                GL.Translate(96*(0.5/91), 104*(0.5/91), 0);
                drawSSeg3();
            }
            GL.PopMatrix();
        }

        private static void drawSeg4()
        {
            GL.PushMatrix();
            {
                GL.Translate(96*(0.5/91), -104*(0.5/91), 0);
                drawSSeg4();
            }
            GL.PopMatrix();
        }

        private static void drawSeg5()
        {
            GL.PushMatrix();
            {
                GL.Translate(0, -195*(0.5/91), 0);
                drawSSeg5();
            }
            GL.PopMatrix();
        }

        private static void drawSeg6()
        {
            GL.PushMatrix();
            {
                GL.Translate(-96*(0.5/91), -104*(0.5/91), 0);
                drawSSeg6();
            }
            GL.PopMatrix();
        }

        private static void drawSeg7()
        {
            GL.PushMatrix();
            {
                drawSSeg7();
            }
            GL.PopMatrix();
        }

        #endregion

        private void glControl1_Resize(object sender, EventArgs e)
        {
            GL.Viewport(this.ClientRectangle);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            double aspect = (double) glControl1.Width / glControl1.Height;
            GL.Ortho(-2 * aspect, 2 * aspect, -2, 2, -1, 1);
            glControl1.Invalidate();
        }

        private void drawChar(char x)
        {
            if (
                x == '4' ||
                x == '5' ||
                x == '6' ||
                x == '8' ||
                x == '9' ||
                x == '0' ||
                x == 'a' ||
                x == 'b' ||
                x == 'c' ||
                x == 'e' ||
                x == 'f')
            {
                drawSeg1();
            }
            if (
                x == '2' ||
                x == '3' ||
                x == '5' ||
                x == '6' ||
                x == '7' ||
                x == '8' ||
                x == '9' ||
                x == '0' ||
                x == 'a' ||
                x == 'c' ||
                x == 'e' ||
                x == 'f')
            {
                drawSeg2();
            }
            if (
                x == '1' ||
                x == '2' ||
                x == '3' ||
                x == '4' ||
                x == '7' ||
                x == '8' ||
                x == '9' ||
                x == '0' ||
                x == 'a' ||
                x == 'd')
            {
                drawSeg3();
            }
            if (
                x == '1' ||
                x == '3' ||
                x == '4' ||
                x == '5' ||
                x == '6' ||
                x == '7' ||
                x == '8' ||
                x == '9' ||
                x == '0' ||
                x == 'a' ||
                x == 'b' ||
                x == 'd')
            {
                drawSeg4();
            }
            if (
                x == '2' ||
                x == '3' ||
                x == '5' ||
                x == '6' ||
                x == '8' ||
                x == '9' ||
                x == '0' ||
                x == 'b' ||
                x == 'c' ||
                x == 'd' ||
                x == 'e')
            {
                drawSeg5();
            }
            if (
                x == '2' ||
                x == '6' ||
                x == '8' ||
                x == '0' ||
                x == 'a' ||
                x == 'b' ||
                x == 'c' ||
                x == 'd' ||
                x == 'e' ||
                x == 'f')
            {
                drawSeg6();
            }
            if (
                x == '2' ||
                x == '3' ||
                x == '4' ||
                x == '5' ||
                x == '6' ||
                x == '8' ||
                x == '9' ||
                x == 'a' ||
                x == 'b' ||
                x == 'd' ||
                x == 'e' ||
                x == 'f' ||
                x == '-')
            {
                drawSeg7();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = new DateTime(2014,1,1) - DateTime.Now;

            this.days = Math.Floor(timeSpan.TotalDays).ToString(CultureInfo.InvariantCulture).Replace("-", string.Empty);
            this.hours = timeSpan.Hours.ToString(CultureInfo.InvariantCulture).Replace("-", string.Empty);
            this.minutes = timeSpan.Minutes.ToString(CultureInfo.InvariantCulture).Replace("-", string.Empty);
            this.seconds = timeSpan.Seconds.ToString(CultureInfo.InvariantCulture).Replace("-", string.Empty);

            glControl1.Invalidate();
        }

        private void drawDot()
        {
            double size = 0.15;

            GL.Begin(BeginMode.Quads);
            {
                GL.Vertex2(size, size);
                GL.Vertex2(size, -size);
                GL.Vertex2(-size, -size);
                GL.Vertex2(-size, size);
            }
            GL.End();
        }

        private void drawColon()
        {
            double sep = 0.5;
            GL.PushMatrix();
            {
                GL.Translate(0, sep, 0);
                drawDot();
            }
            GL.PopMatrix();
            GL.PushMatrix();
            {
                GL.Translate(0, -sep, 0);
                drawDot();
            }
            GL.PopMatrix();
        }

        private void drawPair(string data)
        {
            double sep = 145*(0.5/91);

            if (data.Length >= 2)
            {
                GL.PushMatrix();
                {
                    GL.Translate(-sep, 0, 0);
                    drawChar(data[0]);
                }
                GL.PopMatrix();
                GL.PushMatrix();
                {
                    GL.Translate(sep, 0, 0);
                    drawChar(data[1]);
                }
                GL.PopMatrix();
            }
            if (data.Length == 1)
            {
                GL.PushMatrix();
                {
                    GL.Translate(-sep, 0, 0);
                    drawChar('0');
                }
                GL.PopMatrix();
                GL.PushMatrix();
                {
                    GL.Translate(sep, 0, 0);
                    drawChar(data[0]);
                }
                GL.PopMatrix();
            }
            if (data.Length == 0)
            {
                GL.PushMatrix();
                {
                    GL.Translate(-sep, 0, 0);
                    drawChar('0');
                }
                GL.PopMatrix();
                GL.PushMatrix();
                {
                    GL.Translate(sep, 0, 0);
                    drawChar('0');
                }
                GL.PopMatrix();
            }
        }

        private void drawDays(string data)
        {
            double sep = 145*(0.5/91);

            if (data.Length < 3)
            {
                data = data.PadLeft(3, '0');
            }


            GL.PushMatrix();
            {
                GL.Translate(-sep, 0, 0);
                drawChar(data[1]);
                GL.Translate(-(sep*2), 0, 0);
                drawChar(data[0]);
            }
            GL.PopMatrix();
            GL.PushMatrix();
            {
                GL.Translate(sep, 0, 0);
                drawChar(data[2]);
                GL.Translate((sep * 2), 0, 0);
                drawChar('d');
            }
            GL.PopMatrix();

        }

    }
}
