using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculator
{
    public class Calculator
    {

        private Dictionary<string, Currency> available_currencies;

        public Dictionary<string, Currency> AvailableCurrencies
        {
            get { return available_currencies; }
            set
            {
                if (value is null || value.Count == 0) throw new ArgumentNullException("Null or empty list was provided.");
                available_currencies = value; 
            }
        }

        public Calculator(Dictionary<string, Currency> availableCurrencies)
        {
            AvailableCurrencies = availableCurrencies;
        }

        public double CalculateValue(double valueInEUR, string targetCurrencyName)
        {
            if (!IsCurrencyAvailable(targetCurrencyName)) throw new ArgumentException("Chosen target currency is not available.");

            double targetCurrencyRate = available_currencies[targetCurrencyName].Rate;

            return Math.Round(valueInEUR * targetCurrencyRate, 5);
        }

        private bool IsCurrencyAvailable(string currencyName)
        {
            return available_currencies.ContainsKey(currencyName);
        }
    }
}
