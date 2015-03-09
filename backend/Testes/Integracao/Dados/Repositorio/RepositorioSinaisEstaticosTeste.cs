using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Signa.Dados.Repositorio;
using Signa.Dominio.Sinais;
using System;
using System.Collections.Generic;
using System.IO;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Integracao.Dados.Repositorio
{
    [TestClass]
    public class RepositorioSinaisEstaticosTeste
    {
        private RepositorioSinaisEstaticos repositorioDeSinaisEstaticos;
        private const string CaminhoDoArquivoDeDeAmostras = Caminhos.CaminhoDoArquivoDeAmostras;
        private const string TemplateDaDescricao = "Static sign sample {0}";
        private const string TemplateDoCaminhoDeExemplo = "static-sample-{0}.json";

        [TestInitialize]
        public void TestInitialize()
        {
            repositorioDeSinaisEstaticos = new RepositorioSinaisEstaticos(CaminhoDoArquivoDeDeAmostras);
        }

        [TestMethod]
        public void carregando_sinais_do_arquivo()
        {
            var sinais = DadoQueExistamDadosNoArquivoDeSinais();

            repositorioDeSinaisEstaticos.Carregar();

            DeveTerCarregadoOsSinaisDoArquivo(sinais);
        }

        [TestMethod]
        public void buscando_um_sinal_pela_descricao()
        {
            var sinais = DadoQueExistamDadosNoArquivoDeSinais();
            
            repositorioDeSinaisEstaticos.Carregar();

            DevePoderBuscarOsSinaisPelaDescricao(sinais);
        }

        [TestMethod]
        public void adicionando_um_sinal()
        {
            DadoQueExistamDadosNoArquivoDeSinais();
            
            repositorioDeSinaisEstaticos.Carregar();

            var conteudoDoArquivoDeSinais = BuscarConteudoDoArquivoDeSinais();
            
            var sinal = DadoUmNovoSinal("New sign");
            repositorioDeSinaisEstaticos.Adicionar(sinal);

            BuscarConteudoDoArquivoDeSinais().Should().Be(conteudoDoArquivoDeSinais);

            repositorioDeSinaisEstaticos.SalvarAlteracoes();

            BuscarConteudoDoArquivoDeSinais().Should().NotBe(conteudoDoArquivoDeSinais);
            BuscarConteudoDoArquivoDeSinais().Length.Should().BeGreaterThan(conteudoDoArquivoDeSinais.Length);
        }

        [TestMethod]
        public void adicionando_um_sinal_e_buscando()
        {
            var sinais = DadoQueExistamDadosNoArquivoDeSinais();
            
            repositorioDeSinaisEstaticos.Carregar();

            var descricaoDoSinal = "New sign";
            var indiceDoSinal = sinais.Count;

            var sinal = DadoUmNovoSinal(descricaoDoSinal);
            repositorioDeSinaisEstaticos.Adicionar(sinal);

            repositorioDeSinaisEstaticos.Quantidade.Should().Be(sinais.Count + 1);
            repositorioDeSinaisEstaticos.BuscarPorDescricao(descricaoDoSinal).Descricao.Should().Be(descricaoDoSinal);
            repositorioDeSinaisEstaticos.BuscarPorIndice(indiceDoSinal).Descricao.Should().Be(descricaoDoSinal);
        }

        [TestMethod]
        public void enumerando_os_itens_do_repositorio()
        {
            DadoQueExistamDadosNoArquivoDeSinais();
            
            repositorioDeSinaisEstaticos.Carregar();

            int indice = 0;

            foreach (var sinal in repositorioDeSinaisEstaticos)
            {
                sinal.Should().Be(repositorioDeSinaisEstaticos.BuscarPorIndice(indice));
                indice++;
            }
        }

        [TestMethod]
        public void carregando_um_arquivo_vazio()
        {
            DadoQueOArquivoDeSinaisEstejaVazio();

            Action acaoDeCarregar = () => repositorioDeSinaisEstaticos.Carregar();
            Action acaoDeBuscarPorIndice = () => repositorioDeSinaisEstaticos.BuscarPorIndice(0);
            Action acaoDeBuscarPorDescricao = () => repositorioDeSinaisEstaticos.BuscarPorDescricao("");

            acaoDeCarregar.ShouldNotThrow();
            acaoDeBuscarPorIndice.ShouldNotThrow();
            acaoDeBuscarPorDescricao.ShouldNotThrow();
        }

        [TestMethod]
        public void carregando_dados_quando_arquivo_nao_existe()
        {
            DadoQueOArquivoDeSinaisNaoExista();

            Action acao = () => repositorioDeSinaisEstaticos.Carregar();

            acao.ShouldNotThrow();
        }

        [TestMethod]
        public void salvando_alteracoes_quando_o_arquivo_nao_existe()
        {
            DadoQueOArquivoDeSinaisNaoExista();

            var sinal = DadoUmNovoSinal("saving sign");
            repositorioDeSinaisEstaticos.Adicionar(sinal);

            Action acao = () => repositorioDeSinaisEstaticos.SalvarAlteracoes();

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
            var signs = new ColecaoDeSinaisEstaticosBuilder()
                            .ComQuantidadeDeSinais(4)
                            .ComTemplateDeDescricao(TemplateDaDescricao)
                            .ComTemplateDoCaminhoDoArquivoDeExemplo(TemplateDoCaminhoDeExemplo)
                            .Construir();

            var json = JsonConvert.SerializeObject(signs);

            using (StreamWriter writer = new StreamWriter(CaminhoDoArquivoDeDeAmostras))
            {
                writer.Write(json);
            }

            return signs;
        }

        private Sinal DadoUmNovoSinal(string descricao)
        {
            var sign = new SinalBuilder()
                            .ComDescricao(descricao)
                            .ComCaminhoParaArquivoDeExemplo("new-sign.json")
                            .ComAmostra(new AmostraBuilder().Construir())
                            .Construir();
            return sign;
        }

        private void DevePoderBuscarOsSinaisPelaDescricao(ICollection<Sinal> signs)
        {
            string signId;
            for (int i = 0; i < signs.Count; i++)
            {
                signId = String.Format(TemplateDaDescricao, i);
                repositorioDeSinaisEstaticos.BuscarPorDescricao(signId).Descricao.Should().Be(signId);
            }
        }

        private void DeveTerCarregadoOsSinaisDoArquivo(ICollection<Sinal> signs)
        {
            repositorioDeSinaisEstaticos.Quantidade.Should().Be(signs.Count);
            for (int i = 0; i < signs.Count; i++)
            {
                repositorioDeSinaisEstaticos
                    .BuscarPorIndice(i)
                    .Should()
                    .Match<Sinal>(sign =>
                        sign.Descricao == String.Format(TemplateDaDescricao, i) &&
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
