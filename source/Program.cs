using System;
using System.IO;
using Octokit;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace UmbraInjector
{
    static class Program
    {

        public static bool updateAvailable;
        public static string latestVersion;
        public static string currentVersion;
        public static bool devBuild;
        public static bool upToDate = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new MainForm());
        }

        public static void UnzipFromStream(Stream zipStream, string outFolder)
        {
            using (var zipInputStream = new ZipInputStream(zipStream))
            {
                while (zipInputStream.GetNextEntry() is ZipEntry zipEntry)
                {
                    var entryFileName = zipEntry.Name;
                    var buffer = new byte[4096];

                    var fullZipToPath = Path.Combine(outFolder, entryFileName);
                    var directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                        Directory.CreateDirectory(directoryName);

                    if (Path.GetFileName(fullZipToPath).Length == 0)
                    {
                        continue;
                    }

                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipInputStream, streamWriter, buffer);
                    }
                }
            }
        }

        public static bool FilePresent()
        {
            CheckForUpdate();
            bool filePresent;
            if (currentVersion != null)
            {
                filePresent = true;
            }
            else
            {
                filePresent = false;
            }
            return filePresent;
        }

        public static void DeleteFile()
        {
            if (FilePresent())
            {
                File.Delete($"Data/UmbraMenu/{SearchingForProcessForm.GetDLLName()}");
            }
            else
            {
                return;
            }
            
        }

        public static void DownloadUpdate()
        {
            try
            {
                DeleteFile();
                using (WebClient client = new WebClient())
                {
                    using (var data = new WebClient().OpenRead($"https://github.com/Acher0ns/Umbra-Mod-Menu/releases/latest/download/UmbraMenu-v{Program.latestVersion}.zip"))
                    {
                        // This stream cannot be opened with the ZipFile class because CanSeek is false.
                        Program.UnzipFromStream(data, $"Data/UmbraMenu");
                    }
                }
                Thread.Sleep(1000);
            }
            catch (UnauthorizedAccessException)
            {
                string path = $"Data/UmbraMenu/{SearchingForProcessForm.GetDLLName()}";
                FileAttributes attributes = File.GetAttributes(path);
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes &= ~FileAttributes.ReadOnly;
                    File.SetAttributes(path, attributes);

                    DeleteFile();
                    using (WebClient client = new WebClient())
                    {
                        using (var data = new WebClient().OpenRead($"https://github.com/Acher0ns/Umbra-Mod-Menu/releases/latest/download/UmbraMenu-v{Program.latestVersion}.zip"))
                        {
                            // This stream cannot be opened with the ZipFile class because CanSeek is false.
                            Program.UnzipFromStream(data, $"Data/UmbraMenu");
                        }
                    }
                    Thread.Sleep(1000);
                }
                else
                {
                    throw;
                }
            }
        }

        public static async void CheckForUpdate()
        {
            var client = new GitHubClient(new ProductHeaderValue("UmbraUpdateCheck"));
            var releases = await client.Repository.Release.GetAll("Acher0ns", "Umbra-Mod-Menu").ConfigureAwait(false);
            var latest = releases[0];
            latestVersion = latest.TagName;

            var currentFiles = Directory.GetFiles("Data/UmbraMenu/");
            foreach (string fileName in currentFiles)
            {
                if (fileName.StartsWith("Data/UmbraMenu/UmbraRoR-v"))
                {
                    string temp = fileName.Replace("Data/UmbraMenu/UmbraRoR-v", "");
                    currentVersion = temp.Replace(".dll", "");
                }
            }

            if (currentVersion != null)
            {
                string[] versionSplit = currentVersion.Split('.');
                string[] latestVersionSplit = latestVersion.Split('.');

                for (int i = 0; i < versionSplit.Length; i++)
                {
                    int versionNumber = int.Parse(versionSplit[i]);
                    int latestVersionNumber = int.Parse(latestVersionSplit[i]);
                    if (versionNumber < latestVersionNumber)
                    {
                        upToDate = false;
                        devBuild = false;
                        updateAvailable = true;
                        break;
                    }
                    else if (versionNumber > latestVersionNumber)
                    {
                        upToDate = false;
                        devBuild = true;
                        updateAvailable = false;
                        break;
                    }
                }
            }
            else
            {
                string[] versionSplit = "0.0.0".Split('.');
                string[] latestVersionSplit = latestVersion.Split('.');

                for (int i = 0; i < versionSplit.Length; i++)
                {
                    int versionNumber = int.Parse(versionSplit[i]);
                    int latestVersionNumber = int.Parse(latestVersionSplit[i]);
                    if (versionNumber < latestVersionNumber)
                    {
                        upToDate = false;
                        devBuild = false;
                        updateAvailable = true;
                        break;
                    }
                    else if (versionNumber > latestVersionNumber)
                    {
                        upToDate = false;
                        devBuild = true;
                        updateAvailable = false;
                        break;
                    }
                }
            }
        }
    }
}
