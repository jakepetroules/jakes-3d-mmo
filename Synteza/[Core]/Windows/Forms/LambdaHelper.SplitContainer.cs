namespace Petroules.Synteza.Windows.Forms
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Contains helper methods to interact with <see cref="System.Windows.Forms"/>
    /// controls in a thread-safe manner. All methods are extension methods and follow
    /// the naming convention of <c>Get[PropertyName]()</c> and <c>Set[PropertyName](value)</c>.
    /// </summary>
    public static partial class LambdaHelper
    {
        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.SplitContainer.Panel1Collapsed"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.SplitContainer"/>.</param>
        /// <returns>See summary.</returns>
        public static bool GetPanel1Collapsed(this SplitContainer control)
        {
            if (control.InvokeRequired)
            {
                return (bool)control.Invoke((Func<bool>)(() => control.Panel1Collapsed));
            }
            else
            {
                return control.Panel1Collapsed;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.SplitContainer.Panel1Collapsed"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.SplitContainer"/>.</param>
        /// <param name="value">The <see cref="bool"/> to set.</param>
        public static void SetPanel1Collapsed(this SplitContainer control, bool value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<bool>)((bool var) => control.Panel1Collapsed = var), value);
            }
            else
            {
                control.Panel1Collapsed = value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.SplitContainer.Panel2Collapsed"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.SplitContainer"/>.</param>
        /// <returns>See summary.</returns>
        public static bool GetPanel2Collapsed(this SplitContainer control)
        {
            if (control.InvokeRequired)
            {
                return (bool)control.Invoke((Func<bool>)(() => control.Panel2Collapsed));
            }
            else
            {
                return control.Panel2Collapsed;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.SplitContainer.Panel2Collapsed"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.SplitContainer"/>.</param>
        /// <param name="value">The <see cref="bool"/> to set.</param>
        public static void SetPanel2Collapsed(this SplitContainer control, bool value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<bool>)((bool var) => control.Panel2Collapsed = var), value);
            }
            else
            {
                control.Panel2Collapsed = value;
            }
        }
    }
}
