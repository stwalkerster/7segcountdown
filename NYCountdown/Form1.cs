using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace NYCountdown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Color3(Color.Red);


            drawSeg1();
            drawSeg2();
            drawSeg3();
            drawSeg4();
            drawSeg5();
            drawSeg6();
            drawSeg7();

            glControl1.SwapBuffers();
        }

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
                GL.Vertex2(-25*(0.5/91), -77*(0.5/91));
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

        private void glControl1_Resize(object sender, EventArgs e)
        {
         //   GL.Viewport(this.ClientRectangle);
          //  GL.MatrixMode(MatrixMode.Projection);
         //   GL.LoadIdentity();
         //   GL.Ortho(0, glControl1.Width, glControl1.Height, 0, -1, 0);
        }

    }
}
