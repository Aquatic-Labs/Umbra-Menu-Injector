using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Reflection;
using System.Threading;

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
            ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/Acher0ns/Umbra-Mod-Menu");
            Process.Start(sInfo);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.CheckForUpdate();
        }

        private void UpdateCheck_Tick(object sender, EventArgs e)
        {
            if (Program.updateAvailable)
            {
                InjectButton.Text = $"Update and Inject\nLatest: v{Program.latestVersion}";
            }
            else if (Program.upToDate || Program.devBuild)
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
                if (Program.FilePresent())
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
                    using (WebClient client = new WebClient())
                    {
                        using (var data = new WebClient().OpenRead($"https://github.com/Acher0ns/Umbra-Mod-Menu/releases/latest/download/UmbraMenu-v{Program.latestVersion}.zip"))
                        {
                            // This stream cannot be opened with the ZipFile class because CanSeek is false.
                            Program.UnzipFromStream(data, $"UmbraMenu");
                        }
                    }
                    File.Delete($"UmbraMenu/UmbraRoR-v{Program.currentVersion}.dll");
                    Thread.Sleep(10000);

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
