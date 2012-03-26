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
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Form.WindowState"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Form"/>.</param>
        /// <returns>See summary.</returns>
        public static FormWindowState GetWindowState(this Form control)
        {
            if (control.InvokeRequired)
            {
                return (FormWindowState)control.Invoke((Func<FormWindowState>)(() => control.WindowState));
            }
            else
            {
                return control.WindowState;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Form.WindowState"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Form"/>.</param>
        /// <param name="value">The <see cref="System.Windows.Forms.FormWindowState"/> to set.</param>
        public static void SetWindowState(this Form control, FormWindowState value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<FormWindowState>)((FormWindowState var) => control.WindowState = var), value);
            }
            else
            {
                control.WindowState = value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Form.FormBorderStyle"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Form"/>.</param>
        /// <returns>See summary.</returns>
        public static FormBorderStyle GetFormBorderStyle(this Form control)
        {
            if (control.InvokeRequired)
            {
                return (FormBorderStyle)control.Invoke((Func<FormBorderStyle>)(() => control.FormBorderStyle));
            }
            else
            {
                return control.FormBorderStyle;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Form.FormBorderStyle"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Form"/>.</param>
        /// <param name="value">The <see cref="System.Windows.Forms.FormBorderStyle"/> to set.</param>
        public static void SetFormBorderStyle(this Form control, FormBorderStyle value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<FormBorderStyle>)((FormBorderStyle var) => control.FormBorderStyle = var), value);
            }
            else
            {
                control.FormBorderStyle = value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Form.ShowDialog()"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Form"/>.</param>
        /// <returns>See summary.</returns>
        public static DialogResult ShowDialogSafe(this Form control)
        {
            if (control.InvokeRequired)
            {
                return (DialogResult)control.Invoke((Func<DialogResult>)control.ShowDialog);
            }
            else
            {
                return control.ShowDialog();
            }
        }
    }
}