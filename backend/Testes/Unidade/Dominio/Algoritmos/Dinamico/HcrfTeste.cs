using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Caracteristicas;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Algoritmos.Dinamico
{
    [TestClass]
    public class HcrfTeste
    {
        [TestMethod]
        public void reconhecendo_sem_treinar_o_algoritmo()
        {
            var amostra = new AmostraBuilder().Construir();
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

            var amostraParaReconhecer = new AmostraBuilder().ParaOIndiceComQuantidade(indiceDoSinalResultante).Construir();

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
                .ComGeradorDeAmostras(i => new AmostraBuilder().ParaOIndiceComQuantidade(i).Construir())
                .Construir();

            var dados = new DadosParaAlgoritmoDeReconhecimentoDeSinal(colecaoDeSinais);
            hcrf.TreinarCom(dados);

            return hcrf;
        }

        private Amostra CriarAmostraPorIndice(int indice)
        {
            var frames = new Frame[indice + 2];

            for (int i = 0; i < frames.Length; i++)
            {
                frames[i] = new FrameBuilder()
                    .ComMaoEsquerda(new MaoBuilder().Construir())
                    .Construir();
            }

            return new AmostraBuilder()
                .ComFrames(frames)
                .Construir();
        }
    }
}