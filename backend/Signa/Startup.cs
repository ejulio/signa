using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Signa.ContentTypeProviders;
using Signa.Data;

namespace Signa
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureJsonSerializerSettings();


            var repository = new SignRepository(StaticSignController.SignSamplesFilePath);
            GlobalHost.DependencyResolver.Register(typeof(Hubs.Sign), () => new Hubs.Sign(repository));

            app.UseCors(CorsOptions.AllowAll);

            app.UseFileServer();
            var options = new StaticFileOptions
            {
                ContentTypeProvider = new JsonContentTypeProvider()
            };
            app.UseStaticFiles(options);
            
            app.MapSignalR();
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