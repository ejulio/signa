using Dominio;
using Dominio.Algoritmos;
using Dominio.Algoritmos.Estatico;
using Dominio.Algoritmos.Factories;
using Dominio.Persistencia;
using Dominio.Sinais;
using Dominio.Sinais.Frames;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using Testes.Comum.Util;

namespace Testes.Integracao.Dominio.Algoritmos
{
    [TestClass]
    public class AlgoritmosTeste
    {
        private const string CaminhoDoArquivoDeDadosDeReconhecimento = "Integracao/JsonTestData/repositorio-sinais-teste-reconhecimento.json";
        private const string CaminhoDoArquivoDeDadosDeTreinamento = "Integracao/JsonTestData/repositorio-sinais-treinamento-reconhecimento.json";

        private IRepositorio<Sinal> repositorio;
        private AlgoritmoClassificacaoSinalFactory algoritmoFactory;
        private RepositorioFactory repositorioFactory;
        private CaracteristicasFactory caracteristicasFactory;

        [TestInitialize]
        public void setup()
        {
            caracteristicasFactory = new CaracteristicasFactory();
            repositorioFactory = new RepositorioFactory(CaminhoDoArquivoDeDadosDeTreinamento);
            algoritmoFactory = new AlgoritmoClassificacaoSinalFactory(caracteristicasFactory);
            var inicializadorDeAlgoritmo = new InicializadorDeAlgoritmoFacade(algoritmoFactory, repositorioFactory);

            inicializadorDeAlgoritmo.TreinarAlgoritmoClassificacaoSinaisEstaticos();
            inicializadorDeAlgoritmo.TreinarAlgoritmoClassificacaoSinaisDinamicos();

            repositorio = new RepositorioSinais(CaminhoDoArquivoDeDadosDeReconhecimento);
        }

        [TestMethod]
        public void reconhecendo_sinais_estaticos()
        {
            var repositorioSinaisEstaticos = new RepositorioSinaisEstaticos(repositorio);
            var algoritmo = algoritmoFactory.CriarClassificadorSinaisEstaticos();

            repositorioSinaisEstaticos.Carregar();

            ExecutarTestesDeReconhecimentoComRelatorio(algoritmo, repositorioSinaisEstaticos, repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos());
        }

        [TestMethod]
        public void reconhecendo_sinais_dinamicos()
        {
            var repositorioSinaisDinamicos = new RepositorioSinaisDinamicos(repositorio);
            var algoritmo = algoritmoFactory.CriarClassificadorSinaisDinamicos();

            repositorioSinaisDinamicos.Carregar();

            ExecutarTestesDeReconhecimentoComRelatorio(algoritmo, repositorioSinaisDinamicos, repositorioFactory.CriarECarregarRepositorioDeSinaisDinamicos());
        }

        [TestMethod]
        public void reconhecendo_sinais_frames_de_sinais_dinamicos()
        {
            var repositorioSinaisDinamicos = new RepositorioSinaisDinamicos(repositorio);
            var algoritmo = algoritmoFactory.CriarClassificadorFramesSinaisDinamicos();

            repositorioSinaisDinamicos.Carregar();

            ExecutarTestesDeReconhecimentoFramesComRelatorio(algoritmo, repositorioSinaisDinamicos, repositorioFactory.CriarECarregarRepositorioDeSinaisDinamicos());
        }

        private void ExecutarTestesDeReconhecimentoComRelatorio(IAlgoritmoClassificacaoSinais algoritmo, IRepositorio<Sinal> repositorioTestes, IRepositorio<Sinal> repositorioTreinamento)
        {
            var relatorio = new Relatorio();
            for (var i = 0; i < repositorioTestes.Quantidade; i++)
            {
                var sinal = repositorioTestes.BuscarPorIndice(i);
                sinal.IdNoAlgoritmo = repositorioTreinamento
                    .First(o => o.Descricao == sinal.Descricao)
                    .IdNoAlgoritmo;

                for (var j = 0; j < sinal.Amostras.Count; j++)
                {
                    var stopwatch = Stopwatch.StartNew();
                    var resultado = algoritmo.Classificar(sinal.Amostras[j]);
                    stopwatch.Stop();
                    if (resultado == sinal.IdNoAlgoritmo)
                        relatorio.AdicionarAcerto(sinal, stopwatch.ElapsedMilliseconds);
                    else
                        relatorio.AdicionarErro(sinal, repositorioTreinamento.BuscarPorIndice(resultado), j, stopwatch.ElapsedMilliseconds);
                }
            }

            relatorio.Imprimir();
        }

        private void ExecutarTestesDeReconhecimentoFramesComRelatorio(IAlgoritmoClassificacaoSinais algoritmo, IRepositorio<Sinal> repositorioTestes, IRepositorio<Sinal> repositorioTreinamento)
        {
            var caracteristicas = caracteristicasFactory.CriarGeradorDeCaracteristicasDeSinalEstaticoComTipoFrame();
            var relatorio = new Relatorio();
            for (var i = 0; i < repositorioTestes.Quantidade; i++)
            {
                var sinal = repositorioTestes.BuscarPorIndice(i);
                sinal.IdNoAlgoritmo = repositorioTreinamento
                    .First(o => o.Descricao == sinal.Descricao)
                    .IdNoAlgoritmo;

                for (var j = 0; j < sinal.Amostras.Count; j++)
                {
                    caracteristicas.PrimeiroFrame = null;
                    caracteristicas.TipoFrame = TipoFrame.Primeiro;
                    var stopwatch = Stopwatch.StartNew();
                    var resultado = algoritmo.Classificar(new[] { sinal.Amostras[j].First() });
                    stopwatch.Stop();
                    if (resultado == sinal.IdNoAlgoritmo)
                        relatorio.AdicionarAcerto(sinal, stopwatch.ElapsedMilliseconds, " - PRIMEIRO FRAME");
                    else
                    {
                        var indice = resultado >= repositorioTestes.Quantidade
                            ? resultado - repositorioTestes.Quantidade
                            : resultado;
                        relatorio.AdicionarErro(sinal, repositorioTreinamento.BuscarPorIndice(indice), j, stopwatch.ElapsedMilliseconds, " - PRIMEIRO FRAME");
                        var ddag = "";
                        foreach (var t in ((Svm)algoritmo).path)
                            ddag += string.Format("[{0}, {1}]", t.Item1, t.Item2);
                        relatorio.AdicionarObservacao(ddag);
                    }

                    caracteristicas.PrimeiroFrame = sinal.Amostras[j].First();
                    caracteristicas.TipoFrame = TipoFrame.Ultimo;
                    stopwatch = Stopwatch.StartNew();
                    resultado = algoritmo.Classificar(new[] { sinal.Amostras[j].Last() });
                    stopwatch.Stop();
                    if (resultado == sinal.IdNoAlgoritmo + repositorioTestes.Quantidade)
                        relatorio.AdicionarAcerto(sinal, stopwatch.ElapsedMilliseconds, " - ÚLTIMO FRAME");
                    else
                    {
                        var indice = resultado >= repositorioTestes.Quantidade
                            ? resultado - repositorioTestes.Quantidade
                            : resultado;
                        relatorio.AdicionarErro(sinal, repositorioTreinamento.BuscarPorIndice(indice), j, stopwatch.ElapsedMilliseconds, " - ÚLTIMO FRAME");
                        var ddag = "";
                        foreach (var t in ((Svm)algoritmo).path)
                            ddag += string.Format("[{0}, {1}]", t.Item1, t.Item2);
                        relatorio.AdicionarObservacao(ddag);
                    }
                }
            }

            relatorio.Imprimir();
        }
    }
}
