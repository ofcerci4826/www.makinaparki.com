using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Vegatro.Core;

namespace Vegatro.NetCore.Utils
{
    /// <summary>
    /// Log Helper
    /// </summary>
    public class LogHelper
    {
        private static readonly string SessionId = "LogId";
        private static readonly string RequestId = "Token";

        /// <summary>
        /// Gets log file prefix
        /// For requests with session object checks for config file parameter (Logging:SessionId) 
        /// For sessionless requests checks for config file parameter (Logging:RequestId)
        /// If any of those not exists, generates unique string
        /// </summary>
        /// <returns></returns>
        public static string GetPrefix()
        {
            string userAgent = ContextObject.Current.Request.Headers.ContainsKey("User-Agent") ? ContextObject.Current.Request.Headers["User-Agent"].ToString() : "";
            string uid       = Security.Crypto.MD5(ContextObject.Current.Connection.RemoteIpAddress.ToString() + ContextObject.Current.Connection.RemotePort.ToString() + userAgent);

            StringValues stringValue = new StringValues();

            string sessionId = !string.IsNullOrEmpty(Config.Get("Logging:SessionId")) ? Config.Get("Logging:SessionId") : SessionId;
            string requestId = !string.IsNullOrEmpty(Config.Get("Logging:RequestId")) ? Config.Get("Logging:RequestId") : RequestId;

            if (!string.IsNullOrEmpty(ContextObject.Current.Session.GetString(sessionId)))
                uid = ContextObject.Current.Session.GetString(sessionId);
            else if (ContextObject.Current.Request.Query.TryGetValue(requestId, out stringValue) && !string.IsNullOrEmpty(Convert.ToString(stringValue)))
                uid = Convert.ToString(stringValue);

            return uid;
        }
    }
}
