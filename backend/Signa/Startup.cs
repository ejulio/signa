using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.StaticFiles;
using Owin;
using Signa.ContentTypeProviders;

namespace Signa
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigurarResolvedorDeDependencias();
            ConfigurarCors(app);
            ConfigurarServidorDeArquivos(app);
            ConfigurarSignalR(app);
        }

        private void ConfigurarResolvedorDeDependencias()
        {
            var container = GlobalHost.DependencyResolver;
            var resolvedorDeDependencias = new ResolvedorDeDependencias(container);
            resolvedorDeDependencias.Configurar();
        }

        private void ConfigurarCors(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
        }

        private void ConfigurarServidorDeArquivos(IAppBuilder app)
        {
            app.UseFileServer();
            var options = new StaticFileOptions
            {
                ContentTypeProvider = new JsonContentTypeProvider()
            };
            app.UseStaticFiles(options);
        }

        private void ConfigurarSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}