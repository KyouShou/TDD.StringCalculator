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

        [TearDown]
        public void End()
        {
            _stringCalculator = null;
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

        [Test]
        public void Add_Given_MissingNumberInLstPosition_Return_Exception()
        {
            var inputString = "1,3,";

            var exception = Assert.Throws<Exception>(() => _stringCalculator.Add(inputString));

            string exceptionMessage = exception.Message;

            StringAssert.Contains("最後", exceptionMessage);
            StringAssert.Contains("不可", exceptionMessage);
            StringAssert.Contains("分割字元", exceptionMessage);
        }

        [TestCase(";", "1;2", "3")]
        [TestCase("|", "1|2|3", "6")]
        [TestCase("sep", "2sep3", "5")]
        public void SetCustomSeparators_Returns_Sum(string customSaparators, string inputString, string expexted)
        {
            _stringCalculator.SetCustomSeparators(customSaparators);

            var actual = _stringCalculator.Add(inputString);

            Assert.AreEqual(expexted, actual);
        }

        [TestCase("|", "1|2,3")]
        public void Add_Given_InvalidSeparators_Throw_Exception(string customSaparators, string inputString)
        {
            _stringCalculator.SetCustomSeparators(customSaparators);

            var exception = Assert.Throws<Exception>(() => _stringCalculator.Add(inputString));
            var exceptionMessage = exception.Message;

            StringAssert.Contains("無法使用", exceptionMessage);
        }

        [TestCase("-1,2")]
        [TestCase("2,-4,-5")]
        public void Add_Given_NegativeNumbers_Throw_Exception(string inputString)
        {
            var exception = Assert.Throws<Exception>(() => _stringCalculator.Add(inputString));
            var exceptionMessage = exception.Message;

            StringAssert.Contains("負數", exceptionMessage);
        }
    }
}