using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculator
{
    public class Currency
    {
        private string name;
        private double rate;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Rate
        {
            get { return rate; }
            set 
            {
                if (value <= 0) throw new ArgumentException("Rate's value is negative or equal to zero");
                rate = value; 
            }
        }

        public Currency(string name, double rate)
        {
            Name = name;
            Rate = rate;
        }

        public override string ToString()
        {
            return $"currency={name} rate={rate}";
        }
    }
}
