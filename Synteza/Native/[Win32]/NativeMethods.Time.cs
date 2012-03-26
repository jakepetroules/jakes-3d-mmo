namespace Petroules.Synteza.Native
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Contains Win32 imports specific to time.
    /// </summary>
    internal static partial class NativeMethods
    {
        /// <summary>
        /// Gets the system time using the native Windows method.
        /// </summary>
        /// <param name="systemTime">The system time structure to write to.</param>
        [DllImport("kernel32.dll")]
        public static extern void GetSystemTime(ref SystemTimeStructure systemTime);

        /// <summary>
        /// Sets the system time using the native Windows method.
        /// </summary>
        /// <param name="systemTime">The system time structure to read from.</param>
        /// <returns>Zero indicates failure. Nonzero indicates success.</returns>
        [DllImport("kernel32.dll")]
        public static extern uint SetSystemTime(ref SystemTimeStructure systemTime);

        /// <summary>
        /// Represents the Windows system time structure.
        /// </summary>
        public struct SystemTimeStructure
        {
            /// <summary>
            /// The year component of the date represented by this instance.
            /// </summary>
            public ushort Year;

            /// <summary>
            /// The month component of the date represented by this instance.
            /// </summary>
            public ushort Month;

            /// <summary>
            /// The day of the week represented by this instance.
            /// </summary>
            public ushort DayOfWeek;

            /// <summary>
            /// The day of the month represented by this instance.
            /// </summary>
            public ushort Day;

            /// <summary>
            /// The hour component of the date represented by this instance.
            /// </summary>
            public ushort Hour;

            /// <summary>
            /// The minute component of the date represented by this instance.
            /// </summary>
            public ushort Minute;

            /// <summary>
            /// The seconds component of the date represented by this instance.
            /// </summary>
            public ushort Second;

            /// <summary>
            /// The milliseconds component of the date represented by this instance.
            /// </summary>
            public ushort Milliseconds;
        }
    }
}
