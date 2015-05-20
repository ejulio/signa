using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dominio.Persistencia;
using Dominio.Sinais;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Testes.Comum.Builders.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais.Frames;

namespace Testes.Integracao.Persistencia
{
    [TestClass]
    public class RepositorioSinaisEstaticosTeste
    {
        private RepositorioSinaisEstaticos repositorioSinaisEstaticos;
        private RepositorioSinais repositorioSinais;
        private const string CaminhoDoArquivoDeDeAmostras = Caminhos.CaminhoDoArquivoDeAmostras;
        private const string TemplateDaDescricaoDeSinalEstatico = "sinal estático {0}";
        private const string TemplateDaDescricaoDeSinalDinamico = "sinal dinâmico {0}";
        private const string TemplateDoCaminhoDeExemplo = "static-sample-{0}.json";

        [TestInitialize]
        public void TestInitialize()
        {
            repositorioSinais = new RepositorioSinais(CaminhoDoArquivoDeDeAmostras);
            repositorioSinaisEstaticos = new RepositorioSinaisEstaticos(repositorioSinais);
        }

        [TestMethod]
        public void carregando_sinais_do_arquivo()
        {
            var sinais = DadoQueExistamDadosNoArquivoDeSinais();

            repositorioSinaisEstaticos.Carregar();

            DeveTerCarregadoOsSinaisDoArquivo(sinais);
        }

        [TestMethod]
        public void buscando_um_sinal_pela_descricao()
        {
            var sinais = DadoQueExistamDadosNoArquivoDeSinais();
            
            repositorioSinaisEstaticos.Carregar();

            DevePoderBuscarOsSinaisPelaDescricao(sinais);
        }

        [TestMethod]
        public void adicionando_um_sinal()
        {
            DadoQueExistamDadosNoArquivoDeSinais();
            
            repositorioSinaisEstaticos.Carregar();

            var idEsperadoDoSinal = repositorioSinais.Quantidade;

            var conteudoDoArquivoDeSinais = BuscarConteudoDoArquivoDeSinais();
            
            var sinal = DadoUmNovoSinal("New sign");
            repositorioSinaisEstaticos.Adicionar(sinal);

            BuscarConteudoDoArquivoDeSinais().Should().Be(conteudoDoArquivoDeSinais);

            repositorioSinaisEstaticos.SalvarAlteracoes();

            sinal.Id.Should().Be(idEsperadoDoSinal);
            BuscarConteudoDoArquivoDeSinais().Should().NotBe(conteudoDoArquivoDeSinais);
            BuscarConteudoDoArquivoDeSinais().Length.Should().BeGreaterThan(conteudoDoArquivoDeSinais.Length);
        }

        [TestMethod]
        public void adicionando_um_sinal_e_buscando()
        {
            var sinais = DadoQueExistamDadosNoArquivoDeSinais();
            
            repositorioSinaisEstaticos.Carregar();

            var descricaoDoSinal = "New sign";
            var indiceDoSinal = sinais.Count;

            var sinal = DadoUmNovoSinal(descricaoDoSinal);
            repositorioSinaisEstaticos.Adicionar(sinal);

            repositorioSinaisEstaticos.Quantidade.Should().Be(sinais.Count + 1);
            repositorioSinaisEstaticos.BuscarPorDescricao(descricaoDoSinal).Descricao.Should().Be(descricaoDoSinal);
            repositorioSinaisEstaticos.BuscarPorIndice(indiceDoSinal).Descricao.Should().Be(descricaoDoSinal);
        }

        [TestMethod]
        public void enumerando_os_itens_do_repositorio()
        {
            DadoQueExistamDadosNoArquivoDeSinais();
            
            repositorioSinaisEstaticos.Carregar();

            int indice = 0;

            foreach (var sinal in repositorioSinaisEstaticos)
            {
                sinal.Should().Be(repositorioSinaisEstaticos.BuscarPorIndice(indice));
                indice++;
            }
        }

        [TestMethod]
        public void carregando_um_arquivo_vazio()
        {
            DadoQueOArquivoDeSinaisEstejaVazio();

            Action acaoDeCarregar = () => repositorioSinaisEstaticos.Carregar();
            Action acaoDeBuscarPorIndice = () => repositorioSinaisEstaticos.BuscarPorIndice(0);
            Action acaoDeBuscarPorDescricao = () => repositorioSinaisEstaticos.BuscarPorDescricao("");

            acaoDeCarregar.ShouldNotThrow();
            acaoDeBuscarPorIndice.ShouldNotThrow();
            acaoDeBuscarPorDescricao.ShouldNotThrow();
        }

