using System.Net;

namespace Cyanide
{
    internal static class Program
    {
        static CyanideWindow cyanide = new CyanideWindow();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(cyanide);
        }

        public static void SetupCyanide()
        {
            MessageBox.Show("Cyanide is being setup.", "Cyanide", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Directory.CreateDirectory(cyanide.dir + @"\Projects");
            Directory.CreateDirectory(cyanide.appData + @"\Cyanide");
            Directory.CreateDirectory(cyanide.appData + @"\Cyanide\ProjectTemplate");
            Directory.CreateDirectory(cyanide.appData + @"\Cyanide\Editor");
            Directory.CreateDirectory(cyanide.dir + @"\CyanideDevelopmentEnvironment");
            Directory.CreateDirectory(cyanide.dir + @"\CyanideDevelopmentEnvironment\Blocks");
            Directory.CreateDirectory(cyanide.dir + @"\CyanideDevelopmentEnvironment\Blocks\Categories");
            Directory.CreateDirectory(cyanide.dir + @"\CyanideDevelopmentEnvironment\Blocks\BlockScripts");
            using (WebClient webcli = new())
            {
                try
                {
                    webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/kernel.cpp", cyanide.appData + @"\Cyanide\ProjectTemplate\kernel.cpp");
                    webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/boot.s", cyanide.appData + @"\Cyanide\ProjectTemplate\boot.s");
                    webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/link.ld", cyanide.appData + @"\Cyanide\ProjectTemplate\link.ld");
                    webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/Cyanide/main/CyanideDevelopmentEnvironment/UI.html", cyanide.appData + @"\Cyanide\Editor\UI.html");
                    webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/Cyanide/main/CyanideDevelopmentEnvironment/index.js", cyanide.appData + @"\Cyanide\Editor\index.js");
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
                        cyanide.Close();
                    }
                    if (MessageBox.Show($"ERROR: {ex.Message}", "Cyanide", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/kernel.cpp", cyanide.appData + @"\Cyanide\ProjectTemplate\kernel.cpp");
                        webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/boot.s", cyanide.appData + @"\Cyanide\ProjectTemplate\boot.s");
                        webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/cyanide-template-project/main/link.ld", cyanide.appData + @"\Cyanide\ProjectTemplate\link.ld");
                        webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/Cyanide/main/CyanideDevelopmentEnvironment/UI.html", cyanide.appData + @"\Cyanide\Editor\UI.html");
                        webcli.DownloadFile("https://raw.githubusercontent.com/ThatGuyAstral/Cyanide/main/CyanideDevelopmentEnvironment/index.js", cyanide.appData + @"\Cyanide\Editor\index.js");
                    }
                }
            }
        }
    }
}
