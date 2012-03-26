namespace Petroules.Synteza.Windows.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Petroules.Synteza.Native;

    /// <summary>
    /// Provides methods for sending mouse clicks to an application.
    /// </summary>
    public static class SendClicks
    {
        /// <summary>
        /// Initializes static members of the <see cref="SendClicks"/> class.
        /// </summary>
        /// <exception cref="UnsupportedPlatformException">The current platform is not supported.</exception>
        static SendClicks()
        {
            PlatformUtilities.ThrowIfUnsupported(PlatformID.Win32NT);
        }

        /// <summary>
        /// Enumeration of possible mouse button combinations that can be passed to mouse_event.
        /// </summary>
        [Flags]
        public enum MouseButtonEvents
        {
            /// <summary>
            /// Specifies that the left mouse button should be pressed down.
            /// </summary>
            LeftDown = 2,

            /// <summary>
            /// Specifies that the left mouse button should be released.
            /// </summary>
            LeftUp = 4,

            /// <summary>
            /// Specifies that the left mouse button should be clicked (pressed and released).
            /// </summary>
            Left = LeftDown | LeftUp,

            /// <summary>
            /// Specifies that the right mouse button should be pressed down.
            /// </summary>
            RightDown = 8,

            /// <summary>
            /// Specifies that the right mouse button should be released.
            /// </summary>
            RightUp = 16,

            /// <summary>
            /// Specifies that the right mouse button should be clicked (pressed and released).
            /// </summary>
            Right = RightDown | RightUp,

            /// <summary>
            /// Specifies that the middle mouse button should be pressed down.
            /// </summary>
            MiddleDown = 32,

            /// <summary>
            /// Specifies that the middle mouse button should be released.
            /// </summary>
            MiddleUp = 64,

            /// <summary>
            /// Specifies that the middle mouse button should be clicked (pressed and released).
            /// </summary>
            Middle = MiddleDown | MiddleUp
        }

        /// <summary>
        /// Sends mouse clicks to the active application.
        /// </summary>
        /// <param name="mouseButtons">The mouse button to click.</param>
        /// <exception cref="InvalidEnumArgumentException"><paramref name="mouseButtons"/> was not one of the enumeration values: 'Left', 'Right', 'Middle'.</exception>
        public static void Send(MouseButtons mouseButtons)
        {
            MouseButtonEvents events = 0;

            switch (mouseButtons)
            {
                case MouseButtons.Left:
                    events = MouseButtonEvents.Left;
                    break;
                case MouseButtons.Right:
                    events = MouseButtonEvents.Right;
                    break;
                case MouseButtons.Middle:
                    events = MouseButtonEvents.Middle;
                    break;
                default:
                    throw new InvalidEnumArgumentException("mouseButtons", (int)mouseButtons, typeof(MouseButtons));
            }
            
            NativeMethods.MouseEvent(events, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        /// <summary>
        /// Moves the cursor to the location specified by <code>loc</code> and then sends the mouse button click specified by <code>mb</code> to the active application.
        /// </summary>
        /// <param name="mouseButtons">The mouse button to click.</param>
        /// <param name="location">The screen location to move the cursor to.</param>
        /// <exception cref="InvalidEnumArgumentException"><paramref name="mouseButtons"/> was not one of the enumeration values: 'Left', 'Right', 'Middle'.</exception>
        public static void Send(MouseButtons mouseButtons, Point location)
        {
            Cursor.Position = location;
            SendClicks.Send(mouseButtons);
        }

        /// <summary>
        /// Sends mouse messages to the active application.
        /// </summary>
        /// <param name="flags">The mouse events to send.</param>
        public static void Send(MouseButtonEvents flags)
        {
            NativeMethods.MouseEvent(flags, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }
    }
}
