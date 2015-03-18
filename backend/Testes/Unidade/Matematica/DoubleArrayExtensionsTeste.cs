using System;
using Dominio.Matematica;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes.Unidade.Matematica
{
    [TestClass]
    public class DoubleArrayExtensions
    {
        [TestMethod]
        public void calculando_a_magnitude_de_um_vetor()
        {
            var vetor = new[] { 1.0, 2.0, 3.0 };

            var magnitude = vetor.Magnitude();

            magnitude.Should().Be(Math.Sqrt(14.0));
        }

        [TestMethod]
        public void calculando_o_produto_entre_dois_vetores()
        {
            var vetor1 = new[] { 1.0, 2.0, 3.0 };
            var vetor2 = new[] { 2.0, 3.0, 1.0 };

            var produto = vetor1.ProdutoCom(vetor2);

            produto.Should().Be(11.0);
        }

        [TestMethod]
        public void calculando_o_angulo_entre_dois_vetores()
        {
            var vetor1 = new[] { 1.0, 2.0, 3.0 };
            var vetor2 = new[] { 2.0, 3.0, 1.0 };

            var angulo = vetor1.AnguloAte(vetor2);
            var produto = vetor1.ProdutoCom(vetor2);
            var magnitude = vetor1.Magnitude() * vetor2.Magnitude();

            angulo.Should().Be(Math.Acos(produto / magnitude));
        }

        [TestMethod]
        public void calculando_o_angulo_entre_dois_vetores_quando_um_vetor_eh_a_origem()
        {
            var vetor1 = new[] { 1.0, 2.0, 3.0 };
            var vetor2 = new[] { 0.0, 0.0, 0.0 };

            var angulo = vetor1.AnguloAte(vetor2);

            angulo.Should().Be(0.0);
        }
    }
}