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
            app.UseCors(CorsOptions.AllowAll);

            app.UseFileServer();
            var options = new StaticFileOptions
            {
                ContentTypeProvider = new JsonContentTypeProvider()
            };
            app.UseStaticFiles(options);
            
            app.MapSignalR();
        }
    }
}