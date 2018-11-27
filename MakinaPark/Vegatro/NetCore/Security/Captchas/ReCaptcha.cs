using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Vegatro.Core;

namespace Vegatro.Security.Captchas
{
    /// <summary>
    /// Google ReCaptcha
    /// </summary>
    public class ReCaptcha
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("success")]
        public string Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }

        /// <summary>
        /// Validates google recaptcha is valid
        /// </summary>
        /// <param name="recaptchaResponse"></param>
        /// <returns></returns>
        public static string Validate(string recaptchaResponse)
        {
            try
            {
                return JsonConvert.DeserializeObject<ReCaptcha>(new System.Net.WebClient().DownloadString(string.Format(Config.Get("Security:RecaptchaUrl"), Config.Get("Security:RecaptchaSecret"), recaptchaResponse))).Success.ToLower();
            } catch(Exception) {
                return "false";
            }
        }
    }
}
