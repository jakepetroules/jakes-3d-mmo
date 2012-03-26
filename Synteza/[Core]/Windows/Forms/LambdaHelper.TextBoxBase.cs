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
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.TextBoxBase.SelectionStart"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.TextBoxBase"/>.</param>
        /// <returns>See summary.</returns>
        public static int GetSelectionStart(this TextBoxBase control)
        {
            if (control.InvokeRequired)
            {
                return (int)control.Invoke((Func<int>)(() => control.SelectionStart));
            }
            else
            {
                return control.SelectionStart;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.TextBoxBase.SelectionStart"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.TextBoxBase"/>.</param>
        /// <param name="value">The <see cref="int"/> to set.</param>
        public static void SetSelectionStart(this TextBoxBase control, int value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<int>)((int var) => control.SelectionStart = var), value);
            }
            else
            {
                control.SelectionStart = value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.TextBoxBase.Clear"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.TextBoxBase"/>.</param>
        public static void ClearSafe(this TextBoxBase control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)control.Clear);
            }
            else
            {
                control.Clear();
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.TextBoxBase.ScrollToCaret"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.TextBoxBase"/>.</param>
        public static void ScrollToCaretSafe(this TextBoxBase control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)control.ScrollToCaret);
            }
            else
            {
                control.ScrollToCaret();
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.TextBoxBase.AppendText(string)"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.TextBoxBase"/>.</param>
        /// <param name="value">The <see cref="string"/> to set.</param>
        public static void AppendTextSafe(this TextBoxBase control, string value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<string>)((string var) => control.AppendText(var)), value);
            }
            else
            {
                control.AppendText(value);
            }
        }
    }
}
