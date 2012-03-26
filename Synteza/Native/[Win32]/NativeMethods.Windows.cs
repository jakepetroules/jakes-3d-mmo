namespace Petroules.Synteza.Native
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Contains Win32 imports specific to windows (as in window manager).
    /// </summary>
    internal static partial class NativeMethods
    {
        /// <summary>
        /// Windows constant to restore a minimized window (original name SW_RESTORE).
        /// </summary>
        public const int SWRESTORE = 9;

        /// <summary>
        /// Finds a window handle by class name and window title.
        /// </summary>
        /// <param name="className">Class name of the window to find.</param>
        /// <param name="windowName">Title of the window to find.</param>
        /// <returns>The handle of the requested window.</returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string className, string windowName);

        /// <summary>
        /// Gets the last active popup/dialog box for a main window.
        /// </summary>
        /// <param name="handle">Handle of a main window.</param>
        /// <returns>Handle of the last active popup/dialog box for the specified main window.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetLastActivePopup(IntPtr handle);

        /// <summary>
        /// Finds whether a window is minimized.
        /// </summary>
        /// <param name="handle">The handle of the window to check.</param>
        /// <returns>Whether the window is minimized.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr handle);

        /// <summary>
        /// Finds whether a window is enabled.
        /// </summary>
        /// <param name="handle">The handle of the window to check.</param>
        /// <returns>Whether the window is enabled.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowEnabled(IntPtr handle);

        /// <summary>
        /// Sets the foreground window of the operating system.
        /// </summary>
        /// <param name="handle">The handle of the window to set as the foreground window.</param>
        /// <returns>True if the window was brought to the foreground, false if it was not.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr handle);

        /// <summary>
        /// Sets the specified window's show state.
        /// </summary>
        /// <param name="handle">The handle of the window to send the command to.</param>
        /// <param name="showCommand">The command to send.</param>
        /// <returns>True if the window was previously visible, false if it was not.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr handle, int showCommand);

        /// <summary>
        /// Retrieves the window handle to the active window attached to the calling thread's message queue.
        /// </summary>
        /// <returns>The return value is the handle to the active window attached to the calling thread's message queue. Otherwise, the return value is NULL.</returns>
        /// <remarks>
        /// To get the handle to the foreground window, you can use GetForegroundWindow.
        /// <strong>Windows 98/Me and Windows NT 4.0 SP3 and later:</strong>
        /// To get the window handle to the active window in the message queue for another thread,
        /// use GetGUIThreadInfo.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        /// <summary>
        /// Retrieves a handle to the window that contains the specified point. This is a Win32 function.
        /// </summary>
        /// <param name="point">Specifies a Point structure that defines the point to be checked.</param>
        /// <returns>
        /// The return value is a handle to the window that contains the point.
        /// If no window exists at the given point, the return value is NULL.
        /// If the point is over a static text control, the return value is a
        /// handle to the window under the static text control.
        /// </returns>
        /// <remarks>
        /// The WindowFromPoint function does not retrieve a handle to a hidden or disabled window,
        /// even if the point is within the window. An application should use the ChildWindowFromPoint
        /// function for a nonrestrictive search.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point point);

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window.
        /// </summary>
        /// <param name="handle">Handle to the window.</param>
        /// <returns>The bounding rectangle of the window.</returns>
        public static Rectangle GetWindowRect(IntPtr handle)
        {
            RECT rect;
            NativeMethods.GetWindowRect(handle, out rect);
            return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window. This is a Win32 function.
        /// </summary>
        /// <param name="handle">Handle to the window.</param>
        /// <param name="rect">
        /// Pointer to a structure that receives the screen coordinates
        /// of the upper-left and lower-right corners of the window.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero.
        /// To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// In conformance with conventions for the RECT structure, the
        /// bottom-right coordinates of the returned rectangle are exclusive.
        /// In other words, the pixel at (right, bottom) lies immediately outside the rectangle.
        /// </remarks>
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr handle, out RECT rect);

        /// <summary>
        /// Defines the coordinates of the upper-left and lower-right corners of a rectangle. This is a Win32 structure.
        /// </summary>
        /// <remarks>
        /// By convention, the right and bottom edges of the rectangle are normally considered exclusive.
        /// In other words, the pixel whose coordinates are (right, bottom) lies immediately outside of
        /// the the rectangle. For example, when RECT is passed to the FillRect function, the rectangle
        /// is filled up to, but not including, the right column and bottom row of pixels. This structure
        /// is identical to the RECTL structure.
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            /// <summary>
            /// Specifies the x-coordinate of the upper-left corner of the rectangle. 
            /// </summary>
            public int Left;

            /// <summary>
            /// Specifies the y-coordinate of the upper-left corner of the rectangle. 
            /// </summary>
            public int Top;

            /// <summary>
            /// Specifies the x-coordinate of the lower-right corner of the rectangle. 
            /// </summary>
            public int Right;

            /// <summary>
            /// Specifies the y-coordinate of the lower-right corner of the rectangle. 
            /// </summary>
            public int Bottom;
        }
    }
}
