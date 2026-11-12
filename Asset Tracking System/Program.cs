using System;
using AssetTracking;

// Define offices with their currency and exchange rate (1 USD = X local)
Office usa = new Office("USA", "USD", 1.0);
Office sweden = new Office("Sweden", "SEK", 10.5);
Office turkey = new Office("Turkey", "TRY", 34.0);

// Create services for asset management and reporting
AssetService service = new AssetService();
ReportService report = new ReportService();

// Load existing assets from JSON file on startup (if file exists)
service.LoadFromJson("assets.json");

// If no data was loaded, add sample data for testing (runs only on first launch)
if (service.GetAllAssets().Count == 0)
{
    service.AddAsset(new Computer("Apple", "MacBook Pro", new DateTime(2022, 6, 15), (int)sweden.ConvertFromUSD(2500), sweden));
    service.AddAsset(new MobilePhone("Apple", "iPhone 15", new DateTime(2024, 9, 10), (int)usa.ConvertFromUSD(1200), usa));
    service.AddAsset(new Tablet("Samsung", "Tab 5", new DateTime(2024, 5, 01), (int)usa.ConvertFromUSD(1200), usa));
    service.SaveToJson("assets.json");
}

// Main menu loop
while (true)
{
    Console.Clear();
    Console.WriteLine("=== ASSET TRACKING SYSTEM ===");
    Console.WriteLine($"Assets: {service.GetAllAssets().Count}");
    Console.WriteLine("\n1. Show All Assets");
    Console.WriteLine("2. Add New Computer");
    Console.WriteLine("3. Add New Mobile Phone");
    Console.WriteLine("4. Add New Tablet");
    Console.WriteLine("5. Exit");
    Console.Write("\nChoose option: ");
    string choice = Console.ReadLine() ?? "1";

    if (choice == "1")
    {
        // Display sorted report (by Office, then by Purchase Date)
        Console.Clear();
        report.PrintReport(service.GetAssetsSorted());
        Console.WriteLine("\nPress Enter...");
        Console.ReadLine();
    }
    else if (choice == "2" || choice == "3" || choice == "4")
    {
        // Collect asset details from user input
        Console.Write("\nBrand: ");
        string brand = Console.ReadLine() ?? "Unknown";

        Console.Write("Model: ");
        string model = Console.ReadLine() ?? "Unknown";

        Console.Write("Purchase Date (yyyy-mm-dd): ");
        DateTime date;
        if (!DateTime.TryParse(Console.ReadLine(), out date)) date = DateTime.Now;

        Console.Write("Price in USD ($): ");
        int usdPrice;
        if (!int.TryParse(Console.ReadLine(), out usdPrice)) usdPrice = 1000;

        Console.Write("Office (USA / Sweden / Turkey): ");
        string officeName = (Console.ReadLine() ?? "USA").ToLower();
        Office selectedOffice = officeName == "sweden" ? sweden : officeName == "turkey" ? turkey : usa;

        // Convert USD price to local currency
        int localPrice = (int)selectedOffice.ConvertFromUSD(usdPrice);

        // Create asset based on user choice
        if (choice == "2") service.AddAsset(new Computer(brand, model, date, localPrice, selectedOffice));
        else if (choice == "3") service.AddAsset(new MobilePhone(brand, model, date, localPrice, selectedOffice));
        else service.AddAsset(new Tablet(brand, model, date, localPrice, selectedOffice));

        // Auto-save to JSON file after adding
        service.SaveToJson("assets.json");

        Console.WriteLine($" Added & Auto-Saved! {localPrice} {selectedOffice.Currency}");
        Console.ReadLine();
    }
    else if (choice == "5") break; // Exit application
}