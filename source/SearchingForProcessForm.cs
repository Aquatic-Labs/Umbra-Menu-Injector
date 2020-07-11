using System;
using System.Windows.Forms;
using System.Diagnostics;
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
            startInfo.Arguments = $"/C cd Data&smi.exe inject -p \"Risk of Rain 2\" -a UmbraMenu/UmbraRoR-v1.2.4.dll -n UmbraRoR -c Loader -m Load";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
    }
}
