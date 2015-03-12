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
            ConfigureJsonSerializerSettings();
            ConfigureHubs();

            app.UseCors(CorsOptions.AllowAll);

            app.UseFileServer();
            var options = new StaticFileOptions
            {
                ContentTypeProvider = new JsonContentTypeProvider()
            };
            app.UseStaticFiles(options);
            
            app.MapSignalR();
        }

        private static AlgoritmoDeReconhecimentoDeSinalFactory algorithmFactory;
        private static IRepositorioFactory repositorioFactory;
        private static void ConfigureHubs()
        {
            repositorioFactory = new RepositorioFactory(SinaisEstaticosController.CaminhoDoArquivoDoRepositorio);

            algorithmFactory = new AlgoritmoDeReconhecimentoDeSinalFactory();

            var container = GlobalHost.DependencyResolver;

            container.Register(typeof(Hubs.SequenciaDeSinais),
                () => new Hubs.SequenciaDeSinais(repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos()));

            container.Register(typeof(SinaisEstaticosController),
                () => new SinaisEstaticosController(repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos(), algorithmFactory.CriarReconhecedorDeSinaisEstaticos()));

            container.Register(typeof(Hubs.ReconhecedorDeSinaisEstaticos), 
                () => new Hubs.ReconhecedorDeSinaisEstaticos(container.Resolve<SinaisEstaticosController>()));
        }

        private static void ConfigureJsonSerializerSettings()
        {
            GlobalHost.DependencyResolver.Register(typeof (JsonSerializerSettings), () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}