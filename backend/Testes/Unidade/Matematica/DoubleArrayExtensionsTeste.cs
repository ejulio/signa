using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Dominio.Util.Matematica;

namespace Testes.Unidade.Matematica
{
    [TestClass]
    public class DoubleArrayExtensions
    {
        [TestMethod]
        public void calculando_a_magnitude_de_um_vetor_3d()
        {
            var vetor = new[] { 1.0, 2.0, 3.0 };

            var magnitude = vetor.Magnitude();

            magnitude.Should().Be(Math.Sqrt(14.0));
        }

        [TestMethod]
        public void calculando_a_magnitude_de_um_vetor_5d()
        {
            var vetor = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

            var magnitude = vetor.Magnitude();

            magnitude.Should().Be(Math.Sqrt(55.0));
        }

        [TestMethod]
        public void calculando_o_produto_entre_dois_vetores_3d()
        {
            var vetor1 = new[] { 1.0, 2.0, 3.0 };
            var vetor2 = new[] { 2.0, 3.0, 1.0 };

            var produto = vetor1.ProdutoCom(vetor2);

            produto.Should().Be(11.0);
        }

        [TestMethod]
        public void calculando_o_produto_entre_dois_vetores_5d()
        {
            var vetor1 = new[] { 1.0, 2.0, 3.0, 4.0, 6.0 };
            var vetor2 = new[] { 2.0, 3.0, 1.0, 5.0, 4.0 };

            var produto = vetor1.ProdutoCom(vetor2);

            produto.Should().Be(55.0);
        }

        [TestMethod]
        public void calculando_o_angulo_entre_dois_vetores_4d()
        {
            var vetor1 = new[] { 1.0, 2.0, 3.0, 4.0 };
            var vetor2 = new[] { 2.0, 3.0, 1.0, 5.0 };

            var angulo = vetor1.AnguloAte(vetor2);
            var produto = vetor1.ProdutoCom(vetor2);
            var magnitude = vetor1.Magnitude() * vetor2.Magnitude();

            angulo.Should().Be(Math.Acos(produto / magnitude));
        }

        [TestMethod]
        public void calculando_o_angulo_entre_dois_vetores_2d()
        {
            var vetor1 = new[] { 0.0, 2.0 };
            var vetor2 = new[] { 2.0, 0.0 };

            var angulo = vetor1.AnguloAte(vetor2);

            angulo.Should().BeApproximately(1.571, 3);
        }

        [TestMethod]
        public void calculando_o_angulo_entre_dois_vetores_quando_um_vetor_eh_a_origem()
        {
            var vetor1 = new[] { 1.0, 2.0, 3.0 };
            var vetor2 = new[] { 0.0, 0.0, 0.0 };

            var angulo = vetor1.AnguloAte(vetor2);

            angulo.Should().Be(0.0);
        }

        [TestMethod]
        public void normalizando_um_vetor()
        {
            var vetor = new[] { 8.9, 1.0, 10.3, 5.6, 7.02 };

            var vetorNormalizado = vetor.Normalizado();

            var magnitudeDoVetor = vetor.Magnitude();
            var vetorNormalizadoEsperado = new double[vetor.Length];
            for (var i = 0; i < vetorNormalizadoEsperado.Length; i++)
                vetorNormalizadoEsperado[i] = vetor[i] / magnitudeDoVetor;

            vetorNormalizado.Should().ContainInOrder(vetorNormalizadoEsperado);
        }

        [TestMethod]
        public void normalizando_um_vetor_com_magnitude_0()
        {
            var vetor = new[] { 0.0, 0.0, 0.0 };

            var vetorNormalizado = vetor.Normalizado();

            var vetorNormalizadoEsperado = new[] { 0.0, 0.0, 0.0 };
            vetorNormalizado.Should().ContainInOrder(vetorNormalizadoEsperado);
        }

        [TestMethod]
        public void multiplicando_por_um_valor_escalar()
        {
            var vetor = new[] { 0.0, 1.0, 2.9 };

            var valorEscalar = -1;
            var vetorMultiplicado = vetor.MultiplicarPor(valorEscalar);

            var vetorMultiplicadoEsperado = new double[vetor.Length];
            for (var i = 0; i < vetorMultiplicadoEsperado.Length; i++)
                vetorMultiplicadoEsperado[i] = vetor[i] * valorEscalar;

            vetorMultiplicado.Should().ContainInOrder(vetorMultiplicadoEsperado);
        }

        [TestMethod]
        public void somando_dois_vetores()
        {
            var vetor1 = new[] { 1.0, 2.0, 3.0 };
            var vetor2 = new[] { 0.0, 0.5, -1.0 };

            var vetoresSomados = vetor1.SomarCom(vetor2);

            var vetoresSomadosEsperado = vetor1.Select((valor, indice) => valor + vetor2[indice]);
            vetoresSomados.Should().ContainInOrder(vetoresSomadosEsperado);
        }

        [TestMethod]
        public void subtraindo_dois_vetores()
        {
            var vetor1 = new[] { 10.3, 4.6, 7.8 };
            var vetor2 = new[] { 5.0, -2.0, 1.6 };

            var vetoresSubtraidos = vetor1.Subtrair(vetor2);

            vetoresSubtraidos[0].Should().Be(vetor1[0] - vetor2[0]);
            vetoresSubtraidos[1].Should().Be(vetor1[1] - vetor2[1]);
            vetoresSubtraidos[2].Should().Be(vetor1[2] - vetor2[2]);
        }

        [TestMethod]
        public void projetando_um_vetor_em_um_plano()
        {
            var vetorNormalDoPlano = new[] { 1.0, 1.0, 0.0 };
            var vetor = new[] { 15.6, 8.9, 2.3 };

            var vetorProjetadoNoPlano = vetor.ProjetadoNoPlano(vetorNormalDoPlano);

            vetorProjetadoNoPlano[0].Should().BeApproximately(3.35, 2);
            vetorProjetadoNoPlano[1].Should().BeApproximately(-3.349, 3);
            vetorProjetadoNoPlano[2].Should().Be(2.3);
        }
    }
}