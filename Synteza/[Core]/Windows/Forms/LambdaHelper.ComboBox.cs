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
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.ComboBox.SelectedIndex"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.ComboBox"/>.</param>
        /// <returns>See summary.</returns>
        public static int GetSelectedIndex(this ComboBox control)
        {
            if (control.InvokeRequired)
            {
                return (int)control.Invoke((Func<int>)(() => control.SelectedIndex));
            }
            else
            {
                return control.SelectedIndex;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.ComboBox.SelectedIndex"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.ComboBox"/>.</param>
        /// <param name="value">The <see cref="int"/> to set.</param>
        public static void SetSelectedIndex(this ComboBox control, int value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<int>)((int var) => control.SelectedIndex = var), value);
            }
            else
            {
                control.SelectedIndex = value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.ComboBox.SelectedItem"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.ComboBox"/>.</param>
        /// <returns>See summary.</returns>
        public static object GetSelectedItem(this ComboBox control)
        {
            if (control.InvokeRequired)
            {
                return (object)control.Invoke((Func<object>)(() => control.SelectedItem));
            }
            else
            {
                return control.SelectedItem;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.ComboBox.SelectedItem"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.ComboBox"/>.</param>
        /// <param name="value">The <see cref="object"/> to set.</param>
        public static void SetSelectedItem(this ComboBox control, object value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<object>)((object var) => control.SelectedItem = var), value);
            }
            else
            {
                control.SelectedItem = value;
            }
        }
    }
}
