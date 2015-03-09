using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Dinamico;
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
            var amostra = new AmostraBuilder().ConstruirAmostraDinamica();
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

            var amostraParaReconhecer = new AmostraBuilder().ParaOIndiceComQuantidade(indiceDoSinalResultante).ConstruirAmostraDinamica();

            int sinalReconhecido = hcrf.Reconhecer(amostraParaReconhecer);

            sinalReconhecido.Should().Be(indiceDoSinalResultante);
        }

        private Hcrf DadoUmAlgoritmoTreinado(int quantidadeDeSinais, int quantidadeDeAmostrasPorSinal)
        {
            var hcrf = new Hcrf();
            var colecaoDeSinais = new ColecaoDeSinaisDinamicosBuilder()
                .ComTamanho(quantidadeDeSinais)
                .ComQuantidadeDeAmostras(quantidadeDeAmostrasPorSinal)
                .ComGeradorDeAmostras(i => new AmostraBuilder().ParaOIndiceComQuantidade(i, i + 2).Construir())
                .Construir();

            var dados = new DadosParaAlgoritmoDeReconhecimentoDeSinal(colecaoDeSinais);
            hcrf.TreinarCom(dados);

            return hcrf;
        }
    }
}