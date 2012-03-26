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
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Control.Enabled"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Control"/>.</param>
        /// <returns>See summary.</returns>
        public static bool GetEnabled(this Control control)
        {
            if (control.InvokeRequired)
            {
                return (bool)control.Invoke((Func<bool>)(() => control.Enabled));
            }
            else
            {
                return control.Enabled;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Control.Enabled"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Control"/>.</param>
        /// <param name="value">The <see cref="bool"/> to set.</param>
        public static void SetEnabled(this Control control, bool value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<bool>)((bool var) => control.Enabled = var), value);
            }
            else
            {
                control.Enabled = value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Control.Text"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Control"/>.</param>
        /// <returns>See summary.</returns>
        public static string GetText(this Control control)
        {
            if (control.InvokeRequired)
            {
                return control.Invoke((Func<string>)(() => control.Text)) as string;
            }
            else
            {
                return control.Text;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Control.Text"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Control"/>.</param>
        /// <param name="value">The <see cref="string"/> to set.</param>
        public static void SetText(this Control control, string value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<string>)((string var) => control.Text = var), value);
            }
            else
            {
                control.Text = value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Control.Visible"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Control"/>.</param>
        /// <returns>See summary.</returns>
        public static bool GetVisible(this Control control)
        {
            if (control.InvokeRequired)
            {
                return (bool)control.Invoke((Func<bool>)(() => control.Visible));
            }
            else
            {
                return control.Visible;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Control.Visible"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Control"/>.</param>
        /// <param name="value">The <see cref="bool"/> to set.</param>
        public static void SetVisible(this Control control, bool value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<bool>)((bool var) => control.Visible = var), value);
            }
            else
            {
                control.Visible = value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Control.BringToFront"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Control"/>.</param>
        public static void BringToFrontSafe(this Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)control.BringToFront);
            }
            else
            {
                control.BringToFront();
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Control.SendToBack"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Control"/>.</param>
        public static void SendToBackSafe(this Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)control.SendToBack);
            }
            else
            {
                control.SendToBack();
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Control.Show"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Control"/>.</param>
        public static void ShowSafe(this Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)control.Show);
            }
            else
            {
                control.Show();
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.Control.Hide"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.Control"/>.</param>
        public static void HideSafe(this Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)control.Hide);
            }
            else
            {
                control.Hide();
            }
        }
    }
}
