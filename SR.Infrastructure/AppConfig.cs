using System.Configuration;

namespace SR.Infrastructure
{
    public static class AppConfig
    {

        public static string DB
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SR"].ConnectionString;
            }
        }

        /// <summary>
        /// Get appSettings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            string result = ConfigurationManager.AppSettings.Get(key);
            return result;
        }
    }
}
