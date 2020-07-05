using NUnit.Framework;
using Web_API.Models;

namespace Web_API
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void TestNegativeIntegerValue()
        {
            var res = TaxCalculatorModel.CheckNegativeValues(-1);
            Assert.That(res, Is.True);
        }

        [Test]
        public void TestNonNumericStringValue()
        {
            var res = TaxCalculatorModel.CheckNumericValues("A");
            Assert.That(res, Is.True);
        }
    }
}
