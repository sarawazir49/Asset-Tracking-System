namespace AssetTracking
{
    public class Office
    {
        // Office location name (USA, Sweden, Turkey)
        public string Name { get; set; }

        // Local currency code (USD, SEK, TRY)
        public string Currency { get; set; }

        // Exchange rate: how much 1 USD is worth in local currency
        public double ExchangeRate { get; set; }

        // Constructor to create a new office with name, currency and rate
        public Office(string name, string currency, double rate)
        {
            Name = name;
            Currency = currency;
            ExchangeRate = rate;
        }

        // Convert price from USD to local currency
        // Example: 1000 USD * 10.5 = 10500 SEK
        public double ConvertFromUSD(double usd) => usd * ExchangeRate;

        // Return office name when object is converted to string
        // Useful for sorting and displaying: USA / Sweden / Turkey
        public override string ToString() => Name;
    }
}