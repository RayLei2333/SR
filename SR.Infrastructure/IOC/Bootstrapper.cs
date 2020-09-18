using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace SR.Infrastructure.IOC
{
    public class Bootstrapper
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            Assembly[] controllers = new Assembly[] {
                Assembly.Load("SR")
            };
            builder.RegisterControllers(controllers);

            builder.RegisterAssemblyTypes(new Assembly[] {
                Assembly.Load("SR.Business"),
                Assembly.Load("SR.Business.Imp")
            }).AsImplementedInterfaces();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//这句最重要，提供注入点
        }
    }
}
