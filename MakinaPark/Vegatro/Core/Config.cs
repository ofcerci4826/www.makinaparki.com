using System;

namespace Vegatro.Core
{
    /// <summary>
    /// Configurations
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 
        /// </summary>
        public static Func<string, string> GetFunc { get; set; }

        /// <summary>
        /// Gets value of given key from configuration file
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <returns>Configuration value</returns>
        public static string Get(string key)
        {
            return GetFunc(key);
        }
    }
}
