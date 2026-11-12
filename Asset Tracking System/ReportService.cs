using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetTracking
{
    public class ReportService
    {
        // Entry point for printing - calls main report generator
        public void PrintReport(List<Asset> assets)
        {
            GenerateReport(assets);
        }

        // Generate full report sorted by Office and Purchase Date
        public void GenerateReport(List<Asset> assets)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=================================================================");
            Console.WriteLine("              ASSET TRACKING SYSTEM - FULL REPORT");
            Console.WriteLine("=================================================================");
            Console.ResetColor();

            // Check if asset list is empty
            if (assets == null || assets.Count == 0)
            {
                Console.WriteLine("\n  No assets found!");
                return;
            }

            // Sort assets by Office Name (Sweden, Turkey, USA) then by Purchase Date
            var sorted = assets.OrderBy(a => a.Office.Name).ThenBy(a => a.PurchaseDate).ToList();

            // Print separate sections for each asset type with different colors
            PrintReportForType(sorted, AssetType.Computer, "COMPUTERS", ConsoleColor.Cyan);
            PrintReportForType(sorted, AssetType.MobilePhone, "MOBILE PHONES", ConsoleColor.Yellow);
            PrintReportForType(sorted, AssetType.Tablet, "TABLETS", ConsoleColor.Green);

            // Print summary footer with totals
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n=================================================================");
            Console.WriteLine($"  TOTAL: {assets.Count} | Computers: {assets.Count(a => a.Type == AssetType.Computer)} | Mobiles: {assets.Count(a => a.Type == AssetType.MobilePhone)} | Tablets: {assets.Count(a => a.Type == AssetType.Tablet)}");
            Console.WriteLine("=================================================================");
            Console.ResetColor();

            // LEGEND - explain color coding
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  ■ RED");
            Console.ResetColor();
            Console.WriteLine(" = Expired / Less than 90 days left (End of Life passed)");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  ■ YELLOW");
            Console.ResetColor();
            Console.WriteLine(" = Close to End of Life (Less than 180 days)");
        }

        // Print report for a specific asset type (Computer, Mobile, Tablet)
        private void PrintReportForType(List<Asset> assets, AssetType type, string title, ConsoleColor color)
        {
            // Filter assets by type
            var filtered = assets.Where(a => a.Type == type).ToList();
            if (filtered.Count == 0) return;

            // Print section header with color
            Console.ForegroundColor = color;
            Console.WriteLine($"\n---------------- {title} ({filtered.Count}) ----------------");
            Console.WriteLine($"{"ID",-8} {"Brand",-12} {"Model",-15} {"Office",-10} {"Purchase",-12} {"Price",-12} {"EOL",-12} {"Days Left"}");
            Console.WriteLine(new string('-', 95));
            Console.ResetColor();

            // Loop through filtered assets and print details
            foreach (var asset in filtered)
            {
                // Color coding based on remaining life
                // Red = less than 90 days to End of Life
                // Yellow = less than 180 days to End of Life
                if (asset.DaysRemaining < 90) Console.ForegroundColor = ConsoleColor.Red;
                else if (asset.DaysRemaining < 180) Console.ForegroundColor = ConsoleColor.Yellow;

                // Use Office Name (not Country property) - shows USA/Sweden/Turkey
                string officeName = asset.Office.Name;

                // Print asset row: ID, Brand, Model, Office, Dates, Price, Days left
                Console.WriteLine($"{asset.Id,-8} {asset.Brand,-12} {asset.Model,-15} {officeName,-10} {asset.PurchaseDate:yyyy-MM-dd} {asset.Price} {asset.Office.Currency,-8} {asset.EndOfLifeDate:yyyy-MM-dd} {asset.DaysRemaining:F0}d");
                Console.ResetColor();

            }
        }
    }
}