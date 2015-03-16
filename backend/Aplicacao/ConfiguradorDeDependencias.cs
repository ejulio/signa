using Dominio.Algoritmos.Factories;
using Dominio.Dados;
using Dominio.Dados.Repositorio;
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
            container.Register(typeof(Hubs.Sinais),
                () => new Hubs.Sinais(repositorioFactory.CriarECarregarRepositorioDeSinais()));
        }

        private void ConfigurarReconhecedorDeSinaisEstaticos()
        {
            container.Register(typeof(SinaisEstaticosController),
                () =>
                    new SinaisEstaticosController(repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos(),
                        algoritmoFactory.CriarReconhecedorDeSinaisEstaticos()));

            container.Register(typeof(Hubs.SinaisEstaticos),
                () => new Hubs.SinaisEstaticos(container.Resolve<SinaisEstaticosController>()));
        }

        private void ConfigurarReconhecedorDeSinaisDinamicos()
        {
            container.Register(typeof(SinaisDinamicosController),
                () =>
                    new SinaisDinamicosController(repositorioFactory.CriarECarregarRepositorioDeSinaisDinamicos(),
                        geradorDeCaracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame(),
                        algoritmoFactory.CriarReconhecedorDeSinaisDinamicos(),
                        algoritmoFactory.CriarReconhecedorDeFramesDeSinaisDinamicos()));

            container.Register(typeof(Hubs.SinaisDinamicos),
                () => new Hubs.SinaisDinamicos(container.Resolve<SinaisDinamicosController>()));
        }

        private void ConfigurarJsonSerializerSettings()
        {
            var configuracoes = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            //container.Register(typeof(JsonSerializer), () => JsonSerializer.Create(configuracoes));
        }
    }
}