        [TestMethod]
        public void carregando_dados_quando_arquivo_nao_existe()
        {
            DadoQueOArquivoDeSinaisNaoExista();

            Action acao = () => repositorioSinaisEstaticos.Carregar();

            acao.ShouldNotThrow();
        }

        [TestMethod]
        public void salvando_alteracoes_quando_o_arquivo_nao_existe()
        {
            DadoQueOArquivoDeSinaisNaoExista();

            var sinal = DadoUmNovoSinal("saving sign");
            repositorioSinaisEstaticos.Adicionar(sinal);

            Action acao = () => repositorioSinaisEstaticos.SalvarAlteracoes();

            acao.ShouldNotThrow();
            File.Exists(CaminhoDoArquivoDeDeAmostras).Should().BeTrue();
            BuscarConteudoDoArquivoDeSinais().Should().NotBe("");
        }

        private static void DadoQueOArquivoDeSinaisNaoExista()
        {
            if (File.Exists(CaminhoDoArquivoDeDeAmostras))
                File.Delete(CaminhoDoArquivoDeDeAmostras);
        }

        private void DadoQueOArquivoDeSinaisEstejaVazio()
        {
            using (StreamWriter writer = new StreamWriter(CaminhoDoArquivoDeDeAmostras))
            {
                writer.Write("");
            }
        }

        private ICollection<Sinal> DadoQueExistamDadosNoArquivoDeSinais()
        {
            var sinaisEstaticos = new ColecaoDeSinaisBuilder()
                    .ComQuantidadeDeSinais(4)
                    .ComTemplateDeDescricao(TemplateDaDescricaoDeSinalEstatico)
                    .ComTemplateDoCaminhoDoArquivoDeExemplo(TemplateDoCaminhoDeExemplo)
                    .ComGeradorDeAmostrasEstaticas()
                    .Construir();

            var sinaisDinamicos = new ColecaoDeSinaisBuilder()
                    .ComQuantidadeDeSinais(4)
                    .ComTemplateDeDescricao(TemplateDaDescricaoDeSinalDinamico)
                    .ComTemplateDoCaminhoDoArquivoDeExemplo(TemplateDoCaminhoDeExemplo)
                    .ComGeradorDeAmostrasDinamicas()
                    .Construir();

            var sinais = sinaisEstaticos.Concat(sinaisDinamicos).ToList();

            var json = JsonConvert.SerializeObject(sinais);

            using (StreamWriter writer = new StreamWriter(CaminhoDoArquivoDeDeAmostras))
            {
                writer.Write(json);
            }

            return sinaisEstaticos;
        }

        private Sinal DadoUmNovoSinal(string descricao)
        {
            var sign = new SinalBuilder()
                            .ComDescricao(descricao)
                            .ComCaminhoParaArquivoDeExemplo("new-sign.json")
                            .ComAmostra(new ColecaoDeFramesBuilder().Construir())
                            .Construir();
            return sign;
        }

        private void DevePoderBuscarOsSinaisPelaDescricao(ICollection<Sinal> signs)
        {
            string signId;
            for (int i = 0; i < signs.Count; i++)
            {
                signId = String.Format(TemplateDaDescricaoDeSinalEstatico, i);
                repositorioSinaisEstaticos.BuscarPorDescricao(signId).Descricao.Should().Be(signId);
            }
        }

        private void DeveTerCarregadoOsSinaisDoArquivo(ICollection<Sinal> signs)
        {
            repositorioSinaisEstaticos.Quantidade.Should().Be(signs.Count);
            for (int i = 0; i < signs.Count; i++)
            {
                repositorioSinaisEstaticos
                    .BuscarPorIndice(i)
                    .Should()
                    .Match<Sinal>(sign =>
                        sign.Descricao == String.Format(TemplateDaDescricaoDeSinalEstatico, i) &&
                        sign.CaminhoParaArquivoDeExemplo == String.Format(TemplateDoCaminhoDeExemplo, i) &&
                        sign.Amostras.Count == 4);
            }
        }
        private string BuscarConteudoDoArquivoDeSinais()
        {
            using (StreamReader reader = new StreamReader(CaminhoDoArquivoDeDeAmostras))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
