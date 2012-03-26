namespace Petroules.Synteza.Threading
{
    using System;
    using System.Reflection;
    using System.Threading;
    using Petroules.Synteza.Properties;

    /// <summary>
    /// Provides methods to assist with restricting an application to a single running instance.
    /// </summary>
    public static class SingleInstance
    {
        /// <summary>
        /// Mutex object to check if this program is already running.
        /// </summary>
        private static Mutex mutex;

        /// <summary>
        /// Determines if the process is already running.
        /// </summary>
        /// <returns>Whether the process is already running.</returns>
        /// <exception cref="System.InvalidOperationException">The method has already been called.</exception>
        public static bool ProcessRunning()
        {
            if (SingleInstance.mutex != null)
            {
                throw new InvalidOperationException(Resources.MethodAlreadyCalled);
            }

            // This must be false in .NET 2.0, or the Mutex constructor will block and throw
            // an exception if another instance of the program is running and then shut down
            bool createdNew = false;

            SingleInstance.mutex = new Mutex(false, Assembly.GetEntryAssembly().FullName, out createdNew);

            return !createdNew;
        }
    }
}
