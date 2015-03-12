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
        private static GeradorDeCaracteristicasFactory geradorDeCaracteristicasFactory;

        public ResolvedorDeDependencias(IDependencyResolver container)
        {
            this.container = container;
            repositorioFactory = new RepositorioFactory(SinaisController.CaminhoDoArquivoDoRepositorio);
            geradorDeCaracteristicasFactory = new GeradorDeCaracteristicasFactory();
            algoritmoFactory = new AlgoritmoDeReconhecimentoDeSinalFactory(geradorDeCaracteristicasFactory);
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
                        geradorDeCaracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame(),
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