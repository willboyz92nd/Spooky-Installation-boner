using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Net.NetworkInformation;



namespace LizzyInstaller
{


    public partial class MainForm : Form
    {
        ///  Console.WriteLine(LatestResponse);
        
          

        String CurrentLocation = System.IO.Directory.GetCurrentDirectory();
        FolderBrowserDialog PathPicker = new FolderBrowserDialog();

        /// Custom Property garbage here, for simplicitys Sake.
        /// Edit Accordingly to your pleasure.
        String RepositoryName = "andromeda-engine";
        String UserName = "nebulazorua";
        String PathToMainExe = "Funkin.exe";


        /// asd
        String ExportedBuilds = "";
        String ExportedBetaBuilds = "";
        bool isConnected = new Ping().Send("8.8.8.8").Status == IPStatus.Success;


        HttpClient DownloadListner = new HttpClient();
        WebClient FileDownloader = new WebClient();
        HttpResponseMessage Builds = null;

        String TagId = "";
        String LatestTag = "";
        String LatestBetaTag = "";
        bool InstigateUpdate = false;
        bool RanAlready = false;
        bool IsLookingForBetas = false;
        bool HideCheckBox = false;
        


        public MainForm()
        {

            ///string[] args = Environment.GetCommandLineArgs();
            ///if (args.Length > 1) {
            ///   MessageBox.Show(args[1]);
            ///}
            ///






            DownloadListner.BaseAddress = new Uri("https://api.github.com");
            DownloadListner.DefaultRequestHeaders.Add("user-agent", "C# Launcher");
            DownloadListner.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            if (isConnected) {

                Builds = DownloadListner.GetAsync("/repos/" + UserName + "/" + RepositoryName + "/releases").Result;



                Console.WriteLine(Builds.Content.ReadAsStringAsync().Result);
                ExportedBuilds = Builds.Content.ReadAsStringAsync().Result;

                bool DoBetasExist = ExportedBuilds.IndexOf("\"prerelease\":true") >= -0;
                bool DoReleasesExist = ExportedBuilds.IndexOf("\"prerelease\":false") >= -0;

                ///Console.WriteLine(DoBetasExist + "\n" + DoReleasesExist);

                if (DoBetasExist == false || DoReleasesExist == false)
                {
                    HideCheckBox = true;
                    IsLookingForBetas = true;
                }


                if (DoBetasExist || IsLookingForBetas)
                {
                    ExportedBetaBuilds = ExportedBuilds.Substring(ExportedBuilds.IndexOf("browser_download_url") + 23 + 19);
                    ExportedBetaBuilds = ExportedBetaBuilds.Substring(0, ExportedBetaBuilds.IndexOf(".zip") + 4);
                    ExportedBetaBuilds = "https://github.com/" + ExportedBetaBuilds;

                    LatestBetaTag = Builds.Content.ReadAsStringAsync().Result;
                    LatestBetaTag = LatestBetaTag.Substring(LatestBetaTag.IndexOf("tag_name") + 11);
                    LatestBetaTag = LatestBetaTag.Substring(0, LatestBetaTag.IndexOf(",") - 1);
                }


                if (DoReleasesExist)
                {

                    int Start = ExportedBuilds.IndexOf("\"prerelease\":false");

                    ExportedBuilds = ExportedBuilds.Substring(ExportedBuilds.IndexOf("browser_download_url", Start) + 23 + 19);
                    ExportedBuilds = ExportedBuilds.Substring(0, ExportedBuilds.IndexOf(".zip") + 4);
                    ExportedBuilds = "https://github.com/" + ExportedBuilds;

                    LatestTag = Builds.Content.ReadAsStringAsync().Result;
                    LatestTag = LatestTag.Substring(LatestTag.IndexOf("tag_name", Start - 100) + 11);
                    LatestTag = LatestTag.Substring(0, LatestTag.IndexOf(",") - 1);

                }

                Console.WriteLine(ExportedBetaBuilds + "\n" + ExportedBuilds);

            }


            /// Check if the installer has ran, and if it has, is the game outdated


            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" + UserName + RepositoryName + ".knot"))
            {
                string Data = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" + UserName + RepositoryName + ".knot");
                char[] Splitter = { '|' };
                var SpliceData = Data.Split(Splitter);

                TagId = SpliceData[0];
                CurrentLocation = SpliceData[1];
                IsLookingForBetas = SpliceData[2] == "True";



                if (Directory.Exists(CurrentLocation) && Directory.Exists(CurrentLocation + "/" + RepositoryName) && File.Exists(CurrentLocation + "/" + RepositoryName + "/" + PathToMainExe))
                {
                    if (IsLookingForBetas)
                    {
                        if (TagId != LatestBetaTag && isConnected)
                        {
                            /// brute force update uwu owo mmm so musk
                            Console.WriteLine("Update");

                            DialogResult WillWeUpdate = DialogResult.OK;

                            if (LatestBetaTag == LatestTag)
                            {
                                WillWeUpdate = MessageBox.Show("A new release version is detected, would you like to update ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            else
                            {
                                WillWeUpdate = MessageBox.Show("A new pre-release version is detected, would you like to update ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            }

                            if (WillWeUpdate == DialogResult.Yes)
                            {
                                InstigateUpdate = true;
                            }
                            else
                            {


                                var PrcInfo = new System.Diagnostics.ProcessStartInfo(@CurrentLocation + "/" + RepositoryName + "/" + PathToMainExe);
                                PrcInfo.CreateNoWindow = true;
                                PrcInfo.WorkingDirectory = @CurrentLocation + "/" + RepositoryName;
                                PrcInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                RanAlready = true;
                                System.Diagnostics.Process RealPRC = new System.Diagnostics.Process();
                                RealPRC.StartInfo = PrcInfo;
                                RealPRC.Start();
                            }

                        }

                        else
                        {

                            var PrcInfo = new System.Diagnostics.ProcessStartInfo(@CurrentLocation + "/" + RepositoryName + "/" + PathToMainExe);
                            PrcInfo.CreateNoWindow = true;
                            PrcInfo.WorkingDirectory = @CurrentLocation + "/" + RepositoryName;
                            PrcInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                            RanAlready = true;
                            System.Diagnostics.Process RealPRC = new System.Diagnostics.Process();
                            RealPRC.StartInfo = PrcInfo;
                            RealPRC.Start();

                        }
                    }
                    else
                    {
                        if (TagId != LatestTag && isConnected)
                        {
                            /// brute force update uwu owo mmm so musk
                            Console.WriteLine("Update");

                            var WillWeUpdate = MessageBox.Show("A new release version is detected, would you like to update ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (WillWeUpdate == DialogResult.Yes)
                            {
                                InstigateUpdate = true;
                            }
                            else
                            {

                                var PrcInfo = new System.Diagnostics.ProcessStartInfo(@CurrentLocation + "/" + RepositoryName + "/" + PathToMainExe);
                                PrcInfo.CreateNoWindow = true;
                                PrcInfo.WorkingDirectory = @CurrentLocation + "/" + RepositoryName;
                                PrcInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                RanAlready = true;
                                System.Diagnostics.Process.Start(PrcInfo);
                            }

                        }

                        else
                        {

                            var PrcInfo = new System.Diagnostics.ProcessStartInfo(@CurrentLocation + "/" + RepositoryName + "/" + PathToMainExe);
                            PrcInfo.CreateNoWindow = true;
                            PrcInfo.WorkingDirectory = @CurrentLocation + "/" + RepositoryName;
                            PrcInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                            RanAlready = true;
                            System.Diagnostics.Process.Start(PrcInfo);

                        }
                    }

                }
                else
                {
                    Console.WriteLine("asd");
                    if (isConnected == false)
                    {
                        RanAlready = true;
                        MessageBox.Show("The installer can't run without an internet connection, Silly! While you're here, Why not grab a beverage, you'll need one during the playthrough~", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
            else {
                Console.WriteLine("asd");
                if (isConnected == false)
                {
                    RanAlready = true;
                    MessageBox.Show("The installer can't run without an internet connection silly ? While you're here, Why not grab a beverage, you'll need one during the playthrough~", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }



            /// Knotula 


            InitializeComponent();

            if (CurrentLocation.Length < 4)
            {
                PathBox.Text = CurrentLocation + RepositoryName;
            }
            else
            {
                PathBox.Text = CurrentLocation + "\\" + RepositoryName;
            }

            if (BetaBox.Visible) {
                BetaBox.Checked = IsLookingForBetas;
            }


            BetaBox.Visible =! HideCheckBox;
            PathPicker.SelectedPath = CurrentLocation;


        }




        private void PathSetButton_Click(object sender, EventArgs e)
        {
            DialogResult NewPath = PathPicker.ShowDialog();

            if (NewPath == DialogResult.OK || !string.IsNullOrWhiteSpace(PathPicker.SelectedPath)){
                CurrentLocation = PathPicker.SelectedPath.ToString();
                if (CurrentLocation.Length < 4)
                {
                    PathBox.Text = CurrentLocation + RepositoryName;
                }
                else
                {
                    PathBox.Text = CurrentLocation  + "\\" + RepositoryName;
                }

                PathPicker.SelectedPath = CurrentLocation;

            };


        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            DownloadingLabel.Visible = false;
            DownloadingLabel.Left = 1000;
            ExtractingLabel.Visible = true;


            ZipFile.ExtractToDirectory(@CurrentLocation + "/" + RepositoryName + ".zip", CurrentLocation + "/" + RepositoryName);


            var PrcInfo = new System.Diagnostics.ProcessStartInfo(@CurrentLocation + "/" + RepositoryName + "/" + PathToMainExe);
            PrcInfo.CreateNoWindow = true;
            PrcInfo.WorkingDirectory = @CurrentLocation + "/" + RepositoryName;
            PrcInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            RanAlready = true;
            System.Diagnostics.Process RealPRC = new System.Diagnostics.Process();
            RealPRC.StartInfo = PrcInfo;
            RealPRC.Start();    

            System.Windows.Forms.Application.Exit();
        }


        private void InstallButton_Click(object sender, EventArgs e)
        {

            InstallButton.Enabled = false;
            PathSetButton.Enabled = false;
            BetaBox.Enabled = false;

            if (InstigateUpdate) {
                System.IO.DirectoryInfo TargetToPurge = new DirectoryInfo(@CurrentLocation + "/" + RepositoryName);

                foreach (FileInfo file in TargetToPurge.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in TargetToPurge.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

            if (Directory.Exists(@CurrentLocation + "/" + RepositoryName) && InstigateUpdate == false)
            {

                var Confirmation = MessageBox.Show("The result folder is going to be wiped before installation, are you sure you want to continue ?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

                if (Confirmation == DialogResult.Yes)
                {

                    System.IO.DirectoryInfo TargetToPurge = new DirectoryInfo(@CurrentLocation + "/" + RepositoryName);

                    foreach (FileInfo file in TargetToPurge.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in TargetToPurge.GetDirectories())
                    {
                        dir.Delete(true);
                    }

                }
                else {

                    InstallButton.Enabled = true;
                    PathSetButton.Enabled = true;
                    BetaBox.Enabled = true;
                    return;

                }

            }

            if (IsLookingForBetas) {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" + UserName + RepositoryName + ".knot", LatestBetaTag + "|" + CurrentLocation + "|" + IsLookingForBetas);
            } else
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/" + UserName + RepositoryName + ".knot", LatestTag + "|" + CurrentLocation + "|" + IsLookingForBetas);
            }

            DownloadingLabel.Visible = true;

            Console.WriteLine(ExportedBetaBuilds);



            if (Builds.IsSuccessStatusCode)
            {
                if (IsLookingForBetas)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    FileDownloader.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    FileDownloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    FileDownloader.Headers[HttpRequestHeader.UserAgent] = "API Testing 3.0";
                    /// Issue
                    FileDownloader.Headers[HttpRequestHeader.Accept] = "application/json";
                    Console.WriteLine(@CurrentLocation + "/" + RepositoryName + ".zip");
                    FileDownloader.DownloadFileAsync(new Uri(ExportedBetaBuilds), @CurrentLocation + "/" + RepositoryName + ".zip");
                }
                else {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    FileDownloader.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    FileDownloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    FileDownloader.Headers[HttpRequestHeader.UserAgent] = "API Testing 3.0";
                    /// Issue
                    FileDownloader.Headers[HttpRequestHeader.Accept] = "application/json";
                    Console.WriteLine(@CurrentLocation + "/" + RepositoryName + ".zip");
                    FileDownloader.DownloadFileAsync(new Uri(ExportedBuilds), @CurrentLocation + "/" + RepositoryName + ".zip");

                }
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (RanAlready) {
                System.Windows.Forms.Application.Exit();
            }

            if (InstigateUpdate)
            {
                InstallButton_Click(null, null);
            }
        }

        private void BetaBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BetaBox.Checked == true) {
                DialogResult Confirmation = MessageBox.Show("Are you sure you want to allow for beta builds ? These are generally unstable and are marked as beta for a reason.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (Confirmation == DialogResult.Yes)
                {
                    IsLookingForBetas = BetaBox.Checked;
                }
                else {
                    BetaBox.Checked = false;
                    IsLookingForBetas = false;
                    return;
                }
            }

            IsLookingForBetas = BetaBox.Checked;

        }
    }
}
