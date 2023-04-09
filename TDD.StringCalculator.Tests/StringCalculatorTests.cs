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
    }
}