namespace AssetTracking;

// Concrete asset type - MobilePhone
public class MobilePhone : Asset
{
    public MobilePhone(string brand, string model, DateTime purchaseDate, int price, Office office)
        : base(brand, model, purchaseDate, price, office) { }

    public override AssetType Type => AssetType.MobilePhone;
    public override int LifeSpanYears => 3;
    public override string GetCurrencySymbol() => Office.Currency;
}