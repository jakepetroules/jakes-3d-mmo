namespace Petroules.Synteza.Windows.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Contains helper methods to interact with <see cref="System.Windows.Forms"/>
    /// controls in a thread-safe manner. All methods are extension methods and follow
    /// the naming convention of <c>Get[PropertyName]()</c> and <c>Set[PropertyName](value)</c>.
    /// </summary>
    public static partial class LambdaHelper
    {
        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.PictureBox.Image"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.PictureBox"/>.</param>
        /// <returns>See summary.</returns>
        public static Image GetImage(this PictureBox control)
        {
            if (control.InvokeRequired)
            {
                return control.Invoke((Func<Image>)(() => control.Image)) as Image;
            }
            else
            {
                return control.Image;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.PictureBox.Image"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.PictureBox"/>.</param>
        /// <param name="value">The <see cref="System.Drawing.Image"/> to set.</param>
        public static void SetImage(this PictureBox control, Image value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<Image>)((Image var) => control.Image = var), value);
            }
            else
            {
                control.Image = value;
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.PictureBox.Load()"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.PictureBox"/>.</param>
        public static void LoadSafe(this PictureBox control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)control.Load);
            }
            else
            {
                control.Load();
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.PictureBox.Load(string)"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.PictureBox"/>.</param>
        /// <param name="value">The <see cref="string"/> to set.</param>
        public static void LoadSafe(this PictureBox control, string value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<string>)((string var) => control.Load(var)), value);
            }
            else
            {
                control.Load(value);
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.PictureBox.LoadAsync()"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.PictureBox"/>.</param>
        public static void LoadAsyncSafe(this PictureBox control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action)control.LoadAsync);
            }
            else
            {
                control.LoadAsync();
            }
        }

        /// <summary>
        /// Thread-safe wrapper for <see cref="System.Windows.Forms.PictureBox.LoadAsync(string)"/>.
        /// </summary>
        /// <param name="control">The <see cref="System.Windows.Forms.PictureBox"/>.</param>
        /// <param name="value">The <see cref="string"/> to set.</param>
        public static void LoadAsyncSafe(this PictureBox control, string value)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((Action<string>)((string var) => control.LoadAsync(var)), value);
            }
            else
            {
                control.LoadAsync(value);
            }
        }
    }
}
