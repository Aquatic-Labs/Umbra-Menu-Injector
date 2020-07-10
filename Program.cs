using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace UmbraInjector
{
    static class Program
    {
        public static bool getProcessFirstTry;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process[] getProcess = Process.GetProcessesByName("Risk of Rain 2");
            getProcessFirstTry = getProcess.Length != 0;

            if (!getProcessFirstTry)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MainForm.Inject(true);
            }
        }
    }
}
