using CurrencyCalculator;

namespace CurrencyCalculatorTest
{
    [TestClass]
    public class CalculatorTest
    {

        [TestMethod]
        public void CalculateValueTest_ShouldCalculateProperly()
        {
            Dictionary<string, Currency> currencies = Program.parseCurrencies("resources/currencies.xml");
            Calculator calculator = new Calculator(currencies);

            string currencyName = "USD";
            double valueInEur = 100;
            double expected = 101.31;

            double actual = calculator.CalculateValue(valueInEur, currencyName);
            
            Assert.AreEqual(expected, actual, 0.0001, "Value is not calculated properly.");
        }

        [TestMethod]
        public void CalculateValueTest_ShouldThrowArgumentException()
        {
            Dictionary<string, Currency> currencies = Program.parseCurrencies("resources/currencies.xml");
            Calculator calculator = new Calculator(currencies);

            // incorrect currency name
            string currencyName = "QWE";
            double valueInEur = 100;

            Assert.ThrowsException<System.ArgumentException>(() => calculator.CalculateValue(valueInEur, currencyName));
        }
    }
}