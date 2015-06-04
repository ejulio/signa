using Dominio;
using Dominio.Algoritmos;
using Dominio.Algoritmos.Factories;
using Dominio.Persistencia;
using Dominio.Sinais;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;

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

        [TestInitialize]
        public void setup()
        {
            var geradorDeCaracteristicasFactory = new CaracteristicasFactory();
            repositorioFactory = new RepositorioFactory(CaminhoDoArquivoDeDadosDeTreinamento);
            algoritmoFactory = new AlgoritmoClassificacaoSinalFactory(geradorDeCaracteristicasFactory);
            var inicializadorDeAlgoritmo = new InicializadorDeAlgoritmoFacade(algoritmoFactory, repositorioFactory);

            inicializadorDeAlgoritmo.TreinarAlgoritmoDeReconhecimentoDeSinaisEstaticos();
            inicializadorDeAlgoritmo.TreinarAlgoritmoDeReconhecimentoDeSinaisDinamicos();

            repositorio = new RepositorioSinais(CaminhoDoArquivoDeDadosDeReconhecimento);
        }

        [TestMethod, Ignore]
        public void reconhecendo_sinais_estaticos()
        {
            var repositorioSinaisEstaticos = new RepositorioSinaisEstaticos(repositorio);
            var algoritmo = algoritmoFactory.CriarReconhecedorDeSinaisEstaticos();

            repositorioSinaisEstaticos.Carregar();

            ExecutarTestesDeReconhecimentoComRelatorio(algoritmo, repositorioSinaisEstaticos, repositorioFactory.CriarECarregarRepositorioDeSinaisEstaticos());
        }

        [TestMethod, Ignore]
        public void reconhecendo_sinais_dinamicos()
        {
            var repositorioSinaisDinamicos = new RepositorioSinaisDinamicos(repositorio);
            var algoritmo = algoritmoFactory.CriarReconhecedorDeSinaisDinamicos();

            repositorioSinaisDinamicos.Carregar();

            ExecutarTestesDeReconhecimentoComRelatorio(algoritmo, repositorioSinaisDinamicos, repositorioFactory.CriarECarregarRepositorioDeSinaisDinamicos());
        }

        [TestMethod, Ignore]
        public void reconhecendo_sinais_frames_de_sinais_dinamicos()
        {
            var repositorioSinaisDinamicos = new RepositorioSinaisDinamicos(repositorio);
            var algoritmo = algoritmoFactory.CriarReconhecedorDeFramesDeSinaisDinamicos();

            repositorioSinaisDinamicos.Carregar();

            ExecutarTestesDeReconhecimentoComRelatorio(algoritmo, repositorioSinaisDinamicos, repositorioFactory.CriarECarregarRepositorioDeSinaisDinamicos());
        }

        private void ExecutarTestesDeReconhecimentoComRelatorio(IAlgoritmoClassificacaoSinais algoritmo, IRepositorio<Sinal> repositorioTestes, IRepositorio<Sinal> repositorioTreinamento)
        {
            int totalAcertos = 0;
            int totalErros = 0;
            StringBuilder acertos = new StringBuilder();
            StringBuilder erros = new StringBuilder();
            for (var i = 0; i < repositorioTestes.Quantidade; i++)
            {
                var sinal = repositorioTestes.BuscarPorIndice(i);
                var indiceDoSinalParaOAlgoritmo = repositorioTreinamento
                    .Select((s, index) => new { Descricao = s.Descricao, Indice = index })
                    .First(o => o.Descricao == sinal.Descricao)
                    .Indice;

                Console.WriteLine("{0}({1})", sinal.Descricao, repositorioTreinamento
                    .First(o => o.Descricao == sinal.Descricao).Amostras.Count);

                for (var j = 0; j < sinal.Amostras.Count; j++)
                {
                    var resultado = algoritmo.Classificar(sinal.Amostras[j]);

                    if (resultado == indiceDoSinalParaOAlgoritmo)
                    {
                        acertos.AppendFormat("{0}({1}), ", sinal.Descricao, indiceDoSinalParaOAlgoritmo);
                        totalAcertos++;
                    }
                    else
                    {
                        Console.WriteLine("ERRO");
                        erros.AppendFormat(
                            "Índice da amostra: {2}{0}Esperado: {3} - {1}{0}Reconhecido: {4} - {6}{0}{5}",
                            Environment.NewLine,
                            sinal.Descricao,
                            j,
                            indiceDoSinalParaOAlgoritmo,
                            resultado,
                            String.Empty.PadRight(20, '-'),
                            repositorioTreinamento.BuscarPorIndice(resultado).Descricao);
                        totalErros++;
                    }
                }
            }

            Console.WriteLine("Total acertos: {0}", totalAcertos);
            Console.WriteLine("Total erros: {0}", totalErros);
            Console.WriteLine("Sinais corretos: {0}{1}", acertos.ToString(), Environment.NewLine);
            Console.WriteLine("Sinais errados: {0}{1}", Environment.NewLine, erros.ToString());

            if (totalErros > 0)
            {
                Assert.Inconclusive(
                    "{0}Total acertos: {3}{0}Total erros:{4}{0}{1}{0}-----{0}{2}", 
                    Environment.NewLine,
                    acertos.ToString(), 
                    erros.ToString(),
                    totalAcertos,
                    totalErros);
            }
        }
    }
}
