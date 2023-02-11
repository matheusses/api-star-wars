using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Matheusses.StarWars.UnitTest
{
    [TestFixture]
    public class MathTests
    {
        [Test]
        public void AdditionTest()
        {
            int result = 1 + 2;
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void SubtractionTest()
        {
            int result = 3 - 2;
            Assert.That(result, Is.EqualTo(1));
        }
    }
}
