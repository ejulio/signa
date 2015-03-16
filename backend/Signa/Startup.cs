using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.StaticFiles;
using Microsoft.Practices.Unity;
using Owin;
using Signa.ContentTypeProviders;

namespace Signa
{
    public class Startup
    {
        private IAppBuilder app;
        private ResolvedorDeDependenciasUnity container;

        public void Configuration(IAppBuilder app)
        {
            this.app = app;

            ConfigurarResolvedorDeDependencias();
            UsarCors();
            UsarServidorDeArquivos();
            UsarSignalR();
            UsarWebApi();
        }

        private void ConfigurarResolvedorDeDependencias()
        {
            container = new ResolvedorDeDependenciasUnity(new UnityContainer());
            var resolvedorDeDependencias = new ConfiguradorDeDependencias(GlobalHost.DependencyResolver);
            resolvedorDeDependencias.Configurar();
        }

        private void UsarCors()
        {
            app.UseCors(CorsOptions.AllowAll);
        }

        private void UsarServidorDeArquivos()
        {
            app.UseFileServer();
            var options = new StaticFileOptions
            {
                ContentTypeProvider = new JsonContentTypeProvider()
            };
            app.UseStaticFiles(options);
        }

        private void UsarSignalR()
        {
            app.MapSignalR();
        }

        private void UsarWebApi()
        {
            var resolvedorDeDependencias = new ConfiguradorDeDependencias(container);
            resolvedorDeDependencias.Configurar();
            var configuracao = new HttpConfiguration();
            configuracao.DependencyResolver = container;
            configuracao.Routes.Add("default", new HttpRoute("{controller}/{action}"));
            app.UseWebApi(configuracao);
        }
    }
}