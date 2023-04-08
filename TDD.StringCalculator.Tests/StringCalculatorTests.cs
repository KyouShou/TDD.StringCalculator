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
    }
}