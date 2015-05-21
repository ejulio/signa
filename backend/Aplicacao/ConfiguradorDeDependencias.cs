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
        private static AlgoritmoClassificacaoSinalFactory algoritmoFactory;
        private static IRepositorioFactory repositorioFactory;
        private static CaracteristicasFactory caracteristicasFactory;

        public ConfiguradorDeDependencias(IDependencyResolver container)
        {
            this.container = container;
            repositorioFactory = new RepositorioFactory(GerenciadorSinais.CaminhoDoArquivoDoRepositorio);
            caracteristicasFactory = new CaracteristicasFactory();
            algoritmoFactory = new AlgoritmoClassificacaoSinalFactory(caracteristicasFactory);
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
                        caracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame(),
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