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
    public class RepositorioDeSinaisDinamicosTeste
    {
        private RepositorioSinaisDinamicos repositorioSinaisDinamicos;
        private RepositorioSinais repositorioSinais;
        private const string CaminhoDoArquivoDeAmostras = "Integracao/JsonTestData/repositorio-sinais-dinamicos-teste.json";
        private const string TemplateDaDescricaoDeSinalEstatico = "sinal estático {0}";
        private const string TemplateDaDescricaoDeSinalDinamico = "sinal dinâmico {0}";
        private const string TemplateDoCaminhoDoArquivoDeExemplo = "sample-{0}.json";

        [TestInitialize]
        public void Setup()
        {
            repositorioSinais = new RepositorioSinais(CaminhoDoArquivoDeAmostras);
            repositorioSinaisDinamicos = new RepositorioSinaisDinamicos(repositorioSinais);
        }

        [TestMethod]
        public void carregando_sinais_do_arquivo()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();

            repositorioSinaisDinamicos.Carregar();

            DeveTerOsSinaisDoArquivo(sinais);
        }

        [TestMethod]
        public void buscando_um_sinal_pela_descricao()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioSinaisDinamicos.Carregar();

            DevePoderBuscarUmSinalPelaDescricao(sinais);
        }

        [TestMethod]
        public void adicionando_um_sinal()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioSinaisDinamicos.Carregar();

            var idEsperadoDoSinal = repositorioSinais.Quantidade;

            var conteudoDoArquivoDeSinais = RecuperarConteudoDoArquivoDeSinais();
            
            var sinal = DadoUmNovoSinal("New sign");
            repositorioSinaisDinamicos.Adicionar(sinal);

            RecuperarConteudoDoArquivoDeSinais().Should().Be(conteudoDoArquivoDeSinais);

            repositorioSinaisDinamicos.SalvarAlteracoes();

            sinal.Id.Should().Be(idEsperadoDoSinal);
            RecuperarConteudoDoArquivoDeSinais().Should().NotBe(conteudoDoArquivoDeSinais);
            RecuperarConteudoDoArquivoDeSinais().Length.Should().BeGreaterThan(conteudoDoArquivoDeSinais.Length);
        }

        [TestMethod]
        public void adicionando_um_sinal_e_buscando()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioSinaisDinamicos.Carregar();

            var descricaoDoSinal = "New sign";
            var indiceDoSinal = sinais.Count;

            var sinal = DadoUmNovoSinal(descricaoDoSinal);
            repositorioSinaisDinamicos.Adicionar(sinal);

            repositorioSinaisDinamicos.Quantidade.Should().Be(sinais.Count + 1);
            repositorioSinaisDinamicos.BuscarPorDescricao(descricaoDoSinal).Descricao.Should().Be(descricaoDoSinal);
            repositorioSinaisDinamicos.BuscarPorIndice(indiceDoSinal).Descricao.Should().Be(descricaoDoSinal);
        }

        [TestMethod]
        public void enumerando_o_repositorio()
        {
            DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioSinaisDinamicos.Carregar();

            int indice = 0;

            foreach (var sinal in repositorioSinaisDinamicos)
            {
                sinal.Should().Be(repositorioSinaisDinamicos.BuscarPorIndice(indice));
                indice++;
            }
        }

        [TestMethod]
        public void carregando_um_arquivo_de_sinais_vazio()
        {
            DadoQueOArquivoDeSinaisEstejaVazio();

            Action acaoDeCarregar = () => repositorioSinaisDinamicos.Carregar();
            Action acaoBuscarPorIndice = () => repositorioSinaisDinamicos.BuscarPorIndice(0);
            Action acaoBuscarPorId = () => repositorioSinaisDinamicos.BuscarPorDescricao("");

            acaoDeCarregar.ShouldNotThrow();
            acaoBuscarPorIndice.ShouldNotThrow();
            acaoBuscarPorId.ShouldNotThrow();
        }

        [TestMethod]
        public void carregando_quando_o_arquivo_nao_existe()
        {
            DadoQueOArquivoDeSinaisNaoExiste();

            Action acao = () => repositorioSinaisDinamicos.Carregar();

            acao.ShouldNotThrow();
        }

        [TestMethod]
        public void salvando_alteracoes()
        {
            DadoQueOArquivoDeSinaisNaoExiste();

            var sinal = DadoUmNovoSinal("saving sign");
            repositorioSinaisDinamicos.Adicionar(sinal);

            Action acao = () => repositorioSinaisDinamicos.SalvarAlteracoes();

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
                repositorioSinaisDinamicos.BuscarPorDescricao(idSinal).Descricao.Should().Be(idSinal);
            }
        }

        private void DeveTerOsSinaisDoArquivo(ICollection<Sinal> sinais)
        {
            repositorioSinaisDinamicos.Quantidade.Should().Be(sinais.Count);
            for (int i = 0; i < sinais.Count; i++)
            {
                repositorioSinaisDinamicos
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
