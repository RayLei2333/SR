using System.Configuration;

namespace SR.Infrastructure
{
    public static class AppConfig
    {
        /// <summary>
        /// 企业号ID
        /// </summary>
        public static string CorpId
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("corpId");
            }
        }

        /// <summary>
        /// 企业号Secret
        /// </summary>
        public static string CorpSecret
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("corpSecret");
            }
        }


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
