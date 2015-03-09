using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Signa.Dados.Repositorio;
using Signa.Dominio.Sinais;
using Testes.Comum.Builders.Dominio.Sinais;

namespace Testes.Integracao.Dados.Repositorio
{
    [TestClass]
    public class RepositorioFactoryTeste
    {
        private const string CaminhoDoArquivoDeDeAmostras = "Integracao/JsonTestData/repositorio-factory-teste.json";
        private const string TemplateDaDescricao = "Static sign sample {0}";
        private const string TemplateDoCaminhoDoArquivoDeExemplo = "static-sample-{0}.json";

        [TestMethod]
        public void criando_e_carregando_um_repositorio()
        {
            var sinais = DadoQueExistamAlgunsSinaisNoArquivoDeExemplos();

            var fabrica = new RepositorioFactory(CaminhoDoArquivoDeDeAmostras);

            var repositorioDeSinaisEstaticos = fabrica.CriarECarregarRepositorioDeSinaisEstaticos();

            repositorioDeSinaisEstaticos.Quantidade.Should().Be(sinais.Count);
        }

        [TestMethod]
        public void criando_o_repositorio_duas_vezes()
        {
            var fabrica = new RepositorioFactory(CaminhoDoArquivoDeDeAmostras);

            var repositorioDeSinaisEstaticos1 = fabrica.CriarECarregarRepositorioDeSinaisEstaticos();
            var repositorioDeSinaisEstaticos2 = fabrica.CriarECarregarRepositorioDeSinaisEstaticos();

            repositorioDeSinaisEstaticos1.Should().BeSameAs(repositorioDeSinaisEstaticos2);
        }

        private ICollection<Sinal> DadoQueExistamAlgunsSinaisNoArquivoDeExemplos()
        {
            var signs = new ColecaoDeSinaisEstaticosBuilder()
                            .ComQuantidadeDeSinais(4)
                            .ComTemplateDeDescricao(TemplateDaDescricao)
                            .ComTemplateDoCaminhoDoArquivoDeExemplo(TemplateDoCaminhoDoArquivoDeExemplo)
                            .Construir();

            var json = JsonConvert.SerializeObject(signs);

            using (StreamWriter writer = new StreamWriter(CaminhoDoArquivoDeDeAmostras))
            {
                writer.Write(json);
            }

            return signs;
        }
    }
}