using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Signa.Dados;
using Signa.Dados.Repositorio;
using Signa.Dominio.Algoritmos;

namespace Signa
{
    public class ResolvedorDeDependencias
    {
        private readonly IDependencyResolver container;
        private static AlgoritmoDeReconhecimentoDeSinalFactory algoritmoFactory;
        private static IRepositorioFactory repositorioFactory;
        public ResolvedorDeDependencias(IDependencyResolver container)
        {
            this.container = container;
            repositorioFactory = new RepositorioFactory(SinaisController.CaminhoDoArquivoDoRepositorio);
            algoritmoFactory = new AlgoritmoDeReconhecimentoDeSinalFactory();
        }

        public void Configurar()
        {
            ConfigurarJsonSerializerSettings();
            ConfigurarSequenciaDeSinais();
            ConfigurarReconhecedorDeSinaisEstaticos();
            ConfigurarReconhecedorDeSinaisDinamicos();
        }

        private void ConfigurarSequenciaDeSinais()
        {
            container.Register(typeof(Hubs.SequenciaDeSinais),
                () => new Hubs.SequenciaDeSinais(repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos()));
        }

        private void ConfigurarReconhecedorDeSinaisEstaticos()
        {
            container.Register(typeof(SinaisEstaticosController),
                () =>
                    new SinaisEstaticosController(repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos(),
                        algoritmoFactory.CriarReconhecedorDeSinaisEstaticos()));

            container.Register(typeof(Hubs.ReconhecedorDeSinaisEstaticos),
                () => new Hubs.ReconhecedorDeSinaisEstaticos(container.Resolve<SinaisEstaticosController>()));
        }

        private void ConfigurarReconhecedorDeSinaisDinamicos()
        {
            container.Register(typeof(SinaisDinamicosController),
                () =>
                    new SinaisDinamicosController(repositorioFactory.CriarECarregarRepositorioDeSinaisDinamicos(),
                        null,
                        algoritmoFactory.CriarReconhecedorDeSinaisDinamicos(),
                        algoritmoFactory.CriarReconhecedorDeSinaisEstaticos()));

            container.Register(typeof(Hubs.ReconhecedorDeSinaisDinamicos),
                () => new Hubs.ReconhecedorDeSinaisDinamicos(container.Resolve<SinaisDinamicosController>()));
        }

        private void ConfigurarJsonSerializerSettings()
        {
            GlobalHost.DependencyResolver.Register(typeof(JsonSerializerSettings), () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}