namespace Petroules.Synteza
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Petroules.Synteza.Native;

    /// <summary>
    /// Provides cross-platform methods for interacting with the operating system's window manager.
    /// </summary>
    public static class WindowManager
    {
        /// <summary>
        /// Bring the active window of the application with the specified title to the foreground.
        /// </summary>
        /// <param name="windowCaption">The title of the window to find.</param>
        /// <exception cref="UnsupportedPlatformException">The current platform is not supported.</exception>
        public static void SetFocus(string windowCaption)
        {
            PlatformUtilities.ThrowIfUnsupported(PlatformID.Win32NT);

            // Get the handle to the main window of the previous instance of the program
            IntPtr previousInstanceHandle = NativeMethods.FindWindow(null, windowCaption);

            // If the window was found...
            if (previousInstanceHandle != null)
            {
                // Get the handle of any active child window (dialog, etc.) if it exists
                IntPtr childWindowHandle = NativeMethods.GetLastActivePopup(previousInstanceHandle);

                // If a sub-window was found, set input focus to that,
                // otherwise set it to the application's main window
                if (childWindowHandle != null && NativeMethods.IsWindowEnabled(childWindowHandle))
                {
                    previousInstanceHandle = childWindowHandle;
                }

                // Set the foreground window
                NativeMethods.SetForegroundWindow(previousInstanceHandle);

                // Restore the window if it's minimized
                if (NativeMethods.IsIconic(previousInstanceHandle))
                {
                    NativeMethods.ShowWindow(previousInstanceHandle, NativeMethods.SWRESTORE);
                }
            }
        }

        /// <summary>
        /// Retrieves a handle to the window that contains the specified point.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="UnsupportedPlatformException">The current platform is not supported.</exception>
        public static IntPtr WindowFromPoint(Point point)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    return NativeMethods.WindowFromPoint(point);
                case PlatformID.Unix:
                    return WindowManager.X11WindowFromPoint(point);
                default:
                    throw PlatformUtilities.GetUnsupportedException(PlatformID.Win32NT, PlatformID.Unix);
            }
        }

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window.
        /// </summary>
        /// <param name="handle">Handle to the window.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="UnsupportedPlatformException">The current platform is not supported.</exception>
        public static Rectangle GetWindowRect(IntPtr handle)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    return NativeMethods.GetWindowRect(handle);
                case PlatformID.Unix:
                    return WindowManager.X11GetWindowRect(handle);
                default:
                    throw PlatformUtilities.GetUnsupportedException(PlatformID.Win32NT, PlatformID.Unix);
            }
        }

        /// <summary>
        /// Retrieves a handle to the X11 window that contains the specified point.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>See summary.</returns>
        private static IntPtr X11WindowFromPoint(Point point)
        {
            // Save current cursor position and set to target (this is a trick to get the desired point)
            Point original = Cursor.Position;
            Cursor.Position = point;

            // Get the connection to the X server
            IntPtr display = NativeMethods.XOpenDisplay(null);

            // Get the display's root window (the desktop?)
            IntPtr window = NativeMethods.XDefaultRootWindow(display);

            // Required out variables
            IntPtr root;
            IntPtr child;
            int root_x;
            int root_y;
            int child_x;
            int child_y;
            int keys_buttons;

            // We're just doing this to get the window handle...
            NativeMethods.XQueryPointer(display, window, out root, out child, out root_x, out root_y, out child_x, out child_y, out keys_buttons);

            // Restore original cursor position
            Cursor.Position = original;
            return child;
        }

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window.
        /// </summary>
        /// <param name="handle">Handle to the window.</param>
        /// <returns>The bounding rectangle of the window.</returns>
        private static Rectangle X11GetWindowRect(IntPtr handle)
        {
            // Get the connection to the X server
            IntPtr display = NativeMethods.XOpenDisplay(null);

            // Required out variables
            IntPtr root;
            int x;
            int y;
            uint width;
            uint height;
            uint border;
            uint depth;

            // Get the window's geometry information
            NativeMethods.XGetGeometry(display, handle, out root, out x, out y, out width, out height, out border, out depth);
            return new Rectangle(x, y, (int)width, (int)height);
        }
    }
}
