namespace TDD.StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add_Given_EmptyString_Returns_O()
        {
            var stringCalculator = new StringCalculator();
            var actual = stringCalculator.Add(String.Empty);
            Assert.AreEqual("O", actual);
        }
    }
}