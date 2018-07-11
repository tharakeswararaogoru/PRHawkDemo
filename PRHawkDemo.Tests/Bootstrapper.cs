using Moq;
using Services.Manager;
using System.Net.Http;
using Unity;

namespace GitHubDemo.Tests
{
    public static class Bootstrapper
    {
        /// <summary>
        /// Registers all dependent services, mock data
        /// </summary>
        /// <param name="container"></param>
        public static void Initialise(ref UnityContainer container)
        {
            BuildUnityContainer(ref container);

            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            
        }

        private static IUnityContainer BuildUnityContainer(ref UnityContainer container)
        {
            container.RegisterType<IGitHubManager, GitHubManager>();
            container.RegisterType<IHttpHandler, HttpHandler>();


            container.RegisterInstance<IGitHubManager>(new Mock<IGitHubManager>(MockBehavior.Strict).Object);
            container.RegisterInstance<IHttpHandler>(new Mock<IHttpHandler>(MockBehavior.Strict).Object);

            return container;
        }
    }
}