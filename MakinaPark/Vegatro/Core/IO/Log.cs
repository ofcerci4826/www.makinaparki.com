using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Vegatro.Core.IO
{
    /// <summary>
    /// Logging, Tracing Operations
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Traces given message
        /// If platform is OSX, this method adds OSX: to begin of pathKeyPrefix variable
        /// If platform is Linux, this method adds Linux: to begin of pathKeyPrefix variable
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="prefix">Log file prefix</param>
        /// <param name="pathKeyPrefix">configuration prefix</param>
        public static void Trace(string message, string prefix, string pathKeyPrefix = "Trace")
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) 
                pathKeyPrefix = "OSX:" + pathKeyPrefix;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                pathKeyPrefix = "Linux:" + pathKeyPrefix;

            string appName      = Config.Get("App:Name");
            string appVersion   = Config.Get("App:Version");
            string traceEnabled = string.IsNullOrEmpty(Config.Get("Logging:" + pathKeyPrefix + "Enabled")) ? "1" : Config.Get("Logging:" + pathKeyPrefix + "Enabled");
            string path         = string.IsNullOrEmpty(Config.Get("Logging:" + pathKeyPrefix + "Path")) ? PathDefault() : Config.Get("Logging:" + pathKeyPrefix + "Path");

            // Check tracing enabled
            if (string.IsNullOrEmpty(traceEnabled) || "0".Equals(traceEnabled))
                return;

            if (string.IsNullOrEmpty(path))
                return;

            DateTime now = DateTime.Now;

            // If path includes date hirerarchy or dependency replace with datetime info
            path = path
                .Replace("{appName}", appName)
                .Replace("{folder}", pathKeyPrefix)
                .Replace("{uid}", prefix)
                .Replace("{yyyy}", now.ToString("yyyy"))
                .Replace("{MM}", now.ToString("MM"))
                .Replace("{dd}", now.ToString("dd"));

            File.WriteLine(path, "[T][" + appVersion + "][" + now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "][M] " + message);
        }

        /// <summary>
        /// Logs given error string and stack trace
        /// If platform is OSX, this method adds OSX: to begin of pathKeyPrefix variable
        /// If platform is Linux, this method adds Linux: to begin of pathKeyPrefix variable
        /// </summary>
        /// <param name="error">error message</param>
        /// <param name="stackTrace">stack trace</param>
        /// <param name="prefix">Log file prefix</param>
        /// <param name="pathKeyPrefix">configuration prefix</param>
        public static void Error(string error, string stackTrace, string prefix, string pathKeyPrefix = "Error")
        {
            string errorId = Guid.NewGuid().ToString();

            Trace("[E] [" + errorId + "] " + error, prefix);
            Trace("[E] [" + errorId + "] " + error + (!string.IsNullOrEmpty(stackTrace) ? " [ST]-" + stackTrace : ""), prefix, pathKeyPrefix);
        }

        /// <summary>
        /// Logs exception
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="prefix">Log file prefix</param>
        public static void Error(Exception exception, string prefix)
        {
            Error(exception.Message, exception.StackTrace, prefix);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string PathDefault()
        {
            return Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)) + "\\Apps\\{appName}\\{folder}\\{yyyy}\\{MM}\\{dd}\\{uid}.vtrace";
        }
    }
}
