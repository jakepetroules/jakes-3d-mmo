namespace Petroules.Synteza.Imaging
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Petroules.Synteza.Native;
    using Petroules.Synteza.Properties;

    /// <summary>
    /// Functions for capturing images.
    /// </summary>
    public sealed class ImageCapture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageCapture"/> class.
        /// </summary>
        /// <exception cref="UnsupportedPlatformException">The current platform is not supported.</exception>
        public ImageCapture()
        {
            PlatformUtilities.ThrowIfUnsupported(PlatformID.Win32NT);

            this.CaptureMouseCursor = false;
            this.DelayCapture = 0;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to include the mouse cursor in screen captures.
        /// </summary>
        /// <value>A value indicating whether to include the mouse cursor in screen captures.</value>
        public bool CaptureMouseCursor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of milliseconds to wait before actually capturing the image.
        /// </summary>
        /// <value>Number of milliseconds to wait before actually capturing the image.</value>
        public int DelayCapture
        {
            get;
            set;
        }

        /// <summary>
        /// Captures a bitmap of the mouse cursor.
        /// </summary>
        /// <param name="cursorPosition">The position of the mouse cursor on the screen.</param>
        /// <returns>A bitmap of the mouse cursor.</returns>
        public static Bitmap CaptureCursor(out Point cursorPosition)
        {
            cursorPosition = new Point(Cursor.Position.X - Cursor.Current.HotSpot.X, Cursor.Position.Y - Cursor.Current.HotSpot.Y);

            return Icon.FromHandle(Cursor.Current.CopyHandle()).ToBitmap();
        }

        /// <summary>
        /// Gets a bitmap of the screen at the specified region and with the specified size.
        /// </summary>
        /// <param name="area">A Rectangle object representing the area to capture.</param>
        /// <returns>A bitmap of the screen at the specified coordinates and with the specified size.</returns>
        public Bitmap CaptureRegion(Rectangle area)
        {
            return this.CaptureImage(false, area, -1);
        }

        /// <summary>
        /// Gets a bitmap of the specified window.
        /// </summary>
        /// <param name="handle">The handle of the window to capture.</param>
        /// <returns>A bitmap of the specified window.</returns>
        public Bitmap CaptureWindow(IntPtr handle)
        {
            return this.CaptureImage(false, WindowManager.GetWindowRect(handle), -1);
        }

        /// <summary>
        /// Gets a bitmap of the entire screen of the primary display device.
        /// </summary>
        /// <returns>A bitmap of the entire screen.</returns>
        public Bitmap CaptureDesktop()
        {
            return this.CaptureImage(true, Rectangle.Empty, -1);
        }

        /// <summary>
        /// Gets a bitmap of the entire screen of the specified display device.
        /// </summary>
        /// <param name="displayDeviceIndex">The index of the display device to capture the desktop of.</param>
        /// <returns>A bitmap of the entire screen.</returns>
        public Bitmap CaptureDesktop(int displayDeviceIndex)
        {
            return this.CaptureImage(true, Rectangle.Empty, displayDeviceIndex);
        }

        /// <summary>
        /// Captures an image using coordinates.
        /// </summary>
        /// <param name="fullscreen">True to capture fullscreen or false to use the coordinates.</param>
        /// <param name="area">A Rectangle object representing the area to capture.</param>
        /// <param name="displayDeviceIndex">The index of the displat device to capture. Used only for desktop capture.</param>
        /// <returns>The captured image.</returns>
        /// <exception cref="ArgumentException">Thrown if the width or height of the Rectangle object are less than or equal to zero.</exception>
        private Bitmap CaptureImage(bool fullscreen, Rectangle area, int displayDeviceIndex)
        {
            if (this.DelayCapture > 0)
            {
                Thread.Sleep(this.DelayCapture);
            }

            if (fullscreen)
            {
                if (displayDeviceIndex < -1 || displayDeviceIndex >= Screen.AllScreens.Length)
                {
                    displayDeviceIndex = 0;
                }

                Screen capScreen = displayDeviceIndex == -1 ? Screen.PrimaryScreen : Screen.AllScreens[displayDeviceIndex];
                area = new Rectangle(capScreen.Bounds.Left, capScreen.Bounds.Top, capScreen.Bounds.Width, capScreen.Bounds.Height);
            }

            if (area.Width <= 0 || area.Height <= 0)
            {
                throw new ArgumentException(Resources.ImageSizeTooSmall);
            }

            Bitmap screen = new Bitmap(area.Width, area.Height);
            Graphics g = Graphics.FromImage(screen);
            g.CopyFromScreen(area.Left, area.Top, 0, 0, screen.Size);

            if (this.CaptureMouseCursor)
            {
                Point cursorPosition;

                Bitmap cursor = ImageCapture.CaptureCursor(out cursorPosition);
                Rectangle cursorRect = new Rectangle(cursorPosition.X - area.Left, cursorPosition.Y - area.Top, cursor.Width, cursor.Height);
                g.DrawImage(cursor, cursorRect);
                g.Flush();
            }

            return screen;
        }
    }
}
