using CurrencyCalculator;
using System.Text;
using System.Xml;

public class Program
{
    public static void Main(string[] args)
    {
        Dictionary<string, Currency> currencies = parseCurrencies("resources/currencies.xml");
        Calculator calculator = new Calculator(currencies);

        while(true)
        {
            Console.WriteLine("Please enter amount in EUR:");
            string input = Console.ReadLine();
            if (input == null)
                Console.WriteLine("## Incorrect input.");
            else if (double.TryParse(input, out double valueInEUR) && valueInEUR > 0)
            {
                Console.WriteLine("Please enter currency name:");
                string currencyName = Console.ReadLine();
                while (true)
                {
                    if (currencyName is null || currencyName.Length != 3)
                    {
                        Console.WriteLine("## Incorrect input.");
                        currencyName = Console.ReadLine();
                    }
                    else
                    {
                        try
                        {
                            Console.WriteLine($"{valueInEUR} EUR in {currencyName.ToUpper()} is: " +
                                $"{calculator.CalculateValue(valueInEUR, currencyName.ToUpper())}");
                            break;
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("## Provided currency name is incorrect or not available.");
                            currencyName = Console.ReadLine();
                        }
                    }
                }
            }
            else if (input.ToLower().Equals("currencies") || input.ToLower().Equals("list"))
            {
                currencies.Values.ToList().ForEach(c => Console.WriteLine(c));
            }
            else
            {
                Console.WriteLine("## Incorrect input.");
            }
        }

    }

    public static Dictionary<string, Currency> parseCurrencies(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
        ns.AddNamespace("gesmes", "http://www.gesmes.org/xml/2002-08-01");
        ns.AddNamespace("eu", "http://www.ecb.int/vocabulary/2002-08-01/eurofxref");

        XmlNodeList nodeList = doc.SelectNodes("/gesmes:Envelope/eu:Cube/eu:Cube/eu:Cube", ns);
        Dictionary<string, Currency> currencies = new();
        foreach (XmlNode node in nodeList)
        {
            string currency = node.Attributes["currency"].Value;
            double rate = double.Parse(node.Attributes["rate"].Value);
            currencies.Add(currency, new Currency(currency, rate));
        }

        return currencies;
    }
}




