using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace Cyanide
{
    public partial class CyanideWindow : Form
    {
        private bool isDragging;
        private Point lastcurpos;

        public CyanideWindow()
        {
            if (!Directory.Exists(Program.dir + @"\Projects") && !Directory.Exists(Program.appData))
            {
                Program.SetupCyanide();
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
            BlocklyWindow.CoreWebView2.Navigate(Program.appData + @"\Cyanide\Editor\UI.html");
            BlocklyWindow.Show();
        }
    }
}
