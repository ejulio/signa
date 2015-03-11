using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Signa.Dados.Repositorio;
using Signa.Dominio.Sinais;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Integracao.Dados.Repositorio
{
    [TestClass]
    public class RepositorioDeSinaisTeste
    {
        private RepositorioDeSinais repositorioDeSinais;
        private const string CaminhoDoArquivoDeAmostras = "Integracao/JsonTestData/repositorio-de-sinais-teste.json";
        private const string TemplateDaDescricaoDeSinalEstatico = "sinal estático {0}";
        private const string TemplateDaDescricaoDeSinalDinamico = "sinal dinâmico {0}";
        private const string TemplateDoCaminhoDoArquivoDeExemplo = "sinal-{0}.json";

        [TestInitialize]
        public void Setup()
        {
            repositorioDeSinais = new RepositorioDeSinais(CaminhoDoArquivoDeAmostras);
        }

        [TestMethod]
        public void carregando_sinais_do_arquivo()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();

            repositorioDeSinais.Carregar();

            DeveTerOsSinaisDoArquivo(sinais);
        }

        [TestMethod]
        public void buscando_um_sinal_pela_descricao()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioDeSinais.Carregar();

            DevePoderBuscarUmSinalPelaDescricao(sinais);
        }

        [TestMethod]
        public void adicionando_um_sinal()
        {
            DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioDeSinais.Carregar();

            var conteudoDoArquivoDeSinais = RecuperarConteudoDoArquivoDeSinais();
            
            var sinal = DadoUmNovoSinal("New sign");
            repositorioDeSinais.Adicionar(sinal);

            RecuperarConteudoDoArquivoDeSinais().Should().Be(conteudoDoArquivoDeSinais);

            repositorioDeSinais.SalvarAlteracoes();

            RecuperarConteudoDoArquivoDeSinais().Should().NotBe(conteudoDoArquivoDeSinais);
            RecuperarConteudoDoArquivoDeSinais().Length.Should().BeGreaterThan(conteudoDoArquivoDeSinais.Length);
        }

        [TestMethod]
        public void adicionando_um_sinal_e_buscando()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioDeSinais.Carregar();

            var descricaoDoSinal = "New sign";
            var indiceDoSinal = sinais.Count;

            var sinal = DadoUmNovoSinal(descricaoDoSinal);
            repositorioDeSinais.Adicionar(sinal);

            repositorioDeSinais.Quantidade.Should().Be(sinais.Count + 1);
            repositorioDeSinais.BuscarPorDescricao(descricaoDoSinal).Descricao.Should().Be(descricaoDoSinal);
            repositorioDeSinais.BuscarPorIndice(indiceDoSinal).Descricao.Should().Be(descricaoDoSinal);
        }

        [TestMethod]
        public void enumerando_o_repositorio()
        {
            DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioDeSinais.Carregar();

            int indice = 0;

            foreach (var sinal in repositorioDeSinais)
            {
                sinal.Should().Be(repositorioDeSinais.BuscarPorIndice(indice));
                indice++;
            }
        }

        [TestMethod]
        public void carregando_um_arquivo_de_sinais_vazio()
        {
            DadoQueOArquivoDeSinaisEstejaVazio();

            Action acaoDeCarregar = () => repositorioDeSinais.Carregar();
            Action acaoBuscarPorIndice = () => repositorioDeSinais.BuscarPorIndice(0);
            Action acaoBuscarPorId = () => repositorioDeSinais.BuscarPorDescricao("");

            acaoDeCarregar.ShouldNotThrow();
            acaoBuscarPorIndice.ShouldNotThrow();
            acaoBuscarPorId.ShouldNotThrow();
        }

        [TestMethod]
        public void carregando_quando_o_arquivo_nao_existe()
        {
            DadoQueOArquivoDeSinaisNaoExiste();

            Action acao = () => repositorioDeSinais.Carregar();

            acao.ShouldNotThrow();
        }

        [TestMethod]
        public void salvando_alteracoes()
        {
            DadoQueOArquivoDeSinaisNaoExiste();

            var sinal = DadoUmNovoSinal("saving sign");
            repositorioDeSinais.Adicionar(sinal);

            Action acao = () => repositorioDeSinais.SalvarAlteracoes();

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
                repositorioDeSinais.BuscarPorDescricao(idSinal).Descricao.Should().Be(idSinal);
            }

            for (int i = 0; i < sinais.Count(s => s.Tipo == TipoSinal.Dinamico); i++)
            {
                var idSinal = String.Format(TemplateDaDescricaoDeSinalDinamico, i);
                repositorioDeSinais.BuscarPorDescricao(idSinal).Descricao.Should().Be(idSinal);
            }
        }

        private void DeveTerOsSinaisDoArquivo(ICollection<Sinal> sinais)
        {
            repositorioDeSinais.Quantidade.Should().Be(sinais.Count);
            for (int i = 0; i < sinais.Count; i++)
            {
                repositorioDeSinais
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
