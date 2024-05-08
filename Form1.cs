using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace Cyanide
{
    public partial class CyanideWindow : Form
    {
        private bool isDragging;
        private Point lastcurpos;
        private string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public CyanideWindow()
        {
            if (!Directory.Exists(dir + @"\Projects") && !Directory.Exists(appData + @"\Cyanide"))
            {
                MessageBox.Show("Cyanide is being setup.", "Cyanide", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Directory.CreateDirectory(dir + @"\Projects");
                Directory.CreateDirectory(appData + @"\Cyanide");
                Directory.CreateDirectory(appData + @"\Cyanide\ProjectTemplate");
                using (WebClient webcli = new())
                {
                    try
                    {
                        webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/kernel.cpp", appData + @"\Cyanide\ProjectTemplate\kernel.cpp");
                        webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/boot.s", appData + @"\Cyanide\ProjectTemplate\boot.s");
                        webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/link.ld", appData + @"\Cyanide\ProjectTemplate\link.ld");
                        webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/project.cprj", appData + @"\Cyanide\ProjectTemplate\project.cprj");
                        ProcessStartInfo info = new ProcessStartInfo
                        {
                            FileName = @"C:\Windows\System32\cmd.exe",
                            Arguments = $@"git clone https://github.com/google/blockly-samples.git {appData}\Cyanide",
                            WindowStyle = ProcessWindowStyle.Hidden,
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true
                        };
                        using (Process process = Process.Start(info))
                        {
                            string error = process.StandardError.ReadToEnd();

                            process.WaitForExit();
                            if (error != null)
                            {
                                MessageBox.Show("ERROR: " + error, "Cyanide", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show($"ERROR: {ex.Message}", "Cyanide", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error) == DialogResult.Ignore)
                        {
                            MessageBox.Show("Cyanide failed to install a critical component. If you continue, functionality will be limited.");
                        }
                        if (MessageBox.Show($"ERROR: {ex.Message}", "Cyanide", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error) == DialogResult.Abort)
                        {
                            MessageBox.Show("Cyanide failed to install a critical component.");
                            Close();
                        }
                        if (MessageBox.Show($"ERROR: {ex.Message}", "Cyanide", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error) == DialogResult.Retry)
                        {
                            webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/kernel.cpp", appData + @"\Cyanide\ProjectTemplate\kernel.cpp");
                            webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/boot.s", appData + @"\Cyanide\ProjectTemplate\boot.s");
                            webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/link.ld", appData + @"\Cyanide\ProjectTemplate\link.ld");
                            webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/project.cprj", appData + @"\Cyanide\ProjectTemplate\project.cprj");
                        }
                    }
                }
            }
            InitializeComponent();
            BlocklyWindow.Hide();
        }

        private void DragBar_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastcurpos = e.Location;
        }

        private void DragBar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void DragBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = e.X - lastcurpos.X;
                int deltaY = e.Y - lastcurpos.Y;
                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // these don't have proper functionality on purpose
        private void NewProjectBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {


            }
        }

        private async void OpenProjectBtn_Click(object sender, EventArgs e)
        {
            NewProjectBtn.Hide();
            OpenProjectBtn.Hide();
            ProjectsLabel.Hide();
            ProjectList.Hide();
            WindowTitle.Text = "Untitled project - Cyanide";
            this.Controls.Add(BlocklyWindow);
            await BlocklyWindow.EnsureCoreWebView2Async(null);
            BlocklyWindow.CoreWebView2.Navigate($"file:///{appData}/Cyanide/Blockly/index.html");
            BlocklyWindow.Show();
        }
    }
}
