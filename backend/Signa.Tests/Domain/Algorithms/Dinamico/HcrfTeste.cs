using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Algorithms.Dinamico;
using Signa.Domain.Signs.Dynamic;
using Signa.Tests.Common.Builders.Domain.Features;
using Signa.Tests.Common.Builders.Domain.Signs;
using Signa.Tests.Common.Builders.Domain.Signs.Dinamico;

namespace Signa.Tests.Domain.Algorithms.Dinamico
{
    [TestClass]
    public class HcrfTeste
    {
        [TestMethod]
        public void reconhecendo_sem_treinar_o_algoritmo()
        {
            var amostra = new AmostraDeSinalBuilder().Construir();
            Action acao = () => new Hcrf().Reconhecer(amostra);

            acao.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void reconhecendo_um_sinal()
        {
            const int quantidadeDeSinais = 3;
            const int quantidadeDeAmostrasPorSinal = 4;
            const int indiceDoSinalResultante = 2;

            var hcrf = DadoUmAlgoritmoTreinado(quantidadeDeSinais, quantidadeDeAmostrasPorSinal);

            var amostraParaReconhecer = new AmostraDeSinalBuilder().ParaOIndiceComQuantidade(indiceDoSinalResultante).Construir();

            int sinalReconhecido = hcrf.Reconhecer(amostraParaReconhecer);

            sinalReconhecido.Should().Be(indiceDoSinalResultante);
        }

        private Hcrf DadoUmAlgoritmoTreinado(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            var hcrf = new Hcrf();
            var colecaoDeSinais = new ColecaoDeSinaisDinamicosBuilder()
                .ComTamanho(quantidadeDeSinais)
                .ComQuantidadeDeAmostras(quantidadeDeAmostrasPorSinal)
                .ComGeradorDeAmostras(CriarAmostraPorIndice)
                .ComGeradorDeAmostras(i => new AmostraDeSinalBuilder().ParaOIndiceComQuantidade(i).Construir())
                .Construir();

            var dados = new DadosParaAlgoritmoDeReconhecimentoDeSinal(colecaoDeSinais);
            hcrf.TreinarCom(dados);

            return hcrf;
        }

        private AmostraDeSinal CriarAmostraPorIndice(int indice)
        {
            var frames = new FrameDeSinal[indice + 2];

            for (int i = 0; i < frames.Length; i++)
            {
                frames[i] = new FrameDeSinalBuilder()
                    .ComMaoEsquerda(new MaoBuilder().Construir())
                    .Construir();
            }

            return new AmostraDeSinalBuilder()
                .ComFrames(frames)
                .Construir();
        }
    }
}