namespace AssetTracking;

// Concrete asset type - Computer
public class Computer : Asset
{
    public Computer(string brand, string model, DateTime purchaseDate, int price, Office office)
        : base(brand, model, purchaseDate, price, office) { }

    public override AssetType Type => AssetType.Computer;
    public override int LifeSpanYears => 3; // Business rule: 3 years
    public override string GetCurrencySymbol() => Office.Currency;
}