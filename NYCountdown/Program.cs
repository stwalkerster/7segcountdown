using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NYCountdown
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 6)
            {
                year = int.Parse(args[0]);
                month = int.Parse(args[1]);
                day = int.Parse(args[2]);
                hour = int.Parse(args[3]);
                min = int.Parse(args[4]);
                sec = int.Parse(args[5]);
            }
            else
            {
                year = DateTime.Now.Year + 1;
                month = day = 1;
                hour = min = sec = 0;
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static int day, month, year, hour, min, sec;
    }
}
