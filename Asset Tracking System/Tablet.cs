namespace AssetTracking;

// Optional extension - Tablet, inherits same rules
public class Tablet : Asset
{
    public Tablet(string brand, string model, DateTime purchaseDate, int price, Office office)
        : base(brand, model, purchaseDate, price, office) { }

    public override AssetType Type => AssetType.Tablet;
    public override int LifeSpanYears => 3;
    public override string GetCurrencySymbol() => Office.Currency;
}