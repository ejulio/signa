using Microsoft.Owin.Cors;
using Owin;
using Signa.Data;

namespace Signa
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Configure();
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }

        private void Configure()
        {
            SignSamplesController.Instance.SamplesFilePath = "./data/sign-samples.json";
        }
    }
}