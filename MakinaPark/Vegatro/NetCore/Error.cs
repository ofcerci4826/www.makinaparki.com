namespace Vegatro.NetCore
{
    /// <summary>
    /// Error enums for diagnostic module
    /// </summary>
    public class Error
    {
        /// <summary>
        /// No internet connection
        /// </summary>
        public const string NO_INTERNET = "ERR1000";

        /// <summary>
        /// Couldn't connect to web service
        /// </summary>
        public const string WEB_SERVICE_CONNECT    = "ERR1001";

        /// <summary>
        /// Couldn't disconnect from web service
        /// </summary>
        public const string WEB_SERVICE_DISCONNECT = "ERR1002";

        /// <summary>
        /// Unexpected error
        /// </summary>
        public const string UNEXPECTED = "ERR5000";
    }
}
