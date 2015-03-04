using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Signa.ContentTypeProviders;
using Signa.Data;
using Signa.Data.Repository;
using Signa.Domain.Algorithms;

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

        private static SignRecognitionAlgorithmFactory algorithmFactory;
        private static IRepositoryFactory repositoryFactory;
        private static void ConfigureHubs()
        {
            repositoryFactory = new RepositoryFactory(StaticSignController.SignSamplesFilePath);

            algorithmFactory = new SignRecognitionAlgorithmFactory();

            var container = GlobalHost.DependencyResolver;

            container.Register(typeof(Hubs.SignSequence),
                () => new Hubs.SignSequence(repositoryFactory.CreateAndLoadStaticSignRepository()));

            container.Register(typeof(StaticSignController),
                () => new StaticSignController(repositoryFactory.CreateAndLoadStaticSignRepository(), algorithmFactory.CreateStaticSignRecognizer()));

            container.Register(typeof(Hubs.StaticSignRecognizer), 
                () => new Hubs.StaticSignRecognizer(container.Resolve<StaticSignController>()));
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