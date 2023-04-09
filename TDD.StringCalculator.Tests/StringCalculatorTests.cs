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
            var messageKeywords = new List<string> { "竚", expectedErrorIndex, "莱计" };
            CatchExceptionsAndCheckMessage(inputString, messageKeywords);
        }

        [Test]
        public void Add_Given_MissingNumberInLstPosition_Return_Exception()
        {
            var inputString = "1,3,";

            var messageKeywords = new List<string> { "程", "ぃ", "だ澄じ" };
            CatchExceptionsAndCheckMessage(inputString, messageKeywords);
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

            var messageKeywords = new List<string> { "礚猭ㄏノ" };
            CatchExceptionsAndCheckMessage(inputString, messageKeywords);
        }

        [TestCase("-1,2")]
        [TestCase("2,-4,-5")]
        public void Add_Given_NegativeNumbers_Throw_Exception(string inputString)
        {
            var messageKeywords = new List<string> { "璽计" };
            CatchExceptionsAndCheckMessage(inputString, messageKeywords);
        }

        [Test]
        public void Add_Given_MultipleIllegalString_Throw_MultipleException()
        {
            var inputString = "-1,,2";
            var expectedIndex = "3";
            var messageKeywords = new List<string> { "璽计", "竚" , expectedIndex , "莱计" };
            CatchExceptionsAndCheckMessage(inputString, messageKeywords);
        }

        private void CatchExceptionsAndCheckMessage(string inputString, List<string> messageKeywords)
        {
            var exception = Assert.Throws<AggregateException>(() => _stringCalculator.Add(inputString));
            var exceptionList = exception.InnerExceptions.ToList();

            var exceptionMessage = exception.Message;

            foreach (var ex in exceptionList)
            {
                exceptionMessage += ex.Message;
            }

            foreach (var keyword in messageKeywords)
            {
                StringAssert.Contains(keyword, exceptionMessage);
            }
        }
    }
}