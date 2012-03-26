namespace Petroules.Synteza.Native
{
    using System;
    using System.Linq;

    /// <summary>
    /// Provides utility methods related to cross-platform interoperability.
    /// </summary>
    public static class PlatformUtilities
    {
        /// <summary>
        /// Gets a value indicating whether this application is running on the Mono runtime.
        /// </summary>
        /// <returns>See summary.</returns>
        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        /// <summary>
        /// Throws an <see cref="UnsupportedPlatformException"/> if the current platform is not one of the platforms specified by <paramref name="supportedPlatforms"/>.
        /// </summary>
        /// <param name="supportedPlatforms">The platforms on which the exception will not be thrown.</param>
        /// <exception cref="UnsupportedPlatformException">The current platform is not one of the platforms specified by <paramref name="supportedPlatforms"/>.</exception>
        public static void ThrowIfUnsupported(params PlatformID[] supportedPlatforms)
        {
            if (!supportedPlatforms.Contains(Environment.OSVersion.Platform))
            {
                throw PlatformUtilities.GetUnsupportedException(supportedPlatforms);
            }
        }

        /// <summary>
        /// Gets an <see cref="UnsupportedPlatformException"/> initialized with the supported platforms specified by <paramref name="supportedPlatforms"/>.
        /// </summary>
        /// <param name="supportedPlatforms">The platforms on which the exception does not apply.</param>
        public static UnsupportedPlatformException GetUnsupportedException(params PlatformID[] supportedPlatforms)
        {
            return new UnsupportedPlatformException(supportedPlatforms);
        }
    }
}
