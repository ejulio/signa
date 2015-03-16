using Aplicacao.Dominio.Sinais;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Unidade.Dominio.Sinais
{
    [TestClass]
    public class SinalTeste
    {
        [TestMethod]
        public void adicionando_uma_amotra()
        {
            var sinal = DadoUmSinalComTresAmostras();
            var amostra = new ColecaoDeFramesBuilder().Construir();

            sinal.AdicionarAmostra(amostra);

            sinal.Amostras.Should().HaveCount(4);
            sinal.Amostras[3].Should().Contain(amostra);
        }

        private Sinal DadoUmSinalComTresAmostras()
        {
            return new SinalBuilder()
                .ComAmostra(new ColecaoDeFramesBuilder().Construir())
                .ComAmostra(new ColecaoDeFramesBuilder().Construir())
                .ComAmostra(new ColecaoDeFramesBuilder().Construir())
                .Construir();
        }
    }
}