namespace Petroules.Synteza
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// Contains methods to help with localization.
    /// </summary>
    public static class LocalizationHelper
    {
        /// <summary>
        /// Tries to switch to the specified language, returning false on failure.
        /// </summary>
        /// <param name="control">The control to localize.</param>
        /// <param name="cultureCode">The culture code to set - for example, en-US or de-DE.</param>
        /// <returns>Whether the localization was actually switched.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="control"/> is <c>null</c>.</exception>
        public static bool SwitchLocalization(Control control, string cultureCode)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            ComponentResourceManager resources = new ComponentResourceManager(control.GetType());
            if (LocalizationHelper.SetCulture(resources, cultureCode))
            {
                LocalizationHelper.ApplyResources(resources, control);
                control.PerformLayout();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to set the specified language on the current thread, returning
        /// false if an invalid or unsupported culture was specified.
        /// </summary>
        /// <param name="form">The <see cref="Form"/> used to create the <see cref="ComponentResourceManager"/> used to apply the resources.</param>
        /// <param name="culture">The culture string to set.</param>
        /// <returns>Whether the culture was set successfully.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="form"/> is <c>null</c> -or- <paramref name="culture"/> is <c>null</c>.</exception>
        public static bool SetCulture(Form form, string culture)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            return LocalizationHelper.SetCulture(new ComponentResourceManager(form.GetType()), culture);
        }

        /// <summary>
        /// Tries to set the specified language on the current thread, returning
        /// false if an invalid or unsupported culture was specified.
        /// </summary>
        /// <param name="resources">The <see cref="ComponentResourceManager"/> used to apply the resources.</param>
        /// <param name="culture">The culture string to set.</param>
        /// <returns>Whether the culture was set successfully.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="resources"/> is <c>null</c> -or- <paramref name="culture"/> is <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "This is specifically designed for Windows Forms; the base class would not be appropriate.")]
        public static bool SetCulture(ComponentResourceManager resources, string culture)
        {
            if (resources == null)
            {
                throw new ArgumentNullException("resources");
            }

            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            try
            {
                CultureInfo info = CultureInfo.GetCultureInfo(culture);
                if (resources.GetResourceSet(info, true, false) != null)
                {
                    Thread.CurrentThread.CurrentCulture = info;
                    Thread.CurrentThread.CurrentUICulture = info;

                    return true;
                }
            }
            catch (ArgumentException)
            {
                // Invalid or non-existant culture
            }

            return false;
        }

        /// <summary>
        /// Applies resources to the specified control and all of its children
        /// (be they controls themselves, or various toolstrip objects).
        /// </summary>
        /// <param name="resources">The <see cref="ComponentResourceManager"/> used to apply the resources.</param>
        /// <param name="control">The <see cref="Control"/> to apply the resources to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="resources"/> is <c>null</c> -or- <paramref name="control"/> is <c>null</c>.</exception>
        public static void ApplyResources(ComponentResourceManager resources, Control control)
        {
            if (resources == null)
            {
                throw new ArgumentNullException("resources");
            }

            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            // Ignore Windows Media Player controls so they don't restart the media being played.
            if (control.GetType().ToString() == "AxWMPLib.AxWindowsMediaPlayer")
            {
                return;
            }

            for (int i = 0; i < control.Controls.Count; i++)
            {
                LocalizationHelper.ApplyResources(resources, control.Controls[i]);
            }

            ToolStrip ts = control as ToolStrip;
            if (ts != null)
            {
                for (int i = 0; i < ts.Items.Count; i++)
                {
                    ToolStripDropDownItem item = ts.Items[i] as ToolStripDropDownItem;
                    if (item != null)
                    {
                        LocalizationHelper.ApplyResources(resources, item);
                    }
                }
            }

            resources.ApplyResources(control, (control.GetType() == typeof(Form) ? "$" : string.Empty) + control.Name);
        }

        /// <summary>
        /// Applies resources to the specified <see cref="ToolStripDropDownItem"/>
        /// and its child drop down items.
        /// </summary>
        /// <param name="resources">The <see cref="ComponentResourceManager"/> used to apply the resources.</param>
        /// <param name="dropDownItem">The <see cref="ToolStripDropDownItem"/> to apply the resources to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="resources"/> is <c>null</c> -or- <paramref name="dropDownItem"/> is <c>null</c>.</exception>
        public static void ApplyResources(ComponentResourceManager resources, ToolStripDropDownItem dropDownItem)
        {
            if (resources == null)
            {
                throw new ArgumentNullException("resources");
            }

            if (dropDownItem == null)
            {
                throw new ArgumentNullException("dropDownItem");
            }

            for (int i = 0; i < dropDownItem.DropDownItems.Count; i++)
            {
                LocalizationHelper.ApplyResources(resources, dropDownItem.DropDownItems[i]);
            }

            resources.ApplyResources(dropDownItem, dropDownItem.Name);
        }

        /// <summary>
        /// Applies resources to the specified <see cref="ToolStripItem"/>.
        /// </summary>
        /// <param name="resources">The <see cref="ComponentResourceManager"/> used to apply the resources.</param>
        /// <param name="toolStripItem">The <see cref="ToolStripItem"/> to apply the resources to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="resources"/> is <c>null</c> -or- <paramref name="toolStripItem"/> is <c>null</c>.</exception>
        public static void ApplyResources(ComponentResourceManager resources, ToolStripItem toolStripItem)
        {
            if (resources == null)
            {
                throw new ArgumentNullException("resources");
            }

            if (toolStripItem == null)
            {
                throw new ArgumentNullException("toolStripItem");
            }

            resources.ApplyResources(toolStripItem, toolStripItem.Name);
        }
    }
}
