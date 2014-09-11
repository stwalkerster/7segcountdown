// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="Simon Walker">
//   Copyright (C) 2014 Simon Walker
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//   the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in all copies or substantial portions of 
//   the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//   THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//   TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
//   SOFTWARE.
// </copyright>
// <summary>
//   Defines the Form1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NYCountdown
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    using OpenTK.Graphics.OpenGL;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.TargetDateTime = new DateTime(
                Program.year,
                Program.month,
                Program.day,
                Program.hour,
                Program.min,
                Program.sec);

        }

        private string days = "0", hours = "0", minutes = "0", seconds = "0";

        private string millis = "0";

        private bool expired, expiredflash;

        private Color foreColor = Color.White, backColor = Color.Black;

        private bool fullscreen;

        private DateTime targetDateTime;

        public DateTime TargetDateTime
        {
            get
            {
                return this.targetDateTime;
            }

            set
            {
                this.targetDateTime = value;

                this.Text = this.TargetDateTime.ToShortDateString() + @" " + this.TargetDateTime.ToLongTimeString();
            }
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(this.backColor);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Scale(0.25, 0.25, 1);
            const double Halfwidth = 145 * (0.5 / 91);

            if (this.expired && this.expiredflash)
            {
                GL.Color3(this.backColor);
            }
            else
            {
                GL.Color3(this.foreColor);
            }

            if (this.days != "0")
            {
                GL.PushMatrix();
                {
                    GL.Translate(-8 * Halfwidth, 0, 0);
                    drawDays(days);
                }
                GL.PopMatrix();

                GL.PushMatrix();
                {
                    GL.Translate(-2 * Halfwidth, 0, 0);
                    drawPair(hours);
                }
                GL.PopMatrix();

                GL.PushMatrix();
                {
                    GL.Translate(1 * Halfwidth, 0, 0);
                    drawColon();
                }
                GL.PopMatrix();

                GL.PushMatrix();
                {
                    GL.Translate(4 * Halfwidth, 0, 0);
                    drawPair(minutes);
                }
                GL.PopMatrix();

                GL.PushMatrix();
                {
                    GL.Translate(7 * Halfwidth, 0, 0);
                    drawColon();
                }
                GL.PopMatrix();

                GL.PushMatrix();
                {
                    GL.Translate(10 * Halfwidth, 0, 0);
                    drawPair(seconds);
                }
                GL.PopMatrix();
            }
            else
            {
                GL.PushMatrix();
                {
                    GL.Translate(8 * Halfwidth, 0, 0);
                    drawMillis(millis);
                }
                GL.PopMatrix();

                GL.PushMatrix();
                {
                    GL.Translate(-10 * Halfwidth, 0, 0);
                    drawPair(hours);
                }
                GL.PopMatrix();

                GL.PushMatrix();
                {
                    GL.Translate(-7 * Halfwidth, 0, 0);
                    drawColon();
                }
                GL.PopMatrix();

                GL.PushMatrix();
                {
                    GL.Translate(-4 * Halfwidth, 0, 0);
                    drawPair(minutes);
                }
                GL.PopMatrix();

                GL.PushMatrix();
                {
                    GL.Translate(-1 * Halfwidth, 0, 0);
                    drawColon();
                }
                GL.PopMatrix();

                GL.PushMatrix();
                {
                    GL.Translate(2 * Halfwidth, 0, 0);
                    drawPair(seconds);
                }
                GL.PopMatrix();
            }

            this.glControl1.SwapBuffers();
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
            int fadeTime = 255;
            TimeSpan timeSpan = this.TargetDateTime - DateTime.Now;

            this.expiredflash = Math.Abs(timeSpan.Milliseconds) < 500;

            if (this.expired)
            {
                this.days = "0";
                this.hours = "0";
                this.minutes = "0";
                this.seconds = "0";
                this.millis = "0";
            }
            else
            {
                this.expired = timeSpan.Milliseconds < 0;

                this.days = Math.Floor(timeSpan.TotalDays).ToString(CultureInfo.InvariantCulture).Replace("-", string.Empty);
                this.hours = timeSpan.Hours.ToString(CultureInfo.InvariantCulture).Replace("-", string.Empty);
                this.minutes = timeSpan.Minutes.ToString(CultureInfo.InvariantCulture).Replace("-", string.Empty);
                this.seconds = timeSpan.Seconds.ToString(CultureInfo.InvariantCulture).Replace("-", string.Empty);
                this.millis = timeSpan.Milliseconds.ToString(CultureInfo.InvariantCulture).Replace("-", string.Empty); 
            }


            if (timeSpan.TotalSeconds > 0)
            {
                if (Math.Abs(timeSpan.TotalSeconds) < fadeTime)
                {
                    this.backColor = Color.FromArgb(0, 255 - (int)Math.Abs(timeSpan.TotalSeconds), 0, 0);
                }
                else
                {
                    this.backColor = Color.Black;
                }

                this.foreColor = Color.White;
            }
            else
            {
                if (Math.Abs(timeSpan.TotalSeconds) < fadeTime)
                {
                    this.backColor = Color.FromArgb(0, 255 - (int)Math.Abs(timeSpan.TotalSeconds), 0, 0);
                }

                var d = Math.Abs(timeSpan.TotalSeconds) - fadeTime;
                if (d > 0)
                {
                    var red = 255 - (int)Math.Min(d, 255);

                    this.foreColor = Color.FromArgb(0, red, red, red);
                }
            }

            this.glControl1.Invalidate();
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

        private void drawMillis(string data)
        {
            double sep = 145 * (0.5 / 91);

            if (data.Length < 3)
            {
                data = data.PadLeft(3, '0');
            }

            GL.PushMatrix();
            {
                GL.Translate(-sep, 0, 0);
                drawChar(data[0]); //1
                GL.Translate(-(sep * 2), 0, 0);
                drawColon(); //0
            }
            GL.PopMatrix();
            GL.PushMatrix();
            {
                GL.Translate(sep, 0, 0);
                drawChar(data[1]); // 2
                GL.Translate((sep * 2), 0, 0);
                drawChar(data[2]); // 3
            }
            GL.PopMatrix();

        }

        private void glControl1_DoubleClick(object sender, EventArgs e)
        {
            if (!this.fullscreen)
            {
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.fullscreen = true;
            }
            else
            {
                this.TopMost = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.fullscreen = false;
            }
        }

        private void glControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var val = 5;

            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                if (e.Control)
                {
                    val = 1;
                }

                if (e.KeyCode == Keys.Left)
                {
                    val *= -1;
                }

                if (e.Shift)
                {
                    this.TargetDateTime = this.TargetDateTime.Subtract(new TimeSpan(0, 0, val, 0));
                }
                else if (e.Alt)
                {
                    this.TargetDateTime = this.TargetDateTime.Subtract(new TimeSpan(0, val, 0, 0));
                }
                else
                {
                    this.TargetDateTime = this.TargetDateTime.Subtract(new TimeSpan(0, 0, 0, val));
                }

                this.expired = false;
                this.expiredflash = false;
            }
        }



    }
}
