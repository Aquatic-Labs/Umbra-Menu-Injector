using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace UmbraInjector
{
    public partial class SearchingForProcessForm : Form
    {
        public SearchingForProcessForm()
        {
            InitializeComponent();
            isProcessRunning.Start();
        }

        private void IsProcessRunning_Tick(object sender, EventArgs e)
        {
            Process[] getProcess = Process.GetProcessesByName("Risk of Rain 2");
            if (getProcess.Length != 0)
            {
                isProcessRunning.Stop();
                Inject(false);
                Application.Exit();
            }
        }

        public static string GetDLLName()
        {
            string dllName = "";
            var currentFiles = Directory.GetFiles("Data/UmbraMenu/");
            foreach (string fileName in currentFiles)
            {
                string temp = fileName.Replace("Data/UmbraMenu/", "");
                if (temp.EndsWith(".dll") && temp.Contains("Umbra"))
                {
                    dllName = temp;
                }
            }
            return dllName;
        }

        public static void Inject(bool alreadyOpen)
        {
            if (!alreadyOpen)
            {
                Thread.Sleep(15000);
            }
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C cd Data&smi.exe inject -p \"Risk of Rain 2\" -a UmbraMenu/{GetDLLName()} -n UmbraRoR -c Loader -m Load";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
    }
}
