﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Domain.Algorithms;

namespace Signa.Tests.Domain.Algorithms
{
    [TestClass]
    public class SignRecognitionAlgorithmFactoryTest
    {
        [TestMethod]
        public void creating_static_sign_recognition_algorithm()
        {
            var factory = new SignRecognitionAlgorithmFactory();

            var algorithm1 = factory.CreateStaticSignRecognizer();
            var algorithm2 = factory.CreateStaticSignRecognizer();

            algorithm1.Should().BeSameAs(algorithm2, "Retorna sempre a mesma classe como um Singleton");
        }
    }
}