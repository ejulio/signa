using Dominio;
using Dominio.Algoritmos.Estatico;
using Dominio.Algoritmos.Factories;
using Dominio.Dados.Repositorio;
using Dominio.Sinais;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Integracao.Dominio.Algoritmos
{
    [TestClass]
    public class AlgoritmosTeste
    {
        private const string CaminhoDoArquivoDeDadosDeReconhecimento = "Integracao/JsonTestData/repositorio-sinais-teste-reconhecimento.json";
        private const string CaminhoDoArquivoDeDadosDeTreinamento = "Integracao/JsonTestData/repositorio-sinais-treinamento-reconhecimento.json";

        private IRepositorio<Sinal> repositorio;
        private AlgoritmoDeReconhecimentoDeSinalFactory algoritmoFactory;

        [TestInitialize]
        public void setup()
        {
            var geradorDeCaracteristicasFactory = new GeradorDeCaracteristicasFactory();
            var repositorioFactory = new RepositorioFactory(CaminhoDoArquivoDeDadosDeTreinamento);
            algoritmoFactory = new AlgoritmoDeReconhecimentoDeSinalFactory(geradorDeCaracteristicasFactory);
            var inicializadorDeAlgoritmo = new InicializadorDeAlgoritmoFacade(algoritmoFactory, repositorioFactory);

            inicializadorDeAlgoritmo.TreinarAlgoritmoDeReconhecimentoDeSinaisEstaticos();
            inicializadorDeAlgoritmo.TreinarAlgoritmoDeReconhecimentoDeSinaisDinamicos();

            repositorio = new RepositorioDeSinais(CaminhoDoArquivoDeDadosDeReconhecimento);
        }

        [TestMethod]
        public void reconhecendo_sinais_estaticos()
        {
            var repositorioSinaisEstaticos = new RepositorioDeSinaisEstaticos(repositorio);
            var algoritmo = algoritmoFactory.CriarReconhecedorDeSinaisEstaticos();

            repositorioSinaisEstaticos.Carregar();

            int totalAcertos = 0;
            int totalErros = 0;
            for (var i = 0; i < repositorioSinaisEstaticos.Quantidade; i++)
            {
                var sinal = repositorioSinaisEstaticos.BuscarPorIndice(i);
                for (var j = 0; j < sinal.Amostras.Count; j++)
                {
                    var resultado = algoritmo.Reconhecer(sinal.Amostras[j]);

                    Console.WriteLine(
                        "Sinal: {1}{0}Índice da amostra: {2}{0}Índice Esperado: {3}{0}Índice Reconhecido: {4}{0}{5}", 
                        Environment.NewLine, 
                        sinal.Descricao, 
                        j,
                        i, 
                        resultado, 
                        String.Empty.PadRight(20, '-'));

                    if (resultado == i)
                        totalAcertos++;
                    else
                        totalErros++;
                }
            }

            Console.WriteLine("Total acertos: {0}", totalAcertos);
            Console.WriteLine("Total erros: {0}", totalErros);
        }

        [TestMethod]
        public void reconhecendo_sinais_dinamicos()
        {
            var repositorioSinaisDinamicos = new RepositorioDeSinaisDinamicos(repositorio);
            var algoritmo = algoritmoFactory.CriarReconhecedorDeSinaisDinamicos();

            repositorioSinaisDinamicos.Carregar();

            int totalAcertos = 0;
            int totalErros = 0;
            for (var i = 0; i < repositorioSinaisDinamicos.Quantidade; i++)
            {
                var sinal = repositorioSinaisDinamicos.BuscarPorIndice(i);
                for (var j = 0; j < sinal.Amostras.Count; j++)
                {
                    var resultado = algoritmo.Reconhecer(sinal.Amostras[j]);

                    Console.WriteLine(
                        "Sinal: {1}{0}Índice da amostra: {2}{0}Índice Esperado: {3}{0}Índice Reconhecido: {4}{0}{5}",
                        Environment.NewLine,
                        sinal.Descricao,
                        j,
                        i,
                        resultado,
                        String.Empty.PadRight(20, '-'));

                    if (resultado == i)
                        totalAcertos++;
                    else
                        totalErros++;
                }
            }

            Console.WriteLine("Total acertos: {0}", totalAcertos);
            Console.WriteLine("Total erros: {0}", totalErros);
        }

        [TestMethod]
        public void reconhecendo_sinais_frames_de_sinais_dinamicos()
        {
            var repositorioSinaisDinamicos = new RepositorioDeSinaisDinamicos(repositorio);
            var algoritmo = algoritmoFactory.CriarReconhecedorDeFramesDeSinaisDinamicos();

            repositorioSinaisDinamicos.Carregar();

            int totalAcertos = 0;
            int totalErros = 0;
            for (var i = 0; i < repositorioSinaisDinamicos.Quantidade; i++)
            {
                var sinal = repositorioSinaisDinamicos.BuscarPorIndice(i);
                for (var j = 0; j < sinal.Amostras.Count; j++)
                {
                    var resultado = algoritmo.Reconhecer(sinal.Amostras[j]);

                    Console.WriteLine(
                        "Sinal: {1}{0}Índice da amostra: {2}{0}Índice Esperado: {3}{0}Índice Reconhecido: {4}{0}{5}",
                        resultado == i ? Environment.NewLine : Environment.NewLine.PadRight(5),
                        sinal.Descricao,
                        j,
                        i,
                        resultado,
                        String.Empty.PadRight(20, '-'));

                    if (resultado == i)
                        totalAcertos++;
                    else
                        totalErros++;
                }
            }

            Console.WriteLine("Total acertos: {0}", totalAcertos);
            Console.WriteLine("Total erros: {0}", totalErros);
        }
    }
}
