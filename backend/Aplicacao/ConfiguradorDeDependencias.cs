using Dominio;
using Dominio.Algoritmos.Factories;
using Dominio.Gerenciamento;
using Dominio.Persistencia;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Aplicacao
{
    public class ConfiguradorDeDependencias
    {
        private readonly IDependencyResolver container;
        private static AlgoritmoDeReconhecimentoDeSinalFactory algoritmoFactory;
        private static IRepositorioFactory repositorioFactory;
        private static GeradorDeCaracteristicasFactory geradorDeCaracteristicasFactory;

        public ConfiguradorDeDependencias(IDependencyResolver container)
        {
            this.container = container;
            repositorioFactory = new RepositorioFactory(GerenciadorSinais.CaminhoDoArquivoDoRepositorio);
            geradorDeCaracteristicasFactory = new GeradorDeCaracteristicasFactory();
            algoritmoFactory = new AlgoritmoDeReconhecimentoDeSinalFactory(geradorDeCaracteristicasFactory);
        }

        public void Configurar()
        {
            ConfigurarJsonSerializerSettings();
            ConfigurarSinais();
            ConfigurarReconhecedorDeSinaisEstaticos();
            ConfigurarReconhecedorDeSinaisDinamicos();
            ConfigurarInicializadorDeAlgoritmosFacade();
        }

        private void ConfigurarSinais()
        {
            container.Register(typeof(Hubs.Sinais),
                () => new Hubs.Sinais(repositorioFactory.CriarECarregarRepositorioDeSinais()));
        }

        private void ConfigurarReconhecedorDeSinaisEstaticos()
        {
            container.Register(typeof(GerenciadorSinaisEstaticos),
                () =>
                    new GerenciadorSinaisEstaticos(repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos(),
                        algoritmoFactory.CriarReconhecedorDeSinaisEstaticos()));

            container.Register(typeof(Hubs.SinaisEstaticos),
                () => new Hubs.SinaisEstaticos(container.Resolve<GerenciadorSinaisEstaticos>()));
        }

        private void ConfigurarReconhecedorDeSinaisDinamicos()
        {
            container.Register(typeof(GerenciadorSinaisDinamicos),
                () =>
                    new GerenciadorSinaisDinamicos(repositorioFactory.CriarECarregarRepositorioDeSinaisDinamicos(),
                        geradorDeCaracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame(),
                        algoritmoFactory.CriarReconhecedorDeSinaisDinamicos(),
                        algoritmoFactory.CriarReconhecedorDeFramesDeSinaisDinamicos()));

            container.Register(typeof(Hubs.SinaisDinamicos),
                () => new Hubs.SinaisDinamicos(container.Resolve<GerenciadorSinaisDinamicos>()));
        }

        private void ConfigurarJsonSerializerSettings()
        {
            var configuracoes = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            //container.Register(typeof(JsonSerializer), () => JsonSerializer.Create(configuracoes));
        }

        private void ConfigurarInicializadorDeAlgoritmosFacade()
        {
            container.Register(typeof(InicializadorDeAlgoritmoFacade),
                () => new InicializadorDeAlgoritmoFacade(algoritmoFactory, repositorioFactory));
        }
    }
}