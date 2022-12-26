using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace UmbraInjector
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };

            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/Aquatic-Labs/Umbra-Mod-Menu");
            Process.Start(sInfo);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.CheckForUpdate();
            Program.FilePresent();
        }

        private void UpdateCheck_Tick(object sender, EventArgs e)
        {
            if (Program.upToDate && Program.filePresent)
            {
                InjectButton.Text = $"Inject\nv{Program.currentVersion}";
            }
            else if (!Program.filePresent && !Program.rateLimited)
            {
                InjectButton.Text = $"Download and Inject\nLatest: v{Program.latestVersion}";
            }
            else if (!Program.filePresent && Program.rateLimited)
            {
                InjectButton.Text = $"Download and Inject\n Fallback: v2.0.5";
            }
            else if (Program.updateAvailable && autoUpdateCheck.Checked)
            {
                InjectButton.Text = $"Update and Inject\nLatest: v{Program.latestVersion}";
            }
            else if (Program.filePresent)
            {
                InjectButton.Text = $"Inject\nv{Program.currentVersion}";
            }
        }

        public static bool getProcessFirstTry;
        private void InjectButton_Click(object sender, EventArgs e)
        {
            if (autoUpdateCheck.Checked && Program.updateAvailable)
            {
                Program.DownloadUpdate();

                Process[] getProcess = Process.GetProcessesByName("Risk of Rain 2");
                getProcessFirstTry = getProcess.Length != 0;

                if (!getProcessFirstTry)
                {
                    Form SearchingForProcessForm = new SearchingForProcessForm();
                    SearchingForProcessForm.Show();
                }
                else
                {
                    SearchingForProcessForm.Inject(true);
                }
            }
            else
            {
                if (Program.filePresent)
                {
                    Process[] getProcess = Process.GetProcessesByName("Risk of Rain 2");
                    getProcessFirstTry = getProcess.Length != 0;

                    if (!getProcessFirstTry)
                    {
                        Form SearchingForProcessForm = new SearchingForProcessForm();
                        SearchingForProcessForm.Show();
                    }
                    else
                    {
                        SearchingForProcessForm.Inject(true);
                    }
                }
                else
                {
                    Program.DownloadUpdate();

                    Process[] getProcess = Process.GetProcessesByName("Risk of Rain 2");
                    getProcessFirstTry = getProcess.Length != 0;

                    if (!getProcessFirstTry)
                    {
                        Form SearchingForProcessForm = new SearchingForProcessForm();
                        SearchingForProcessForm.Show();
                    }
                    else
                    {
                        SearchingForProcessForm.Inject(true);
                    }
                }
            }
        }
    }
}
