using System;
using System.Collections.Generic;

namespace AssetTracking;

public abstract class Asset
{
    // Static counters to keep MON01, COM01 unique - start from 01
    private static readonly Dictionary<string, int> _counters = new();

    public string Id { get; private set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int Price { get; set; } // Price in local currency
    public Office Office { get; set; } // Office where asset is located
    public abstract AssetType Type { get; } // Must be defined by child
    public abstract int LifeSpanYears { get; } // Lifespan, business rule = 3 years

    protected Asset(string brand, string model, DateTime purchaseDate, int price, Office office)
    {
        Brand = brand;
        Model = model;
        PurchaseDate = purchaseDate;
        Price = price;
        Office = office;

        // Generate unique prefixed ID: MOB01, COM01, etc.
        Id = GenerateId();
    }

    private string GenerateId()
    {
        string prefix = Type switch
        {
            AssetType.Computer => "COM",
            AssetType.MobilePhone => "MOB",
            AssetType.Tablet => "TAB",
            _ => "AST"
        };

        // For Monitor? If you have Monitor type, add:
        // AssetType.Monitor => "MON"

        if (!_counters.ContainsKey(prefix))
            _counters[prefix] = 0;

        _counters[prefix]++;

        return $"{prefix}{_counters[prefix]:D2}"; // MON01, MON02... D2 = 01 format
    }

    // Calculates when asset reaches end of life
    public DateTime EndOfLifeDate => PurchaseDate.AddYears(LifeSpanYears);

    // Calculates current age of asset
    public TimeSpan Age => DateTime.Now - PurchaseDate;

    // Days remaining until end of life
    public double DaysRemaining => (EndOfLifeDate - DateTime.Now).TotalDays;

    // Each office defines its own currency symbol display
    public abstract string GetCurrencySymbol();

    public override string ToString()
    {
        return $"{Id} - {Brand} {Model} - {Office} - {Price}{GetCurrencySymbol()} - EOL: {EndOfLifeDate:yyyy-MM-dd}";
    }
}