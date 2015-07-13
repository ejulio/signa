using Dominio.Algoritmos.Caracteristicas;
using Dominio.Algoritmos.Dinamico;
using Dominio.Algoritmos.Treinamento;
using Dominio.Sinais.Frames;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Frames;

namespace Testes.Unidade.Algoritmos.Dinamico
{
    [TestClass]
    public class HmmTeste
    {
        [TestMethod]
        public void reconhecendo_sem_treinar_o_algoritmo()
        {
            var frames = new Frame[0];
            Action acao = () => new Hmm(new CaracteristicasSinalDinamico()).Classificar(frames);

            acao.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void reconhecendo_um_sinal()
        {
            const int quantidadeDeSinais = 10;
            const int quantidadeDeAmostrasPorSinal = 2;
            const int indiceDoSinalResultante = 4;

            var hmm = DadoUmAlgoritmoTreinado(quantidadeDeSinais, quantidadeDeAmostrasPorSinal);

            var framesParaReconhecer = new []
            {
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir()
            };

            int sinalReconhecido = hmm.Classificar(framesParaReconhecer);

            sinalReconhecido.Should().Be(indiceDoSinalResultante);
        }

        private Hmm DadoUmAlgoritmoTreinado(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            var hmm = new Hmm(new CaracteristicasSinalDinamico());
            var colecaoDeSinais = new ColecaoDeSinaisBuilder()
                .ComQuantidadeDeSinais(quantidadeDeSinais)
                .ComQuantidadeDeAmostrasPorSinal(quantidadeDeAmostrasPorSinal)
                .ComGeradorDeAmostras(i => new ColecaoDeFramesBuilder().ParaOIndiceComQuantidade(i, 5).Construir())
                .Construir();

            var dados = new DadosSinaisDinamicos(colecaoDeSinais);
            dados.Processar();
            hmm.Aprender(dados);

            return hmm;
        }
    }
}