﻿using Aplicacao.ContentTypeProviders;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.StaticFiles;
using Microsoft.Practices.Unity;
using Owin;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Aplicacao
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
            var resolvedorDeDependenciasAspNet = new ConfiguradorDeDependencias(container);
            resolvedorDeDependenciasAspNet.Configurar();

            var resolvedorDeDependenciasSignalR = new ConfiguradorDeDependencias(GlobalHost.DependencyResolver);
            resolvedorDeDependenciasSignalR.Configurar();
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
            var configuracao = new HttpConfiguration
            {
                DependencyResolver = container
            };
            configuracao.Routes.Add("default", new HttpRoute("{controller}/{action}"));
            app.UseWebApi(configuracao);
        }
    }
}