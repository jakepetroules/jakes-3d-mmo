namespace Petroules.Synteza.Native
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Contains Win32 imports specific to icons.
    /// </summary>
    internal static partial class NativeMethods
    {
        /// <summary>
        /// Retrieves information about the specified icon or cursor. 
        /// </summary>
        /// <param name="icon">Handle to the icon or cursor.</param>
        /// <param name="iconInfo">Pointer to an ICONINFO structure. The function fills in the structure's members.</param>
        /// <returns>See summary.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr icon, ref IconInfo iconInfo);

        /// <summary>
        /// Creates an icon or cursor from an ICONINFO structure.
        /// </summary>
        /// <param name="icon">Pointer to an ICONINFO structure the function used to create the icon or cursor.</param>
        /// <returns>See summary.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        /// <summary>
        /// Contains information about an icon or a cursor.
        /// </summary>
        public struct IconInfo
        {
            /// <summary>
            /// Specifies whether this structure defines an icon or a cursor.
            /// A value of TRUE specifies an icon; FALSE specifies a cursor.
            /// </summary>
            public bool Icon;

            /// <summary>
            /// Specifies the x-coordinate of a cursor's hot spot.
            /// If this structure defines an icon, the hot spot is always in the center of the icon,
            /// and this member is ignored.
            /// </summary>
            public int HotspotX;

            /// <summary>
            /// Specifies the y-coordinate of the cursor's hot spot.
            /// If this structure defines an icon, the hot spot is always in the center of the icon,
            /// and this member is ignored.
            /// </summary>
            public int HotspotY;

            /// <summary>
            /// Specifies the icon bitmask bitmap.
            /// If this structure defines a black and white icon,
            /// this bitmask is formatted so that the upper half
            /// is the icon AND bitmask and the lower half is the icon XOR bitmask.
            /// Under this condition, the height should be an even multiple of two.
            /// If this structure defines a color icon, this mask only defines the
            /// AND bitmask of the icon.
            /// </summary>
            public IntPtr Mask;

            /// <summary>
            /// Handle to the icon color bitmap. This member can be optional if this
            /// structure defines a black and white icon. The AND bitmask of hbmMask
            /// is applied with the SRCAND flag to the destination; subsequently, the
            /// color bitmap is applied (using XOR) to the destination by using the
            /// SRCINVERT flag.
            /// </summary>
            public IntPtr Color;

            /// <summary>
            /// Initializes a new instance of the IconInfo struct.
            /// </summary>
            /// <param name="hotspotX">
            /// Specifies the x-coordinate of a cursor's hot spot.
            /// If this structure defines an icon, the hot spot is always in the center of the icon,
            /// and this member is ignored.
            /// </param>
            /// <param name="hotspotY">
            /// Specifies the y-coordinate of the cursor's hot spot.
            /// If this structure defines an icon, the hot spot is always in the center of the icon,
            /// and this member is ignored.
            /// </param>
            /// <param name="icon">
            /// Specifies whether this structure defines an icon or a cursor.
            /// A value of TRUE specifies an icon; FALSE specifies a cursor.
            /// </param>
            public IconInfo(int hotspotX, int hotspotY, bool icon)
            {
                this.HotspotX = hotspotX;
                this.HotspotY = hotspotY;
                this.Icon = icon;
                this.Mask = IntPtr.Zero;
                this.Color = IntPtr.Zero;
            }
        }
    }
}
