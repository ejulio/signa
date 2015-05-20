using Dominio.Algoritmos.Caracteristicas;
using Dominio.Algoritmos.Dados;
using Dominio.Algoritmos.Estatico;
using Dominio.Sinais;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Dominio.Sinais.Frames;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Frames;

namespace Testes.Unidade.Algoritmos.Estatico
{
    [TestClass]
    public class SvmTeste
    {
        [TestMethod]
        public void reconhecendo_um_sinal_sem_treinar_o_algoritmo()
        {
            var amostra = new ColecaoDeFramesBuilder().Construir();
            Action acao = () => new Svm(new CaracteristicasSinalEstatico()).Classificar(amostra);

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

            var indiceReconhecido = svm.Classificar(amostra);

            indiceReconhecido.Should().Be(indiceDoSinalEsperado);
        }

        private Svm DadoUmAlgoritmoTreinado(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            var sinais = DadaUmaColecaoDeSinais(quantidadeDeSinais, quantidadeDeAmostrasPorSinal);
            var dadosDeTreinamento = new GeradorDeDadosDeSinaisEstaticos(sinais);

            var svm = new Svm(new CaracteristicasSinalEstatico());
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
