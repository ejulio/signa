﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Caracteristicas;
using Signa.Testes.Comum.Builders.Dominio.Caracteristicas;
using System.Linq;

namespace Signa.Testes.Unidade.Dominio.Caracteristicas
{
    [TestClass]
    public class HandTest
    {
        [TestMethod]
        public void returning_an_array_with_samples_properties()
        {
            var palmNormal = new[] { 0.123, 0.556, 0.897 };
            var handDirection = new[] { 0.896, 0.132, 0.745 };
            var fingers = new[] 
            { 
                DedoBuilder.Dedao(), 
                DedoBuilder.Indicador(), 
                DedoBuilder.Meio(), 
                DedoBuilder.Anelar(), 
                DedoBuilder.Mindinho() 
            };

            var hand = new Mao
            {
                PalmNormal = palmNormal,
                HandDirection = handDirection,
                Dedos = fingers
            };

            var handData = hand.ToArray();

            var expectedHandData = GivenExpectedHandDataFor(palmNormal, handDirection, fingers);

            handData.Should().HaveSameCount(expectedHandData);
            handData.Should().ContainInOrder(expectedHandData);
        }

        private double[] GivenExpectedHandDataFor(double[] palmNormal, double[] handDirection, Dedo[] dedos)
        {
            return palmNormal
                .Concat(handDirection)
                .Concat(dedos[0].ToArray())
                .Concat(dedos[1].ToArray())
                .Concat(dedos[2].ToArray())
                .Concat(dedos[3].ToArray())
                .Concat(dedos[4].ToArray())
                .ToArray();
        }
    }
}
