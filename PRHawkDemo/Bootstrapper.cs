using Services.Manager;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace PRHawkDemo
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IGitHubManager, GitHubManager>();
            container.RegisterType<IHttpHandler, HttpHandler>();

            return container;
        }
    }
}