namespace Petroules.Synteza.Native
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Provides methods imported from the X11 Window System libraries.
    /// </summary>
    internal static partial class NativeMethods
    {
        [DllImport("libX11", EntryPoint = "XOpenDisplay")]
        public static extern IntPtr XOpenDisplay(string displayName);

        [DllImport("libX11", EntryPoint = "XDefaultRootWindow")]
        public static extern IntPtr XDefaultRootWindow(IntPtr display);
        
        [DllImport("libX11", EntryPoint = "XQueryPointer")]
        public static extern bool XQueryPointer(IntPtr display, IntPtr window, out IntPtr root, out IntPtr child, out int root_x, out int root_y, out int win_x, out int win_y, out int keys_buttons);

        [DllImport("libX11", EntryPoint = "XGetGeometry")]
        public static extern IntPtr XGetGeometry(IntPtr display, IntPtr d, out IntPtr root_return, out int x_return, out int y_return, out uint width_return, out uint height_return, out uint border_width_return, out uint depth_return);
    }
}
