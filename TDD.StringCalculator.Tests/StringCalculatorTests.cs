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

        [TestCase("175.2,\n35", "6")]
        [TestCase("175\n\n2,\n35", "4")]
        [TestCase("175..2,\n35", "4")]
        public void Add_Given_ContinuousSplitChar_Returns_Exception(string inputString, string expectedErrorIndex)
        {
            var exception = Assert.Throws<Exception>(() => _stringCalculator.Add(inputString));

            string exceptionMessage = exception.Message;

            StringAssert.Contains("位置", exceptionMessage);
            StringAssert.Contains(expectedErrorIndex, exceptionMessage);
            StringAssert.Contains("應為數字", exceptionMessage);
        }
    }
}