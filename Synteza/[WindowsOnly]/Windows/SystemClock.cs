namespace Petroules.Synteza.Windows
{
    using System;
    using Petroules.Synteza.Native;
    using Petroules.Synteza.Properties;

    /// <summary>
    /// Provides a simple interface to the Windows system time using .NET types.
    /// </summary>
    public static class SystemClock
    {
        /// <summary>
        /// Gets or sets the system time in UTC.
        /// </summary>
        /// <exception cref="InvalidOperationException">Unable to set the system time.</exception>
        /// <exception cref="UnsupportedPlatformException">The current platform is not supported.</exception>
        public static DateTime UtcTime
        {
            get
            {
                PlatformUtilities.ThrowIfUnsupported(PlatformID.Win32NT);

                NativeMethods.SystemTimeStructure systime = new NativeMethods.SystemTimeStructure();
                NativeMethods.GetSystemTime(ref systime);

                return new DateTime(systime.Year, systime.Month, systime.Day, systime.Hour, systime.Minute, systime.Second, systime.Milliseconds, DateTimeKind.Utc);
            }

            set
            {
                PlatformUtilities.ThrowIfUnsupported(PlatformID.Win32NT);

                // Windows system time is stored as universal time!
                value = value.ToUniversalTime();

                NativeMethods.SystemTimeStructure systime = new NativeMethods.SystemTimeStructure();
                systime.Year = (ushort)value.Year;
                systime.Month = (ushort)value.Month;
                systime.DayOfWeek = (ushort)value.DayOfWeek;
                systime.Day = (ushort)value.Day;
                systime.Hour = (ushort)value.Hour;
                systime.Minute = (ushort)value.Minute;
                systime.Second = (ushort)value.Second;
                systime.Milliseconds = (ushort)value.Millisecond;

                if (NativeMethods.SetSystemTime(ref systime) == 0)
                {
                    throw new InvalidOperationException(Resources.UnableToSetSystemTime);
                }
            }
        }

        /// <summary>
        /// Gets the system time in local time.
        /// </summary>
        /// <exception cref="UnsupportedPlatformException">The current platform is not supported.</exception>
        public static DateTime LocalTime
        {
            get { return SystemClock.UtcTime.ToLocalTime(); }
        }
    }
}