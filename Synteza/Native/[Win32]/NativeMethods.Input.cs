namespace Petroules.Synteza.Native
{
    using System;
    using System.Runtime.InteropServices;
    using Petroules.Synteza.Windows.Forms;

    /// <summary>
    /// Contains Win32 imports specific to input, such as the keyboard and mouse.
    /// </summary>
    internal static partial class NativeMethods
    {
        /// <summary>
        /// Defines a system-wide hot key.
        /// </summary>
        /// <param name="handle">Handle to the window that will receive the messages.</param>
        /// <param name="id">The identifier of the hotkey.</param>
        /// <param name="modifiers">The modifiers associated with the hotkey.</param>
        /// <param name="vlc">Specifies the virtual-key code of the hot key.</param>
        /// <returns>Whether the function succeeds.</returns>
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr handle, int id, int modifiers, int vlc);

        /// <summary>
        /// Frees a hot key previously registered by the calling thread. 
        /// </summary>
        /// <param name="handle">Handle to the window associated with the hot key to be freed. This parameter should be NULL if the hot key is not associated with a window.</param>
        /// <param name="id">Specifies the identifier of the hot key to be freed.</param>
        /// <returns>Whether the function succeeds.</returns>
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr handle, int id);

        /// <summary>
        /// Synthesizes mouse motion and button clicks.
        /// </summary>
        /// <param name="flags">
        /// Specifies various aspects of mouse motion and button clicking.
        /// This parameter can be certain combinations of the following values.
        /// The values that specify mouse button status are set to indicate changes in status,
        /// not ongoing conditions. For example, if the left mouse button is pressed and held down,
        /// MOUSEEVENTF_LEFTDOWN is set when the left button is first pressed,
        /// but not for subsequent motions. Similarly,
        /// MOUSEEVENTF_LEFTUP is set only when the button is first released.
        /// You cannot specify both MOUSEEVENTF_WHEEL and either MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP
        /// simultaneously in the dwFlags parameter, because they both require use of the dwData field.
        /// MOUSEEVENTF_ABSOLUTE
        /// Specifies that the dx and dy parameters contain normalized absolute coordinates.
        /// If not set, those parameters contain relative data:
        /// the change in position since the last reported position.
        /// This flag can be set, or not set, regardless of what kind of mouse or mouse-like device,
        /// if any, is connected to the system. For further information about relative mouse motion,
        /// see the following Remarks section.
        /// MOUSEEVENTF_MOVE
        /// Specifies that movement occurred.
        /// MOUSEEVENTF_LEFTDOWN
        /// Specifies that the left button is down.
        /// MOUSEEVENTF_LEFTUP
        /// Specifies that the left button is up.
        /// MOUSEEVENTF_RIGHTDOWN
        /// Specifies that the right button is down.
        /// MOUSEEVENTF_RIGHTUP
        /// Specifies that the right button is up.
        /// MOUSEEVENTF_MIDDLEDOWN
        /// Specifies that the middle button is down.
        /// MOUSEEVENTF_MIDDLEUP
        /// Specifies that the middle button is up.
        /// MOUSEEVENTF_WHEEL
        /// Windows NT/2000/XP: Specifies that the wheel has been moved, if the mouse has a wheel.
        /// The amount of movement is specified in dwData
        /// MOUSEEVENTF_XDOWN
        /// Windows 2000/XP: Specifies that an X button was pressed.
        /// MOUSEEVENTF_XUP
        /// Windows 2000/XP: Specifies that an X button was released.
        /// </param>
        /// <param name="x">
        /// Specifies the mouse's absolute position along the x-axis or its
        /// amount of motion since the last mouse event was generated,
        /// depending on the setting of MOUSEEVENTF_ABSOLUTE.
        /// Absolute data is specified as the mouse's actual x-coordinate;
        /// relative data is specified as the number of mickeys moved.
        /// A mickey is the amount that a mouse has to move for it to report that it has moved.
        /// </param>
        /// <param name="y">
        /// Specifies the mouse's absolute position along the y-axis or its
        /// amount of motion since the last mouse event was generated,
        /// depending on the setting of MOUSEEVENTF_ABSOLUTE.
        /// Absolute data is specified as the mouse's actual y-coordinate;
        /// relative data is specified as the number of mickeys moved.
        /// </param>
        /// <param name="buttons">
        /// If dwFlags contains MOUSEEVENTF_WHEEL, then dwData specifies the amount of wheel movement.
        /// A positive value indicates that the wheel was rotated forward, away from the user;
        /// a negative value indicates that the wheel was rotated backward, toward the user.
        /// One wheel click is defined as WHEEL_DELTA, which is 120.
        /// If dwFlags contains MOUSEEVENTF_HWHEEL, then dwData specifies the amount of wheel movement.
        /// A positive value indicates that the wheel was rotated to the right;
        /// a negative value indicates that the wheel was rotated to the left.
        /// One wheel click is defined as WHEEL_DELTA, which is 120.
        /// Windows 2000/XP: If dwFlags contains MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP,
        /// then dwData specifies which X buttons were pressed or released.
        /// This value may be any combination of the following flags.
        /// If dwFlags is not MOUSEEVENTF_WHEEL, MOUSEEVENTF_XDOWN, or MOUSEEVENTF_XUP,
        /// then dwData should be zero.
        /// XBUTTON1
        /// Set if the first X button was pressed or released.
        /// XBUTTON2
        /// Set if the second X button was pressed or released.
        /// </param>
        /// <param name="extraInfo">
        /// Specifies an additional value associated with the mouse event.
        /// An application calls GetMessageExtraInfo to obtain this extra information.
        /// </param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, EntryPoint = "mouse_event")]
        internal static extern void MouseEvent(SendClicks.MouseButtonEvents flags, long x, long y, long buttons, long extraInfo);
    }
}
