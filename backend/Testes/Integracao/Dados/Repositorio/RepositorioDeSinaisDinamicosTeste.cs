using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dominio.Dados.Repositorio;
using Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Integracao.Dados.Repositorio
{
    [TestClass]
    public class RepositorioDeSinaisDinamicosTeste
    {
        private RepositorioDeSinaisDinamicos repositorioDeSinaisDinamicos;
        private RepositorioDeSinais repositorioDeSinais;
        private const string CaminhoDoArquivoDeAmostras = "Integracao/JsonTestData/repositorio-sinais-dinamicos-teste.json";
        private const string TemplateDaDescricaoDeSinalEstatico = "sinal estático {0}";
        private const string TemplateDaDescricaoDeSinalDinamico = "sinal dinâmico {0}";
        private const string TemplateDoCaminhoDoArquivoDeExemplo = "sample-{0}.json";

        [TestInitialize]
        public void Setup()
        {
            repositorioDeSinais = new RepositorioDeSinais(CaminhoDoArquivoDeAmostras);
            repositorioDeSinaisDinamicos = new RepositorioDeSinaisDinamicos(repositorioDeSinais);
        }

        [TestMethod]
        public void carregando_sinais_do_arquivo()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();

            repositorioDeSinaisDinamicos.Carregar();

            DeveTerOsSinaisDoArquivo(sinais);
        }

        [TestMethod]
        public void buscando_um_sinal_pela_descricao()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioDeSinaisDinamicos.Carregar();

            DevePoderBuscarUmSinalPelaDescricao(sinais);
        }

        [TestMethod]
        public void adicionando_um_sinal()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioDeSinaisDinamicos.Carregar();

            var idEsperadoDoSinal = repositorioDeSinais.Quantidade;

            var conteudoDoArquivoDeSinais = RecuperarConteudoDoArquivoDeSinais();
            
            var sinal = DadoUmNovoSinal("New sign");
            repositorioDeSinaisDinamicos.Adicionar(sinal);

            RecuperarConteudoDoArquivoDeSinais().Should().Be(conteudoDoArquivoDeSinais);

            repositorioDeSinaisDinamicos.SalvarAlteracoes();

            sinal.Id.Should().Be(idEsperadoDoSinal);
            RecuperarConteudoDoArquivoDeSinais().Should().NotBe(conteudoDoArquivoDeSinais);
            RecuperarConteudoDoArquivoDeSinais().Length.Should().BeGreaterThan(conteudoDoArquivoDeSinais.Length);
        }

        [TestMethod]
        public void adicionando_um_sinal_e_buscando()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioDeSinaisDinamicos.Carregar();

            var descricaoDoSinal = "New sign";
            var indiceDoSinal = sinais.Count;

            var sinal = DadoUmNovoSinal(descricaoDoSinal);
            repositorioDeSinaisDinamicos.Adicionar(sinal);

            repositorioDeSinaisDinamicos.Quantidade.Should().Be(sinais.Count + 1);
            repositorioDeSinaisDinamicos.BuscarPorDescricao(descricaoDoSinal).Descricao.Should().Be(descricaoDoSinal);
            repositorioDeSinaisDinamicos.BuscarPorIndice(indiceDoSinal).Descricao.Should().Be(descricaoDoSinal);
        }

        [TestMethod]
        public void enumerando_o_repositorio()
        {
            DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioDeSinaisDinamicos.Carregar();

            int indice = 0;

            foreach (var sinal in repositorioDeSinaisDinamicos)
            {
                sinal.Should().Be(repositorioDeSinaisDinamicos.BuscarPorIndice(indice));
                indice++;
            }
        }

        [TestMethod]
        public void carregando_um_arquivo_de_sinais_vazio()
        {
            DadoQueOArquivoDeSinaisEstejaVazio();

            Action acaoDeCarregar = () => repositorioDeSinaisDinamicos.Carregar();
            Action acaoBuscarPorIndice = () => repositorioDeSinaisDinamicos.BuscarPorIndice(0);
            Action acaoBuscarPorId = () => repositorioDeSinaisDinamicos.BuscarPorDescricao("");

            acaoDeCarregar.ShouldNotThrow();
            acaoBuscarPorIndice.ShouldNotThrow();
            acaoBuscarPorId.ShouldNotThrow();
        }

        [TestMethod]
        public void carregando_quando_o_arquivo_nao_existe()
        {
            DadoQueOArquivoDeSinaisNaoExiste();

            Action acao = () => repositorioDeSinaisDinamicos.Carregar();

            acao.ShouldNotThrow();
        }

        [TestMethod]
        public void salvando_alteracoes()
        {
            DadoQueOArquivoDeSinaisNaoExiste();

            var sinal = DadoUmNovoSinal("saving sign");
            repositorioDeSinaisDinamicos.Adicionar(sinal);

            Action acao = () => repositorioDeSinaisDinamicos.SalvarAlteracoes();

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
            var sinaisDinamicos = new ColecaoDeSinaisBuilder()
                    .ComQuantidadeDeSinais(4)
                    .ComTemplateDeDescricao(TemplateDaDescricaoDeSinalDinamico)
                    .ComTemplateDoCaminhoDoArquivoDeExemplo(TemplateDoCaminhoDoArquivoDeExemplo)
                    .ComGeradorDeAmostrasDinamicas()
                    .Construir();

            var sinaisEstaticos = new ColecaoDeSinaisBuilder()
                    .ComQuantidadeDeSinais(4)
                    .ComTemplateDeDescricao(TemplateDaDescricaoDeSinalEstatico)
                    .ComTemplateDoCaminhoDoArquivoDeExemplo(TemplateDoCaminhoDoArquivoDeExemplo)
                    .ComGeradorDeAmostrasEstaticas()
                    .Construir();

            var sinais = sinaisDinamicos.Concat(sinaisEstaticos).ToList();

            var json = JsonConvert.SerializeObject(sinais);

            using (StreamWriter writer = new StreamWriter(CaminhoDoArquivoDeAmostras))
            {
                writer.Write(json);
            }

            return sinaisDinamicos;
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
            string idSinal;
            for (int i = 0; i < sinais.Count; i++)
            {
                idSinal = String.Format(TemplateDaDescricaoDeSinalDinamico, i);
                repositorioDeSinaisDinamicos.BuscarPorDescricao(idSinal).Descricao.Should().Be(idSinal);
            }
        }

        private void DeveTerOsSinaisDoArquivo(ICollection<Sinal> sinais)
        {
            repositorioDeSinaisDinamicos.Quantidade.Should().Be(sinais.Count);
            for (int i = 0; i < sinais.Count; i++)
            {
                repositorioDeSinaisDinamicos
                    .BuscarPorIndice(i)
                    .Should()
                    .Match<Sinal>(sinal =>
                        sinal.Descricao == String.Format(TemplateDaDescricaoDeSinalDinamico, i) &&
                        sinal.CaminhoParaArquivoDeExemplo == String.Format(TemplateDoCaminhoDoArquivoDeExemplo, i) &&
                        sinal.Amostras.Count == 4);
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
