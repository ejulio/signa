using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using System;
using System.Collections.Generic;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Algoritmos.Estatico
{
    [TestClass]
    public class SvmTeste
    {
        [TestMethod]
        public void reconhecendo_um_sinal_sem_treinar_o_algoritmo()
        {
            var amostra = new ColecaoDeFramesBuilder().Construir();
            Action acao = () => new Svm(new GeradorDeCaracteristicasDeSinalEstatico()).Reconhecer(amostra);

            acao.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void reconhecendo_um_sinal()
        {
            const int quantidadeDeSinais = 3;
            const int quantidadeDeAmostrasPorSinal = 2;
            const int indiceDoSinalEsperado = 2;

            var svm = DadoUmAlgoritmoTreinado(quantidadeDeSinais, quantidadeDeAmostrasPorSinal);

            var amostra = CriarAmostraPeloIndice(indiceDoSinalEsperado);

            var indiceReconhecido = svm.Reconhecer(amostra);

            indiceReconhecido.Should().Be(indiceDoSinalEsperado);
        }

        private Svm DadoUmAlgoritmoTreinado(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            var sinais = DadaUmaColecaoDeSinais(quantidadeDeSinais, quantidadeDeAmostrasPorSinal);
            var dadosDeTreinamento = new GeradorDeDadosDeSinaisEstaticos(sinais);

            var svm = new Svm(new GeradorDeCaracteristicasDeSinalEstatico());
            svm.Treinar(dadosDeTreinamento);

            return svm;
        }

        private ICollection<Sinal> DadaUmaColecaoDeSinais(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            var signs = new ColecaoDeSinaisBuilder()
                            .ComQuantidadeDeSinais(quantidadeDeSinais)            
                            .ComQuantidadeDeAmostrasPorSinal(quantidadeDeAmostrasPorSinal)
                            .ComGeradorDeAmostras(CriarAmostraPeloIndice)
                            .Construir();
            return signs;
        }

        private IList<Frame> CriarAmostraPeloIndice(int index)
        {
            return new []{ new FrameBuilder().ParaOIndice(index).Construir() };
        }
    }
}
