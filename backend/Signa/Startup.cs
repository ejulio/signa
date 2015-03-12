using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Signa.ContentTypeProviders;
using Signa.Dados;
using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos;

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

        private static void ConfigurarCors(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
        }

        private static void ConfigurarServidorDeArquivos(IAppBuilder app)
        {
            app.UseFileServer();
            var options = new StaticFileOptions
            {
                ContentTypeProvider = new JsonContentTypeProvider()
            };
            app.UseStaticFiles(options);
        }

        private static void ConfigurarSignalR(IAppBuilder app)
        {
            app.MapSignalR();
        }

        private static void ConfigurarResolvedorDeDependencias()
        {
            var container = GlobalHost.DependencyResolver;
            var resolvedorDeDependencias = new ResolvedorDeDependencias(container);
            resolvedorDeDependencias.Configurar();
        }

        
    }
}