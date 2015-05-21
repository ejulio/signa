using Dominio.Persistencia;
using Dominio.Sinais;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Frames;

namespace Testes.Integracao.Persistencia
{
    [TestClass]
    public class RepositorioSinaisTeste
    {
        private RepositorioSinais repositorioSinais;
        private const string CaminhoDoArquivoDeAmostras = "Integracao/JsonTestData/repositorio-de-sinais-teste.json";
        private const string TemplateDaDescricaoDeSinalEstatico = "sinal estático {0}";
        private const string TemplateDaDescricaoDeSinalDinamico = "sinal dinâmico {0}";
        private const string TemplateDoCaminhoDoArquivoDeExemplo = "sinal-{0}.json";

        [TestInitialize]
        public void Setup()
        {
            repositorioSinais = new RepositorioSinais(CaminhoDoArquivoDeAmostras);
        }

        [TestMethod]
        public void carregando_sinais_do_arquivo()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();

            repositorioSinais.Carregar();

            DeveTerOsSinaisDoArquivo(sinais);
        }

        [TestMethod]
        public void buscando_um_sinal_pela_descricao()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioSinais.Carregar();

            DevePoderBuscarUmSinalPelaDescricao(sinais);
        }

        [TestMethod]
        public void adicionando_um_sinal()
        {
            DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioSinais.Carregar();

            var idEsperadoDoSinal = repositorioSinais.Quantidade;

            var conteudoDoArquivoDeSinais = RecuperarConteudoDoArquivoDeSinais();
            
            var sinal = DadoUmNovoSinal("New sign");
            repositorioSinais.Adicionar(sinal);

            RecuperarConteudoDoArquivoDeSinais().Should().Be(conteudoDoArquivoDeSinais);

            repositorioSinais.SalvarAlteracoes();

            sinal.Id.Should().Be(idEsperadoDoSinal);
            RecuperarConteudoDoArquivoDeSinais().Should().NotBe(conteudoDoArquivoDeSinais);
            RecuperarConteudoDoArquivoDeSinais().Length.Should().BeGreaterThan(conteudoDoArquivoDeSinais.Length);
        }

        [TestMethod]
        public void adicionando_um_sinal_e_buscando()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioSinais.Carregar();

            var descricaoDoSinal = "New sign";
            var indiceDoSinal = sinais.Count;

            var sinal = DadoUmNovoSinal(descricaoDoSinal);
            repositorioSinais.Adicionar(sinal);

            repositorioSinais.Quantidade.Should().Be(sinais.Count + 1);
            repositorioSinais.BuscarPorDescricao(descricaoDoSinal).Descricao.Should().Be(descricaoDoSinal);
            repositorioSinais.BuscarPorIndice(indiceDoSinal).Descricao.Should().Be(descricaoDoSinal);
        }

        [TestMethod]
        public void enumerando_o_repositorio()
        {
            DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioSinais.Carregar();

            int indice = 0;

            foreach (var sinal in repositorioSinais)
            {
                sinal.Should().Be(repositorioSinais.BuscarPorIndice(indice));
                indice++;
            }
        }

        [TestMethod]
        public void carregando_um_arquivo_de_sinais_vazio()
        {
            DadoQueOArquivoDeSinaisEstejaVazio();

            Action acaoDeCarregar = () => repositorioSinais.Carregar();
            Action acaoBuscarPorIndice = () => repositorioSinais.BuscarPorIndice(0);
            Action acaoBuscarPorId = () => repositorioSinais.BuscarPorDescricao("");

            acaoDeCarregar.ShouldNotThrow();
            acaoBuscarPorIndice.ShouldNotThrow();
            acaoBuscarPorId.ShouldNotThrow();
        }

        [TestMethod]
        public void carregando_quando_o_arquivo_nao_existe()
        {
            DadoQueOArquivoDeSinaisNaoExiste();

            Action acao = () => repositorioSinais.Carregar();

            acao.ShouldNotThrow();
        }

        [TestMethod]
        public void salvando_alteracoes()
        {
            DadoQueOArquivoDeSinaisNaoExiste();

            var sinal = DadoUmNovoSinal("saving sign");
            repositorioSinais.Adicionar(sinal);

            Action acao = () => repositorioSinais.SalvarAlteracoes();

            acao.ShouldNotThrow();
            File.Exists(CaminhoDoArquivoDeAmostras).Should().BeTrue();
            RecuperarConteudoDoArquivoDeSinais().Should().NotBe("");
        }

        private static void DadoQueOArquivoDeSinaisNaoExiste()
        {
            if (File.Exists(CaminhoDoArquivoDeAmostras))
                File.Delete(CaminhoDoArquivoDeAmostras);
        }

        private void DadoQueOArquivoDeSinaisEstejaVazio()
        {
            using (StreamWriter writer = new StreamWriter(CaminhoDoArquivoDeAmostras))
            {
                writer.Write("");
            }
        }

        private ICollection<Sinal> DadoQueExistamAlgunsSinaisNoArquivoDeExemplo()
        {
            var sinaisEstaticos = new ColecaoDeSinaisBuilder()
                            .ComQuantidadeDeSinais(4)
                            .ComTemplateDeDescricao(TemplateDaDescricaoDeSinalEstatico)
                            .ComTemplateDoCaminhoDoArquivoDeExemplo(TemplateDoCaminhoDoArquivoDeExemplo)
                            .ComGeradorDeAmostrasEstaticas()
                            .Construir();

            var sinaisDinamicos = new ColecaoDeSinaisBuilder()
                            .ComQuantidadeDeSinais(4)
                            .ComTemplateDeDescricao(TemplateDaDescricaoDeSinalDinamico)
                            .ComTemplateDoCaminhoDoArquivoDeExemplo(TemplateDoCaminhoDoArquivoDeExemplo)
                            .ComGeradorDeAmostrasDinamicas()
                            .Construir();

            var sinais = sinaisDinamicos.Concat(sinaisEstaticos).ToList();

            var json = JsonConvert.SerializeObject(sinais);

            using (StreamWriter writer = new StreamWriter(CaminhoDoArquivoDeAmostras))
            {
                writer.Write(json);
            }

            return sinais;
        }

        private static Sinal DadoUmNovoSinal(string descricao)
        {
            var sinal = new SinalBuilder()
                            .ComDescricao(descricao)
                            .ComCaminhoParaArquivoDeExemplo("new-sign.json")
                            .ComAmostra(new ColecaoDeFramesBuilder().Construir())
                            .Construir();
            return sinal;
        }

        private void DevePoderBuscarUmSinalPelaDescricao(ICollection<Sinal> sinais)
        {
            for (int i = 0; i < sinais.Count(s => s.Tipo == TipoSinal.Estatico); i++)
            {
                var idSinal = String.Format(TemplateDaDescricaoDeSinalEstatico, i);
                repositorioSinais.BuscarPorDescricao(idSinal).Descricao.Should().Be(idSinal);
            }

            for (int i = 0; i < sinais.Count(s => s.Tipo == TipoSinal.Dinamico); i++)
            {
                var idSinal = String.Format(TemplateDaDescricaoDeSinalDinamico, i);
                repositorioSinais.BuscarPorDescricao(idSinal).Descricao.Should().Be(idSinal);
            }
        }

        private void DeveTerOsSinaisDoArquivo(ICollection<Sinal> sinais)
        {
            repositorioSinais.Quantidade.Should().Be(sinais.Count);
            for (int i = 0; i < sinais.Count; i++)
            {
                repositorioSinais
                    .BuscarPorIndice(i)
                    .Should().NotBeNull();
            }
        }
        private string RecuperarConteudoDoArquivoDeSinais()
        {
            using (StreamReader reader = new StreamReader(CaminhoDoArquivoDeAmostras))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
