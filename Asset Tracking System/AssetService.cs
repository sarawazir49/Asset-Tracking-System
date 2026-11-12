using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AssetTracking
{
    public class AssetService
    {
        // In-memory list of all assets
        private List<Asset> _assets = new();

        // Add asset and auto-save to JSON immediately
        public void AddAsset(Asset asset)
        {
            _assets.Add(asset);
            SaveToJson(); // Auto-save on every add
        }

        // Get assets sorted by Office (USA, Sweden, Turkey) then by Purchase Date
        public List<Asset> GetAssetsSorted()
        {
            return _assets.OrderBy(a => a.Office.Name).ThenBy(a => a.PurchaseDate).ToList();
        }

        // Get all assets without sorting
        public List<Asset> GetAllAssets()
        {
            return _assets;
        }

        // Save all assets to JSON file
        public void SaveToJson(string fileName = "assets.json")
        {
            try
            {
                // Convert assets to simple DTO for serialization
                var data = _assets.Select(a => new JsonAsset
                {
                    Type = a.Type.ToString(),
                    Brand = a.Brand,
                    Model = a.Model,
                    PurchaseDate = a.PurchaseDate,
                    Price = a.Price,
                    OfficeName = a.Office.Name,
                    OfficeCurrency = a.Office.Currency,
                    OfficeRate = a.Office.ExchangeRate
                }).ToList();

                // Serialize with indentation for readability
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
            }
            catch
            {
                // Ignore save errors to prevent app crash
            }
        }

        // Load assets from JSON file
        public void LoadFromJson(string fileName = "assets.json")
        {
            try
            {
                // If file does not exist, do nothing (first run)
                if (!File.Exists(fileName)) return;

                string json = File.ReadAllText(fileName);
                if (string.IsNullOrWhiteSpace(json)) return;

                var data = JsonSerializer.Deserialize<List<JsonAsset>>(json);
                if (data == null || data.Count == 0) return;

                // Clear current list and rebuild from file
                _assets.Clear();
                foreach (var item in data)
                {
                    // Recreate Office object
                    Office office = new Office(item.OfficeName, item.OfficeCurrency, item.OfficeRate);

                    // Recreate correct asset type based on saved Type
                    Asset? asset = item.Type switch
                    {
                        "Computer" => new Computer(item.Brand, item.Model, item.PurchaseDate, item.Price, office),
                        "MobilePhone" => new MobilePhone(item.Brand, item.Model, item.PurchaseDate, item.Price, office),
                        "Tablet" => new Tablet(item.Brand, item.Model, item.PurchaseDate, item.Price, office),
                        _ => null // Unknown type
                    };

                    if (asset != null) _assets.Add(asset);
                }
            }
            catch
            {
                // Ignore load errors (corrupted file, etc.)
            }
        }
    }

    // DTO class for JSON serialization - must be outside AssetService but inside namespace
    public class JsonAsset
    {
        public string Type { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public DateTime PurchaseDate { get; set; }
        public int Price { get; set; }
        public string OfficeName { get; set; } = "";
        public string OfficeCurrency { get; set; } = "";
        public double OfficeRate { get; set; }
    }
}