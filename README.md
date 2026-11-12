# Asset Tracking System

A console-based .NET application to track company assets (Computers, Mobile Phones, Tablets) across multiple offices: USA, Sweden, and Turkey.

## Features

- **Multi-Office Support**: USA (USD), Sweden (SEK), Turkey (TRY) with currency conversion
- **Asset Types**: Computer, MobilePhone, Tablet (inheritance from base Asset class)
- **Auto Save/Load**: All assets automatically saved to `assets.json` using System.Text.Json
- **Smart Reporting**: Sorted by Office Name then Purchase Date, grouped by type
- **End of Life Tracking**: 
    - Red color = < 90 days remaining
    - Yellow color = < 180 days remaining
    - End of Life = PurchaseDate + 3 years
- **OOP Principles**: Inheritance, Polymorphism, Encapsulation

## Project Structure
AssetTracking/
├── Asset.cs              # Abstract base class with Id, Brand, Model, PurchaseDate, Price, Office
├── Computer.cs           # Inherits from Asset
├── MobilePhone.cs        # Inherits from Asset
├── Tablet.cs             # Inherits from Asset
├── Office.cs             # Office class with currency conversion logic
├── AssetType.cs          # Computer, MobilePhone, Tablet
├── AssetService.cs       # Business logic + JSON Save/Load
├── ReportService.cs      # Report generation with color coding
├── Program.cs            # Main menu and user input
├── assets.json           # Auto-generated data file
└── README.mdjavascript
## How to Run

1. Clone or download the project
2. Open with Visual Studio 2022
3. Clean Build: `Build > Clean Solution > Rebuild Solution`
4. Run: `Ctrl + F5`

## Menu Options
=== ASSET TRACKING SYSTEM ===
Show All Assets       - Display full report grouped by typeAdd New ComputerAdd New Mobile PhoneAdd New TabletExitjavascript
When adding an asset you enter:
- Brand (e.g. Apple, Dell)
- Model (e.g. MacBook Pro, iPhone 15)
- Purchase Date (yyyy-mm-dd)
- Price in USD (auto-converted to local currency)
- Office (USA / Sweden / Turkey)

## JSON Storage Example

`assets.json`:
```json
[
  {
    "Type": "Computer",
    "Brand": "Apple",
    "Model": "MacBook Pro",
    "PurchaseDate": "2022-06-15T00:00:00",
    "Price": 26250,
    "OfficeName": "Sweden",
    "OfficeCurrency": "SEK",
    "OfficeRate": 10.5
  }
]

## Technical Requirements

.NET 10 System.Text.Json (included in .NET)System.Linq for sorting/filteringNotes
If assets.json exists, data is loaded on startupIf file doesn't exist, 3 sample assets are createdTo reset data, delete assets.json from bin/Debug/netX.X/ folderPrice is stored in local currency after USD conversionAuthor
Student Project - Asset Tracking with OOP and JSON Persistence
