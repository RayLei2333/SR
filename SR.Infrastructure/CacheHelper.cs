using System.Linq;

namespace SR.Infrastructure
{
    public static class CacheHelper
    {
        public static void SetAppliation(string key, object data)
        {
            if (System.Web.HttpContext.Current.Application.AllKeys.Contains(key))
                System.Web.HttpContext.Current.Application.Remove(key);
            System.Web.HttpContext.Current.Application.Add(key, data);
        }

        public static object GetApplication(string key)
        {
            object data = System.Web.HttpContext.Current.Application.Get(key);
            return data;
        }

        public static T GetApplication<T>(string key)
        {
            object data = System.Web.HttpContext.Current.Application.Get(key);
            T ins = (T)data;
            return ins;
        }

        public static void SetSession(string key, object value)
        {
            System.Web.HttpContext.Current.Session[key] = value;
        }

        public static object GetSession(string key)
        {
            object data = System.Web.HttpContext.Current.Session[key];
            return data;
        }

        public static T GetSession<T>(string key)
        {
            object data = System.Web.HttpContext.Current.Session[key];
            T ins = (T)data;
            return ins;
        }

        public static void RemoveSession(string key)
        {
            System.Web.HttpContext.Current.Session.Remove(key);
        }
    }
}
