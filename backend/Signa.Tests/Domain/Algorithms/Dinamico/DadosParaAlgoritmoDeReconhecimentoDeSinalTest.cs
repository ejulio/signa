using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Algorithms.Dinamico;
using Signa.Domain.Signs.Dynamic;
using Signa.Tests.Common.Builders.Domain.Signs.Dynamic;

namespace Signa.Tests.Domain.Algorithms.Dinamico
{
    [TestClass]
    public class DadosParaAlgoritmoDeReconhecimentoDeSinalTest
    {
        [TestMethod]
        public void criando_dados_para_um_sinal_com_cinco_frames()
        {
            var frames = new[]
            {
                new FrameDeSinalBuilder().WithDefaultLeftAndRightHand().Build(),
                new FrameDeSinalBuilder().WithDefaultLeftAndRightHand().Build(),
                new FrameDeSinalBuilder().WithDefaultLeftAndRightHand().Build(),
                new FrameDeSinalBuilder().WithDefaultLeftAndRightHand().Build(),
                new FrameDeSinalBuilder().WithDefaultLeftAndRightHand().Build()
            };
            
            var amostra = new AmostraDeSinalBuilder().WithFrames(frames).Build();
            var sinal = new SinalBuilder().WithSample(amostra).Build();
            
            var dados = new DadosParaAlgoritmoDeReconhecimentoDeSinal(sinal.Amostras);

            dados.Saidas.Should().HaveSameCount(dados.Entradas);
            dados.Saidas.Should().ContainInOrder(new [] { 0 });
            dados.QuantidadeDeClasses.Should().Be(1);
            DeveTerAsEntradasDasAmostras(dados.Entradas, sinal.Amostras);
        }

        private void DeveTerAsEntradasDasAmostras(double[][][] entradas, IList<AmostraDeSinal> amostras)
        {
            for (var i = 0; i < entradas.Length; i++)
            {
                var amostra = amostras[i].ToArray();
                for (var j = 0; j < entradas[i].Length; j++)
                {
                    entradas[i][j].Should().ContainInOrder(amostra[j]);
                }
            }
        }
    }
}