namespace Petroules.Synteza
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using Petroules.Synteza.Properties;

    /// <summary>
    /// Provides helper methods for enumeration extension classes.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Parses an enumeration constant from a string. If no equivalent is found, and <paramref name="defaultEnum"/> is non-null,
        /// <paramref name="defaultEnum"/> will be returned. Otherwise, an exception will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration being parsed.</typeparam>
        /// <param name="enumConstant">The string to parse.</param>
        /// <param name="defaultEnum">The default constant to return on error or failure. <c>null</c> to throw an exception instead.</param>
        /// <returns>See summary.</returns>
        public static T ParseFromString<T>(string enumConstant, T? defaultEnum) where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), enumConstant);
            }
            catch (ArgumentNullException e)
            {
                if (defaultEnum.HasValue)
                {
                    return defaultEnum.Value;
                }
                else
                {
                    throw e;
                }
            }
            catch (ArgumentException e)
            {
                if (defaultEnum.HasValue)
                {
                    return defaultEnum.Value;
                }
                else
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets a sorted list of the enumeration constants as a
        /// collection of strings with the same names. The constant
        /// representing none, null, empty or default will be placed
        /// at the top of the list, the rest following alphabetically.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration being retrieved.</typeparam>
        /// <param name="defaultEnum">The constant representing none, null, empty or default (will be placed at the top).</param>
        /// <returns>See summary.</returns>
        public static string[] GetSortedList<T>(T defaultEnum) where T : struct
        {
            string[] enumNames = Enum.GetNames(typeof(T));
            Array.Sort(enumNames);

            StringCollection strings = new StringCollection();
            strings.AddRange(enumNames);

            strings.Remove(defaultEnum.ToString());
            strings.Insert(0, defaultEnum.ToString());

            strings.CopyTo(enumNames, 0);

            return enumNames;
        }

        /// <summary>
        /// Determines whether a flag is set on the enumeration constant.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="tool">The enumeration constant to check.</param>
        /// <param name="value">The flag to check.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        public static bool IsSet<T>(this T tool, T value) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException(Resources.NotAnEnumeration);
            }

            if (Enum.GetUnderlyingType(typeof(T)) == typeof(ulong))
            {
                return (Convert.ToUInt64(tool, CultureInfo.InvariantCulture) & Convert.ToUInt64(value, CultureInfo.InvariantCulture)) == Convert.ToUInt64(value, CultureInfo.InvariantCulture);
            }
            else
            {
                return (Convert.ToInt64(tool, CultureInfo.InvariantCulture) & Convert.ToInt64(value, CultureInfo.InvariantCulture)) == Convert.ToInt64(value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Determines whether a flag is not set on the enumeration constant.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="tool">The enumeration constant to check.</param>
        /// <param name="value">The flag to check.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        public static bool IsNotSet<T>(this T tool, T value) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException(Resources.NotAnEnumeration);
            }

            return !tool.IsSet(value);
        }

        /// <summary>
        /// Sets a flag on the enumeration constant and returns the new value.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="tool">The enumeration constant to set the flag on.</param>
        /// <param name="value">The flag to set.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        public static T Set<T>(this T tool, T value) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException(Resources.NotAnEnumeration);
            }

            if (Enum.GetUnderlyingType(typeof(T)) == typeof(ulong))
            {
                return (T)Enum.ToObject(typeof(T), (Convert.ToUInt64(tool, CultureInfo.InvariantCulture) | Convert.ToUInt64(value, CultureInfo.InvariantCulture)));
            }
            else
            {
                return (T)Enum.ToObject(typeof(T), (Convert.ToInt64(tool, CultureInfo.InvariantCulture) | Convert.ToInt64(value, CultureInfo.InvariantCulture)));
            }
        }

        /// <summary>
        /// Clears a flag from the enumeration constant and returns the new value.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="tool">The enumeration constant to clear the flag from.</param>
        /// <param name="value">The flag to clear.</param>
        /// <returns>See summary.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not an enumeration.</exception>
        public static T Clear<T>(this T tool, T value) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException(Resources.NotAnEnumeration);
            }

            if (Enum.GetUnderlyingType(typeof(T)) == typeof(ulong))
            {
                return (T)Enum.ToObject(typeof(T), (Convert.ToUInt64(tool, CultureInfo.InvariantCulture) & ~Convert.ToUInt64(value, CultureInfo.InvariantCulture)));
            }
            else
            {
                return (T)Enum.ToObject(typeof(T), (Convert.ToInt64(tool, CultureInfo.InvariantCulture) & ~Convert.ToInt64(value, CultureInfo.InvariantCulture)));
            }
        }
    }
}
