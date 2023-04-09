using NUnit.Framework;

namespace TDD.StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        StringCalculator _stringCalculator;

        [SetUp]
        public void Setup()
        {
            _stringCalculator = new StringCalculator();
        }

        [Test]
        public void Add_Given_EmptyString_Returns_O()
        {
            var actual = _stringCalculator.Add(String.Empty);
            Assert.AreEqual("O", actual);
        }

        [TestCase("1", "1")]
        [TestCase("1.1,2.2", "6")]
        [TestCase("1\n2,3", "6")]
        public void Add_Given_CorrectFormatInputString_Returns_SumOfNumber(string inputString, string expected)
        {
            var actual = _stringCalculator.Add(inputString);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Add_Given_ContinuousSplitChar_Returns_Exception()
        {
            var inputString = "175.2,\n35";

            var exception = Assert.Throws<Exception>(() => _stringCalculator.Add(inputString));

            string exceptionMessage = exception.Message;

            StringAssert.Contains("��m", exceptionMessage);
            StringAssert.Contains("6", exceptionMessage);
            StringAssert.Contains("�����Ʀr", exceptionMessage);
        }
    }
}