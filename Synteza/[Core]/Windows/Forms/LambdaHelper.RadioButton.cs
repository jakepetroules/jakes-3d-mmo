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
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.RadioButton.Checked"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.RadioButton"/>.</param>
        /// <returns>See summary.</returns>
        public static bool GetChecked(this RadioButton control)
        {
            if (control.InvokeRequired)
            {
                return (bool)control.Invoke((Func<bool>)(() => control.Checked));
            }
            else
            {
                return control.Checked;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.RadioButton.Checked"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.RadioButton"/>.</param>
        /// <param name="value">The <see cref="bool"/> to set.</param>
        public static void SetChecked(this RadioButton control, bool value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<bool>)((bool var) => control.Checked = var), value);
            }
            else
            {
                control.Checked = value;
            }
        }
    }
}