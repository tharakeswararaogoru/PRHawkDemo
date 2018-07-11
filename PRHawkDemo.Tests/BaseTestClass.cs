using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Unity;

namespace PRHawkDemo.Tests
{
    /// <summary>
    /// Base class for tests that invoke Services. 
    /// common test setup tasks.
    /// </summary>
    public abstract class BaseTestClass
    {
        protected static UnityContainer _container;

        static BaseTestClass()
        {
        }

        /// <summary>
        /// DI container with the core services pre-registered.  Add your own real or mocked services
        /// at your discretion.
        /// </summary>
        protected static IUnityContainer Container => _container;

        protected BaseTestClass()
        {
            Init();
        }

        /// <summary>
        /// Initializes the DI container.
        /// </summary>
        protected void Init()
        {
            if (_container == null)
            {
                _container = new UnityContainer();
            }
            GitHubDemo.Tests.Bootstrapper.Initialise(ref _container);
        }
    }
}
