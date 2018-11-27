using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Collections.Generic;
using Vegatro.Database;
using Vegatro.Core;
using Vegatro.Core.IO;

namespace Vegatro.NetCore.Utils
{
    /// <summary>
    /// Security Helper
    /// </summary>
    public class SecurityHelper
    {
        /// <summary>
        /// Logs request ip, port and url informations if enabled from config file (Logging:EnableRequestLog)
        /// </summary>
        public static void RequestLog()
        {
            string ip        = ContextObject.Current.Connection.RemoteIpAddress.ToString();
            string port      = ContextObject.Current.Connection.RemotePort.ToString();

            string userAgent = (ContextObject.Current.Request.Headers != null && ContextObject.Current.Request.Headers.ContainsKey("User-Agent") 
                ?  ContextObject.Current.Request.Headers["User-Agent"].ToString() : "");

            string url       = ContextObject.Current.Request.Path.Value.ToString();
            string method    = ContextObject.Current.Request.Method;

            if (!"1".Equals(Config.Get("Logging:EnableRequestLog")))
                return;

            Log.Trace(ip + ":" + port + " HTTP " + method.ToUpper() + " " + url, LogHelper.GetPrefix(), "RequestLog");
        }

        /// <summary>
        /// Checks token is valid or not then checks if user has permission to that url
        /// (EXEC sp_token_and_auth_control 'token', 'controller' 'action', 'type', refId)
        /// </summary>
        /// <param name="token">Auth Token</param>
        /// <param name="controller">MVC Controller</param>
        /// <param name="action">MVC Action</param>
        /// <param name="type">Action related table type</param>
        /// <param name="refId">Action related Id</param>
        /// <returns>String result (SUCCEED, INVALID_TOKEN, NOT_AUTHORIZED)</returns>
        public static string TokenAndAuthControl(string token, string controller, string action, string type = null, long? refId = null)
        {
            return Sql.GetInstance().Get("sp_token_and_auth_control", new List<object> {
                token,
                controller,
                action,
                type,
                refId
            }, (row) =>
            {
                return row.GetString("Result");
            });
        }

        /// <summary>
        /// Redirects to route or returns json response
        /// </summary>
        /// <param name="filterContext">FilterContext</param>
        /// <param name="controller">MVC Controller to Redirect</param>
        /// <param name="action">MVC Action to Redirect</param>
        /// <param name="status">Status Code</param>
        public static void Redirect(ActionExecutingContext filterContext, string controller, string action, int status)
        {
            if (!IsAjaxRequest(filterContext.HttpContext.Request))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                            { "controller", controller },
                            { "action",     action }
                    });
                return;
            }

            ContentResult result = new ContentResult();
            result.Content = JsonConvert.SerializeObject(new { Status = status, Description = action + " " + controller });

            filterContext.Result = result;
        }

        /// <summary>
        /// Determines whether the specified HTTP request is an AJAX request.
        /// Checks X-Requested-With == 'XMLHttpRequest' or V-Ajax == 'True'
        /// </summary>
        /// 
        /// <returns>
        /// true if the specified HTTP request is an AJAX request; otherwise, false.
        /// </returns>
        /// <param name="request">The HTTP request.</param><exception cref="T:System.ArgumentNullException">The <paramref name="request"/> parameter is null (Nothing in Visual Basic).</exception>
        public static bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
                return false;

            if (request.Headers == null)
                return false;

            if (request.Headers.ContainsKey("X-Requested-With") && request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return true;

            if (request.Headers.ContainsKey("V-Ajax") && request.Headers["V-Ajax"] == "True")
                return true;

            return false;
        }
    }
}