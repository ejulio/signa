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
    public class RepositorioDeSinaisDinamicosTeste
    {
        private RepositorioDeSinaisDinamicos repositorioDeSinaisDinamicos;
        private const string CaminhoDoArquivoDeAmostras = Caminhos.CaminhoDoArquivoDeAmostras;
        private const string TemplateDaDescricao = "Sign sample {0}";
        private const string TemplateDoCaminhoDoArquivoDeExemplo = "sample-{0}.json";

        [TestInitialize]
        public void Setup()
        {
            repositorioDeSinaisDinamicos = new RepositorioDeSinaisDinamicos(CaminhoDoArquivoDeAmostras);
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
            DadoQueExistamAlgunsSinaisNoArquivoDeExemplo();
            
            repositorioDeSinaisDinamicos.Carregar();

            var conteudoDoArquivoDeSinais = RecuperarConteudoDoArquivoDeSinais();
            
            var sinal = DadoUmNovoSinal("New sign");
            repositorioDeSinaisDinamicos.Adicionar(sinal);

            RecuperarConteudoDoArquivoDeSinais().Should().Be(conteudoDoArquivoDeSinais);

            repositorioDeSinaisDinamicos.SalvarAlteracoes();

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
            var sinais = new ColecaoDeSinaisBuilder()
                            .ComQuantidadeDeSinais(4)
                            .ComTemplateDeDescricao(TemplateDaDescricao)
                            .ComTemplateDoCaminhoDoArquivoDeExemplo(TemplateDoCaminhoDoArquivoDeExemplo)
                            .Construir();

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
                            .ComAmostra(new AmostraBuilder().Construir())
                            .Construir();
            return sinal;
        }

        private void DevePoderBuscarUmSinalPelaDescricao(ICollection<Sinal> sinais)
        {
            string idSinal;
            for (int i = 0; i < sinais.Count; i++)
            {
                idSinal = String.Format(TemplateDaDescricao, i);
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
                        sinal.Descricao == String.Format(TemplateDaDescricao, i) &&
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
