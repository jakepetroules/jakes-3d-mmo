namespace Petroules.Synteza
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Provides properties to access assembly information.
    /// </summary>
    public sealed class AssemblyProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyProperties"/> class.
        /// </summary>
        /// <param name="assembly">The assembly whose properties will be read.</param>
        public AssemblyProperties(Assembly assembly)
        {
            this.Assembly = assembly;
        }

        /// <summary>
        /// Gets company name information.
        /// </summary>
        public static string CurrentCompanyName
        {
            get { return AssemblyProperties.GetAssemblyProperty<AssemblyCompanyAttribute>().Company; }
        }

        /// <summary>
        /// Gets copyright information.
        /// </summary>
        public static string CurrentCopyright
        {
            get { return AssemblyProperties.GetAssemblyProperty<AssemblyCopyrightAttribute>().Copyright; }
        }

        /// <summary>
        /// Gets assembly description information.
        /// </summary>
        public static string CurrentDescription
        {
            get { return AssemblyProperties.GetAssemblyProperty<AssemblyDescriptionAttribute>().Description; }
        }

        /// <summary>
        /// Gets the Win32 file version resource name.
        /// </summary>
        public static Version CurrentFileVersion
        {
            get { return new Version(AssemblyProperties.GetAssemblyProperty<AssemblyFileVersionAttribute>().Version); }
        }

        /// <summary>
        /// Gets product name information.
        /// </summary>
        public static string CurrentProductName
        {
            get { return AssemblyProperties.GetAssemblyProperty<AssemblyProductAttribute>().Product; }
        }

        /// <summary>
        /// Gets assembly title information.
        /// </summary>
        public static string CurrentTitle
        {
            get { return new AssemblyProperties(Assembly.GetExecutingAssembly()).Title; }
        }

        /// <summary>
        /// Gets the version number of the attributed assembly.
        /// </summary>
        public static Version CurrentVersion
        {
            get { return Assembly.GetCallingAssembly().GetName().Version; }
        }

        /// <summary>
        /// Gets the assembly whose properties are being read.
        /// </summary>
        public Assembly Assembly
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets company name information.
        /// </summary>
        public string CompanyName
        {
            get { return AssemblyProperties.GetAssemblyProperty<AssemblyCompanyAttribute>(this.Assembly).Company; }
        }

        /// <summary>
        /// Gets copyright information.
        /// </summary>
        public string Copyright
        {
            get { return AssemblyProperties.GetAssemblyProperty<AssemblyCopyrightAttribute>(this.Assembly).Copyright; }
        }

        /// <summary>
        /// Gets assembly description information.
        /// </summary>
        public string Description
        {
            get { return AssemblyProperties.GetAssemblyProperty<AssemblyDescriptionAttribute>(this.Assembly).Description; }
        }

        /// <summary>
        /// Gets the Win32 file version resource name.
        /// </summary>
        public Version FileVersion
        {
            get { return new Version(AssemblyProperties.GetAssemblyProperty<AssemblyFileVersionAttribute>(this.Assembly).Version); }
        }

        /// <summary>
        /// Gets product name information.
        /// </summary>
        public string ProductName
        {
            get { return AssemblyProperties.GetAssemblyProperty<AssemblyProductAttribute>(this.Assembly).Product; }
        }

        /// <summary>
        /// Gets assembly title information.
        /// </summary>
        public string Title
        {
            get
            {
                var titleAttr = AssemblyProperties.GetAssemblyProperty<AssemblyTitleAttribute>(this.Assembly);
                if (titleAttr != null && !string.IsNullOrEmpty(titleAttr.Title))
                {
                    return titleAttr.Title;
                }

                return Path.GetFileNameWithoutExtension(this.Assembly.CodeBase);
            }
        }

        /// <summary>
        /// Gets the version number of the attributed assembly.
        /// </summary>
        public Version Version
        {
            get { return this.Assembly.GetName().Version; }
        }

        /// <summary>
        /// Gets an assembly property specified by the <typeparamref name="T"/> attribute type.
        /// </summary>
        /// <typeparam name="T">The type of attribute to obtain.</typeparam>
        /// <returns>See summary.</returns>
        private static T GetAssemblyProperty<T>() where T : Attribute
        {
            return AssemblyProperties.GetAssemblyProperty<T>(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Gets an assembly property specified by the <typeparamref name="T"/> attribute type.
        /// </summary>
        /// <param name="assembly">The assembly whose properties will be read.</param>
        /// <typeparam name="T">The type of attribute to obtain.</typeparam>
        /// <returns>See summary.</returns>
        private static T GetAssemblyProperty<T>(Assembly assembly) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(assembly, typeof(T));
        }
    }
}
