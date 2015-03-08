﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Caracteristicas;

namespace Signa.Testes.Unidade.Dominio.Caracteristicas
{
    [TestClass]
    public class FingerTest
    {
        [TestMethod]
        public void returning_an_array_with_finger_data()
        {
            var finger = new Dedo
            {
                Tipo = TipoDeDedo.Dedao,
                Direcao = new[] { 0.12, 0.478, 0.6985 }
            };

            var fingerArray = finger.ToArray();

            var expectedFingerArray = new double[] { (int)TipoDeDedo.Dedao, 0.12, 0.478, 0.6985 };

            fingerArray.Should().HaveSameCount(expectedFingerArray);
            fingerArray.Should().ContainInOrder(expectedFingerArray);
        }
    }
}
