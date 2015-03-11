using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Dinamico;
using Signa.Dominio.Sinais;
using System;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Algoritmos.Dinamico
{
    [TestClass]
    public class HcrfTeste
    {
        [TestMethod]
        public void reconhecendo_sem_treinar_o_algoritmo()
        {
            var frames = new Frame[0];
            Action acao = () => new Hcrf().Reconhecer(frames);

            acao.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void reconhecendo_um_sinal()
        {
            const int quantidadeDeSinais = 3;
            const int quantidadeDeAmostrasPorSinal = 4;
            const int indiceDoSinalResultante = 2;

            var hcrf = DadoUmAlgoritmoTreinado(quantidadeDeSinais, quantidadeDeAmostrasPorSinal);

            var framesParaReconhecer = new []
            {
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir(),
                new FrameBuilder().ParaOIndice(indiceDoSinalResultante).Construir()
            };

            int sinalReconhecido = hcrf.Reconhecer(framesParaReconhecer);

            sinalReconhecido.Should().Be(indiceDoSinalResultante);
        }

        private Hcrf DadoUmAlgoritmoTreinado(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            var hcrf = new Hcrf();
            var colecaoDeSinais = new ColecaoDeSinaisBuilder()
                .ComQuantidadeDeSinais(quantidadeDeSinais)
                .ComQuantidadeDeAmostrasPorSinal(quantidadeDeAmostrasPorSinal)
                .ComGeradorDeAmostras(i => new ColecaoDeFramesBuilder().ParaOIndiceComQuantidade(i, 5).Construir())
                .Construir();

            var dados = new DadosParaAlgoritmoDeReconhecimentoDeSinaisDinamicos(colecaoDeSinais);
            hcrf.Treinar(dados);

            return hcrf;
        }
    }
}