using System.Collections.Generic;
using System.Web.Mvc;

namespace SR.Infrastructure.IOC
{
    public class ObjectFactory
    {
        public static T GetObject<T>()
        {
            //T result = (T)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(T));
            T result = DependencyResolver.Current.GetService<T>();
            return result;
        }

        public static IEnumerable<T> GetObjects<T>()
        {
            IEnumerable<T> result = DependencyResolver.Current.GetServices<T>();
            return result;
        }
    }
}
