using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Owin.Security.Provider;
using Microsoft.Practices.Unity;
using IDependencyResolver = Microsoft.AspNet.SignalR.IDependencyResolver;

namespace Signa
{
    public class ResolvedorDeDependenciasUnity : IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IUnityContainer container;

        public ResolvedorDeDependenciasUnity(IUnityContainer container)
        {
            this.container = container;
        }

        public void Dispose()
        {
            container.Dispose();
        }

        object IDependencyResolver.GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        IEnumerable<object> IDependencyScope.GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            var container = this.container.CreateChildContainer();
            return new ResolvedorDeDependenciasUnity(container);
        }

        object IDependencyScope.GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        IEnumerable<object> IDependencyResolver.GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType);
        }

        public void Register(Type serviceType, Func<object> activator)
        {
            container.RegisterInstance(serviceType, activator());
        }

        public void Register(Type serviceType, IEnumerable<Func<object>> activators)
        {
            foreach (var activator in activators)
            {
                container.RegisterInstance(serviceType, activator());
            }
        }
    }
}