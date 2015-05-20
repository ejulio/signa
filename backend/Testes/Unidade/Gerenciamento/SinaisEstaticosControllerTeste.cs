using Dominio.Algoritmos.Estatico;
using Dominio.Gerenciamento;
using Dominio.Persistencia;
using Dominio.Sinais;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Frames;

namespace Testes.Unidade.Gerenciamento
{
    [TestClass]
    public class SinaisEstaticosControllerTeste
    {
        private Mock<IRepositorio<Sinal>> repositorio;
        private Mock<IAlgoritmoClassificacaoSinaisEstaticos> algoritmo;
        private GerenciadorSinaisEstaticos gerenciadorSinaisEstaticos;

        [TestInitialize]
        public void Setup()
        {
            repositorio = new Mock<IRepositorio<Sinal>>();
            algoritmo = new Mock<IAlgoritmoClassificacaoSinaisEstaticos>();
            gerenciadorSinaisEstaticos = new GerenciadorSinaisEstaticos(repositorio.Object, algoritmo.Object);
        }

        [TestMethod]
        public void reconhecendo_um_sinal()
        {
            const int idDoSinal = 23;
            var amostra = new ColecaoDeFramesBuilder().Construir();
            algoritmo.Setup(a => a.Classificar(amostra)).Returns(idDoSinal);

            var resultado = gerenciadorSinaisEstaticos.Reconhecer(idDoSinal, amostra);

            resultado.Should().BeTrue();
        }

        private Sinal DadoQueORepositorioRetorneUmSinalParaADescricao(string descricaoDoSinal)
        {
            var sinal = DadoUmSinalComDuasAmostras(descricaoDoSinal);

            repositorio
                .Setup(r => r.BuscarPorDescricao(descricaoDoSinal))
                .Returns(sinal);

            return sinal;
        }

        private void DadoQueORepositorioRetorneNullParaADescricao(string descricaoDoSinal)
        {
            repositorio.Setup(r => r.BuscarPorDescricao(descricaoDoSinal)).Returns((Sinal)null);
        }

        private Sinal DadoUmSinalComDuasAmostras(string descricaoDoSinal)
        {
            var sinal = new SinalBuilder()
                            .ComDescricao(descricaoDoSinal)
                            .ComAmostra(new ColecaoDeFramesBuilder().Construir())
                            .ComAmostra(new ColecaoDeFramesBuilder().Construir())
                            .Construir();
            return sinal;
        }

        private void DeveTerAdicionadoOSinalNoRepositorioESalvoAsAlteracoes(Sinal sinalEstatico)
        {
            repositorio.Verify(r => r.Adicionar(sinalEstatico));
            repositorio.Verify(r => r.SalvarAlteracoes());
        }

        private void DeveTerJuntadoAsAmostrasESalvoAsAlteracoes(Sinal sinalAntigo, Sinal novoSinal)
        {
            repositorio.Verify(r => r.Adicionar(It.IsAny<Sinal>()), Times.Never);
            repositorio.Verify(r => r.SalvarAlteracoes());
            sinalAntigo.Amostras.Count.Should().Be(4);
            sinalAntigo.Amostras.Should().Contain(novoSinal.Amostras);
        }
    }
}
