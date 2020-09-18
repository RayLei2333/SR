using System.Configuration;

namespace SR.Infrastructure
{
    public static class AppConfig
    {

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

        public static string GetConnectionString(string name)
        {
            string connstr = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            return connstr;
        }
    }
}
