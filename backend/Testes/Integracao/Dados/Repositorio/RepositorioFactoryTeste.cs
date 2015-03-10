﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Signa.Dados.Repositorio;
using Signa.Dominio.Sinais;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Integracao.Dados.Repositorio
{
    [TestClass]
    public class RepositorioFactoryTeste
    {
        private const string CaminhoDoArquivoDeDeAmostras = "Integracao/JsonTestData/repositorio-factory-teste.json";
        private const string TemplateDaDescricaoDeSinalEstatico = "sinal estático {0}";
        private const string TemplateDaDescricaoDeSinalDinamico = "sinal dinâmico {0}";
        private const string TemplateDoCaminhoDoArquivoDeExemplo = "exemplo-{0}.json";

        [TestMethod]
        public void criando_e_carregando_um_repositorio_de_sinais_estaticos()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplos();

            var fabrica = new RepositorioFactory(CaminhoDoArquivoDeDeAmostras);

            var repositorioDeSinaisEstaticos = fabrica.CriarECarregarRepositorioDeSinaisEstaticos();

            repositorioDeSinaisEstaticos.Quantidade.Should().Be(sinais.Count(s => s.Tipo == TipoSinal.Estatico));
        }

        [TestMethod]
        public void criando_e_carregando_um_repositorio_de_sinais_dinamicos()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplos();

            var fabrica = new RepositorioFactory(CaminhoDoArquivoDeDeAmostras);

            var repositorioDeSinaisDinamicos = fabrica.CriarECarregarRepositorioDeSinaisDinamicos();

            repositorioDeSinaisDinamicos.Quantidade.Should().Be(sinais.Count(s => s.Tipo == TipoSinal.Dinamico));
        }

        [TestMethod]
        public void criando_o_repositorio_de_sinais_estaticos__duas_vezes()
        {
            var fabrica = new RepositorioFactory(CaminhoDoArquivoDeDeAmostras);

            var repositorioDeSinaisDinamicos1 = fabrica.CriarECarregarRepositorioDeSinaisEstaticos();
            var repositorioDeSinaisDinamicos2 = fabrica.CriarECarregarRepositorioDeSinaisEstaticos();

            repositorioDeSinaisDinamicos1.Should().BeSameAs(repositorioDeSinaisDinamicos2);
        }

        [TestMethod]
        public void criando_o_repositorio_de_sinais_dinamicos__duas_vezes()
        {
            var fabrica = new RepositorioFactory(CaminhoDoArquivoDeDeAmostras);

            var repositorioDeSinaisEstaticos1 = fabrica.CriarECarregarRepositorioDeSinaisDinamicos();
            var repositorioDeSinaisEstaticos2 = fabrica.CriarECarregarRepositorioDeSinaisDinamicos();

            repositorioDeSinaisEstaticos1.Should().BeSameAs(repositorioDeSinaisEstaticos2);
        }

        private ICollection<Sinal> DadoQueExistamAlgunsSinaisNoArquivoDeExemplos()
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

            var sinais = sinaisEstaticos.Concat(sinaisDinamicos).ToList();

            var json = JsonConvert.SerializeObject(sinais);

            using (StreamWriter writer = new StreamWriter(CaminhoDoArquivoDeDeAmostras))
            {
                writer.Write(json);
            }

            return sinais;
        }
    }
}