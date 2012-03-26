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
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.ProgressBar.Style"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.ProgressBar"/>.</param>
        /// <returns>See summary.</returns>
        public static ProgressBarStyle GetStyle(this ProgressBar control)
        {
            if (control.InvokeRequired)
            {
                return (ProgressBarStyle)control.Invoke((Func<ProgressBarStyle>)(() => control.Style));
            }
            else
            {
                return control.Style;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.ProgressBar.Style"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.ProgressBar"/>.</param>
        /// <param name="value">The <see cref="System.Windows.Forms.ProgressBarStyle"/> to set.</param>
        public static void SetStyle(this ProgressBar control, ProgressBarStyle value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<ProgressBarStyle>)((ProgressBarStyle var) => control.Style = var), value);
            }
            else
            {
                control.Style = value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.ProgressBar.Value"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.ProgressBar"/>.</param>
        /// <returns>See summary.</returns>
        public static int GetValue(this ProgressBar control)
        {
            if (control.InvokeRequired)
            {
                return (int)control.Invoke((Func<int>)(() => control.Value));
            }
            else
            {
                return control.Value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.ProgressBar.Value"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.ProgressBar"/>.</param>
        /// <param name="value">The <see cref="int"/> to set.</param>
        public static void SetValue(this ProgressBar control, int value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<int>)((int var) => control.Value = var), value);
            }
            else
            {
                control.Value = value;
            }
        }
    }
}
