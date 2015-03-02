using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Signa.Util;
using System.Linq;

namespace Signa.Tests.Util
{
    [TestClass]
    public class IEnumerableExtensionsTest
    {
        [TestMethod]
        public void concatenating_arrays_of_double()
        {
            var arrays = new[]
            {
                new [] { 0.8, 0.7, 0.5 },
                new [] { 1.4, 1.6, 1.8 }
            };

            var concatenatedArray = arrays.Concatenate().ToArray();

            var expectedArray = new[] { 0.8, 0.7, 0.5, 1.4, 1.6, 1.8 };

            concatenatedArray.Should().HaveSameCount(expectedArray);
            concatenatedArray.Should().ContainInOrder(expectedArray);
        }
    }
}