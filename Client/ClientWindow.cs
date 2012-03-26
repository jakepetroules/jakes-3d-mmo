namespace MMO3D.Client
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using OpenTK.Graphics;
    using MMO3D.Engine;
    using OpenTK;

    /// <summary>
    /// The MMO3D Client main GUI form.
    /// </summary>
    public partial class ClientWindow : Form
    {
        /// <summary>
        /// Initializes a new instance of the ClientWindow class.
        /// </summary>
        public ClientWindow()
        {
            this.InitializeComponent();
            Application.Idle += delegate
            {
                if (this.renderWindow.ShouldDraw)
                {
                    this.renderWindow.Invalidate();
                }
            };
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            try
            {
                if (ContentBuilder.VerifyConfigurationFile(null))
                {
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ClientWindow());
            }
            catch (Exception e)
            {
                File.AppendAllText(Application.StartupPath + "\\log.txt", e.ToString() + System.Environment.NewLine + System.Environment.NewLine);
                MessageBox.Show("MMO3D has encountered a problem and will now close. Please check the error log file for details.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft ? MessageBoxOptions.RtlReading : 0);
            }
        }

        /// <summary>
        /// Cleans up when the program is closed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void ClientWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.renderWindow.Network.Disconnect();

            try
            {
                DisplayDevice.Default.RestoreResolution();
            }
            catch (GraphicsModeException)
            {
            }
        }
    }
}
