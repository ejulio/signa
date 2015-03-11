using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Dominio.Algoritmos.Estatico;
using Signa.Dominio.Sinais;
using Signa.Dominio.Sinais.Caracteristicas;
using Signa.Util;
using System.Linq;
using Testes.Comum.Builders.Dominio.Caracteristicas;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Algoritmos.Estatico
{
    [TestClass]
    public class GeradorDeAmostraDeSinalEstaticoTeste
    {
        [TestMethod]
        public void extraindo_caracteristicas_de_um_frame()
        {
            var maoEsquerda = DadaUmaMaoEsquerda();
            var maoDireita = DadaUmaMaoDireita();
            var frame = DadoUmFrameComMaos(maoEsquerda, maoDireita);

            var geradorDeAmostraDeSinalEstatico = new GeradorDeCaracteristicasDeSinalEstatico();
            var frameArray = geradorDeAmostraDeSinalEstatico.ExtrairCaracteristicasDaAmostra(new[] { frame });

            DeveRetornarUmArrayComDadosDasMaosEsquerdaEDireita(maoEsquerda, maoDireita, frameArray);
        }

        private Frame DadoUmFrameComMaos(Mao maoEsquerda, Mao maoDireita)
        {
            var frame = new FrameBuilder()
                .ComMaoEsquerda(maoEsquerda)
                .ComMaoDireita(maoDireita)
                .Construir();

            return frame;
        }

        private Mao DadaUmaMaoDireita()
        {
            var maoDireita = new MaoBuilder()
                .ComDedos(DedoBuilder.DedosPadroes())
                .ComDirecaoDaMao(new[] { 0.4, 0.5, 0.6 })
                .ComVetorNormalDaPalma(new[] { 0.0, 1.0, 1.0 })
                .Construir();
            return maoDireita;
        }

        private Mao DadaUmaMaoEsquerda()
        {
            var maoEsquerda = new MaoBuilder()
                .ComDedos(DedoBuilder.DedosPadroes())
                .ComDirecaoDaMao(new[] { 0.1, 0.2, 0.3 })
                .ComVetorNormalDaPalma(new[] { 1.0, 0.0, 1.0 })
                .Construir();
            return maoEsquerda;
        }

        private void DeveRetornarUmArrayComDadosDasMaosEsquerdaEDireita(Mao maoEsquerda, Mao maoDireita, double[] frameArray)
        {
            var dadosDoFrameEsperados = MontarArrayEsperadoParaAMao(maoEsquerda)
                .Concat(MontarArrayEsperadoParaAMao(maoDireita))
                .ToArray();

            frameArray.Should().HaveCount(dadosDoFrameEsperados.Count());
            frameArray.Should().ContainInOrder(dadosDoFrameEsperados);
        }

        private double[] MontarArrayEsperadoParaAMao(Mao mao)
        {
            var dadosDosDedos = mao.Dedos.Select(d =>
            {
                var tipo = new double[] { (int)d.Tipo };
                return tipo.Concat(d.Direcao).ToArray();
            }).Concatenar();

            return mao.VetorNormalDaPalma
                    .Concat(mao.DirecaoDaMao)
                    .Concat(dadosDosDedos)
                    .ToArray();
        }
    }
}