using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Caracteristicas;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Caracteristicas;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Algoritmos.Estatico
{
    [TestClass]
    public class SvmTeste
    {
        [TestMethod]
        public void reconhecendo_um_sinal_sem_treinar_o_algoritmo()
        {
            var amostra = new AmostraBuilder().ConstruirAmostraEstatica();
            Action acao = () => new Svm().Reconhecer(amostra);

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
            var dadosDeTreinamento = new DadosParaAlgoritmoDeReconhecimentoDeSinal(sinais);

            var svm = new Svm();
            svm.Treinar(dadosDeTreinamento);

            return svm;
        }

        private ICollection<Sinal> DadaUmaColecaoDeSinais(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            var signs = new ColecaoDeSinaisEstaticosBuilder()
                            .ComQuantidadeDeSinais(quantidadeDeSinais)            
                            .ComQuantidadeDeAmostrasPorSinal(quantidadeDeAmostrasPorSinal)
                            .ComGeradorDeAmostras(CriarAmostraPeloIndice)
                            .Construir();
            return signs;
        }

        private Amostra CriarAmostraPeloIndice(int index)
        {
            return new AmostraBuilder().ParaOIndiceComQuantidade(index, 1).Construir();
        }

        private Mao DadaUmaMaoComDedos(int indice)
        {
            var fingers = new[] 
            {
                new DedoBuilder().DoTipo(TipoDeDedo.Dedao).ComDirecao(new double[] { indice, indice, indice }).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Indicador).ComDirecao(new double[] { indice, indice, indice }).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Meio).ComDirecao(new double[] { indice, indice, indice }).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Anelar).ComDirecao(new double[] { indice, indice, indice }).Construir(),
                new DedoBuilder().DoTipo(TipoDeDedo.Mindinho).ComDirecao(new double[] { indice, indice, indice }).Construir()
            };

            return new MaoBuilder()
                    .ComDedos(fingers)
                    .ComVetorNormalDaPalma(new double[] { indice, indice, indice })
                    .ComDirecaoDaMao(new double[] { indice, indice, indice })
                    .Construir();
        }
    }
}
