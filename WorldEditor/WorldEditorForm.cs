namespace MMO3D.WorldEditor
{
    using System;
    using System.Windows.Forms;
    using Petroules.Synteza;
    //using PetroulesEnterprises.NetUtilities.MySQL;
    //using MMO3D.DatabaseManagement;
    using MMO3D.Engine;
    using Petroules.Synteza.Windows.Forms;

    /// <summary>
    /// The MMO3D world editor main GUI class.
    /// </summary>
    public partial class WorldEditorForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the WorldEditorForm class.
        /// </summary>
        /// <param name="manager">The database manager.</param>
        /// <exception cref="System.InvalidOperationException">Attempt was made to create multiple instances of this class.</exception>
        public WorldEditorForm()
        {
            // If the instance isn't null, blow up
            if (WorldEditorForm.Instance != null)
            {
                throw new InvalidOperationException(Resources.WorldEditorFormOneInstanceOnly);
            }

            WorldEditorForm.Instance = this;
            //this.DatabaseManager = manager;
            try
            {
                this.InitializeComponent();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            // TODO: Re-enable this
            //this.objectSpawnsEditor.Initialize(manager);
        }

        /// <summary>
        /// Gets a reference to the active instance of <see cref="WorldEditorForm"/>.
        /// </summary>
        /// <value>See summary.</value>
        public static WorldEditorForm Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the database manager used to connect to the database.
        /// </summary>
        /// <value>See summary.</value>
        //public DatabaseManager DatabaseManager
        //{
        //    get;
        //    private set;
        //}
        
        /// <summary>
        /// Gets or sets the cursor of the editor's rendering window.
        /// </summary>
        /// <value>See summary.</value>
        public Cursor RenderingWindowCursor
        {
            get { return this.editorRenderWindow.Cursor; }
            set { this.editorRenderWindow.Cursor = value; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The command-line arguments passed to the program.</param>
        [STAThread]
        public static void Main(string[] args)
        {
            if (ContentBuilder.VerifyConfigurationFile(args))
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //DatabaseManager manager = null; //ProgramLauncher.GetLogOnInfo(args);
            //if (manager != null)
            {
                Application.Run(new WorldEditorForm());
            }
            //else
            {
                // TODO: FIX ALL THIS REENABLE
                //MessageBox.Show("Unable to connect to the database.");
            }
        }

        /// <summary>
        /// Appends text to the program's log window.
        /// </summary>
        /// <param name="text">The text to append.</param>
        public void AppendLogText(string text)
        {
            this.richTextBoxLog.AppendTextSafe(text + Environment.NewLine);
        }
    }
}