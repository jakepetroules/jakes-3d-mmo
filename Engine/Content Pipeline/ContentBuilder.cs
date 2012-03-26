//-----------------------------------------------------------------------------
// ContentBuilder.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
namespace MMO3D.Engine
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using Microsoft.Build.BuildEngine;
    using Petroules.Synteza;

    /// <summary>
    /// This class wraps the MSBuild functionality needed to build XNA Framework
    /// content dynamically at runtime. It creates a temporary MSBuild project
    /// in memory, and adds whatever content files you choose to this project.
    /// It then builds the project, which will create compiled .xnb content files
    /// in a temporary directory. After the build finishes, you can use a regular
    /// ContentManager to load these temporary .xnb files in the usual way.
    /// </summary>
    public class ContentBuilder : IDisposable
    {
        /// <summary>
        /// The version of the XNA framework being used.
        /// </summary>
        private const string XnaVersion = ", Version=3.1.0.0, PublicKeyToken=6d5c3888ef60e27d";

        /// <summary>
        /// The importers or processors that should be loaded.
        /// </summary>
        private static string[] pipelineAssemblies =
        {
            "Microsoft.Xna.Framework.Content.Pipeline.FBXImporter" + XnaVersion,
            "Microsoft.Xna.Framework.Content.Pipeline.XImporter" + XnaVersion,
            "Microsoft.Xna.Framework.Content.Pipeline.TextureImporter" + XnaVersion,
            "Microsoft.Xna.Framework.Content.Pipeline.EffectImporter" + XnaVersion,
        };

        /// <summary>
        /// For generating unique directory names if there is more than one ContentBuilder.
        /// </summary>
        private static int directorySalt;

        /// <summary>
        /// MSBuild object used to dynamically build content. This is an instance of the MSBuild engine.
        /// </summary>
        private Engine buildEngine;

        /// <summary>
        /// MSBuild object used to dynamically build content. This is our MSBuild project that builds the content.
        /// </summary>
        private Project buildProject;

        /// <summary>
        /// MSBuild object used to dynamically build content. This is an error logger to record errors to display to the user.
        /// </summary>
        private ErrorLogger errorLogger;

        /// <summary>
        /// Temporary build directory used by the content build.
        /// </summary>
        private string buildDirectory;

        /// <summary>
        /// Temporary processing directory used by the content build.
        /// </summary>
        private string processDirectory;

        /// <summary>
        /// Temporary base directory used by the content build.
        /// </summary>
        private string baseDirectory;

        /// <summary>
        /// Whether the class has been disposed.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the ContentBuilder class (creates a new content builder).
        /// </summary>
        public ContentBuilder()
        {
            this.CreateTempDirectory();
            this.CreateBuildProject();
        }

        /// <summary>
        /// Finalizes an instance of the ContentBuilder class.
        /// </summary>
        ~ContentBuilder()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the output directory, which will contain the generated .xnb files.
        /// </summary>
        /// <value>See summary.</value>
        public string OutputDirectory
        {
            get { return Path.Combine(this.buildDirectory, "bin\\Content"); }
        }

        /// <summary>
        /// Verifies that the configuration file exists and returns a value indicating whether the program should exit.
        /// </summary>
        /// <param name="args">The command-line arguments passed to the program.</param>
        /// <returns>See summary.</returns>
        public static bool VerifyConfigurationFile(string[] args)
        {
            if (!File.Exists(ContentBuilder.GetConfigurationFileName()))
            {
                if (ContentBuilder.WriteConfigurationFile())
                {
                    Process process = new Process();
                    process.StartInfo.FileName = Application.StartupPath + @"\" + Process.GetCurrentProcess().ProcessName;

                    if (args != null && args.Length > 0)
                    {
                        process.StartInfo.Arguments = string.Join(" ", args);
                    }

                    process.Start();

                    return true;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a new content file to the MSBuild project. The importer and
        /// processor are optional: if you leave the importer null, it will
        /// be autodetected based on the file extension, and if you leave the
        /// processor null, data will be passed through without any processing.
        /// </summary>
        /// <param name="fileName">The filename of the file to add.</param>
        /// <param name="name">The name of the content.</param>
        /// <param name="importer">The importer to use.</param>
        /// <param name="processor">The processor to use.</param>
        public void Add(string fileName, string name, string importer, string processor)
        {
            BuildItem buildItem = this.buildProject.AddNewItem("Compile", fileName);

            buildItem.SetMetadata("Link", Path.GetFileName(fileName));
            buildItem.SetMetadata("Name", name);

            if (!string.IsNullOrEmpty(importer))
            {
                buildItem.SetMetadata("Importer", importer);
            }

            if (!string.IsNullOrEmpty(processor))
            {
                buildItem.SetMetadata("Processor", processor);
            }
        }

        /// <summary>
        /// Removes all content files from the MSBuild project.
        /// </summary>
        public void Clear()
        {
            this.buildProject.RemoveItemsByName("Compile");
        }

        /// <summary>
        /// Builds all the content files which have been added to the project,
        /// dynamically creating .xnb files in the OutputDirectory.
        /// Returns an error message if the build fails.
        /// </summary>
        /// <returns>An error message if the build fails; null normally.</returns>
        public string Build()
        {
            // Clear any previous errors.
            this.errorLogger.Errors.Clear();

            // Build the project.
            if (!this.buildProject.Build())
            {
                string[] errors = new string[this.errorLogger.Errors.Count];
                for (int i = 0; i < errors.Length; i++)
                {
                    errors[i] = this.errorLogger.Errors[i];
                }

                // If the build failed, return an error string.
                return string.Join("\n", errors);
            }

            return null;
        }

        /// <summary>
        /// Disposes the content builder when it is no longer required.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implements the standard .NET IDisposable pattern.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                this.isDisposed = true;

                this.DeleteTempDirectory();
            }
        }

        /// <summary>
        /// Gets the name of the configuration file.
        /// </summary>
        /// <returns>See summary.</returns>
        private static string GetConfigurationFileName()
        {
            return Application.StartupPath + @"\" + Process.GetCurrentProcess().ProcessName + ".exe.config";
        }

        /// <summary>
        /// Writes the configuration file with binding redirects, for build support.
        /// </summary>
        /// <returns>Whether the file was successfully written.</returns>
        private static bool WriteConfigurationFile()
        {
            try
            {
                File.WriteAllText(ContentBuilder.GetConfigurationFileName(), Resources.Configuration);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a temporary MSBuild content project in memory.
        /// </summary>
        private void CreateBuildProject()
        {
            string projectPath = Path.Combine(this.buildDirectory, "content.contentproj");
            string outputPath = Path.Combine(this.buildDirectory, "bin");

            // Create the build engine.
            this.buildEngine = new Engine();

            // Hook up our custom error logger.
            this.errorLogger = new ErrorLogger();

            this.buildEngine.RegisterLogger(this.errorLogger);

            // Create the build project.
            this.buildProject = new Project(this.buildEngine);

            this.buildProject.FullFileName = projectPath;

            this.buildProject.SetProperty("XnaPlatform", "Windows");
            this.buildProject.SetProperty("XnaFrameworkVersion", "v3.1");
            this.buildProject.SetProperty("Configuration", "Release");
            this.buildProject.SetProperty("OutputPath", outputPath);

            // Register any custom importers or processors.
            foreach (string pipelineAssembly in ContentBuilder.pipelineAssemblies)
            {
                this.buildProject.AddNewItem("Reference", pipelineAssembly);
            }

            // Include the standard targets file that defines
            // how to build XNA Framework content.
            this.buildProject.AddNewImport("$(MSBuildExtensionsPath)\\Microsoft\\XNA Game Studio\\v3.1\\Microsoft.Xna.GameStudio.ContentPipeline.targets", null);
        }

        /// <summary>
        /// Creates a temporary directory in which to build content.
        /// </summary>
        private void CreateTempDirectory()
        {
            // Start with a standard base name:
            //  %temp%\WinFormsContentLoading.ContentBuilder
            this.baseDirectory = Path.Combine(Path.GetTempPath(), GetType().FullName);

            // Include our process ID, in case there is more than
            // one copy of the program running at the same time:
            //  %temp%\WinFormsContentLoading.ContentBuilder\<ProcessId>
            int processId = Process.GetCurrentProcess().Id;

            this.processDirectory = Path.Combine(this.baseDirectory, processId.ToString(CultureInfo.InvariantCulture));

            // Include a salt value, in case the program
            // creates more than one ContentBuilder instance:
            //  %temp%\WinFormsContentLoading.ContentBuilder\<ProcessId>\<Salt>
            ContentBuilder.directorySalt++;

            this.buildDirectory = Path.Combine(this.processDirectory, ContentBuilder.directorySalt.ToString(CultureInfo.InvariantCulture));

            // Create our temporary directory.
            Directory.CreateDirectory(this.buildDirectory);

            this.PurgeStaleTempDirectories();
        }

        /// <summary>
        /// Deletes our temporary directory when we are finished with it.
        /// </summary>
        private void DeleteTempDirectory()
        {
            Directory.Delete(this.buildDirectory, true);

            // If there are no other instances of ContentBuilder still using their
            // own temp directories, we can delete the process directory as well.
            if (Directory.GetDirectories(this.processDirectory).Length == 0)
            {
                Directory.Delete(this.processDirectory);

                // If there are no other copies of the program still using their
                // own temp directories, we can delete the base directory as well.
                if (Directory.GetDirectories(this.baseDirectory).Length == 0)
                {
                    Directory.Delete(this.baseDirectory);
                }
            }
        }

        /// <summary>
        /// Ideally, we want to delete our temp directory when we are finished using
        /// it. The DeleteTempDirectory method (called by whichever happens first out
        /// of Dispose or our finalizer) does exactly that. Trouble is, sometimes
        /// these cleanup methods may never execute. For instance if the program
        /// crashes, or is halted using the debugger, we never get a chance to do
        /// our deleting. The next time we start up, this method checks for any temp
        /// directories that were left over by previous runs which failed to shut
        /// down cleanly. This makes sure these orphaned directories will not just
        /// be left lying around forever.
        /// </summary>
        private void PurgeStaleTempDirectories()
        {
            // Check all subdirectories of our base location.
            foreach (string directory in Directory.GetDirectories(this.baseDirectory))
            {
                // The subdirectory name is the ID of the process which created it.
                int processId;

                if (int.TryParse(Path.GetFileName(directory), out processId))
                {
                    try
                    {
                        // Is the creator process still running?
                        Process.GetProcessById(processId);
                    }
                    catch (ArgumentException)
                    {
                        // If the process is gone, we can delete its temp directory.
                        Directory.Delete(directory, true);
                    }
                }
            }
        }
    }
}
