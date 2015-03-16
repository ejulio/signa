using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.StaticFiles;
using Owin;
using Signa.ContentTypeProviders;

namespace Signa
{
    public class Startup
    {
        private IAppBuilder app;

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
            var container = GlobalHost.DependencyResolver;
            var resolvedorDeDependencias = new ResolvedorDeDependencias(container);
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
            var configuracao = new HttpConfiguration();
            configuracao.Routes.Add("default", new HttpRoute("{controller}/{action}"));
            app.UseWebApi(configuracao);
        }
    }
}