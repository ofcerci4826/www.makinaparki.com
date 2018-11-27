using System;
using Vegatro.Core.IO;

namespace Vegatro.Core
{
    /// <summary>
    /// Base class
    /// </summary>
    public class VegatroBase
    {
        /// <summary>
        /// File prefix for tracing end error logging actions
        /// </summary>
        public string LogPrefix { get; set; }

        /// <summary>
        /// Error callback
        /// </summary>
        public Action<Exception> OnError { get; set; }

        /// <summary>
        /// Tracing function
        /// </summary>
        /// <param name="message"></param>
        public void Trace(string message)
        {
            Log.Trace(message, LogPrefix);
        }

        /// <summary>
        /// Error tracing function
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex)
        {
            Log.Error(ex, LogPrefix);
            OnError?.Invoke(ex);
        }
    }
}
