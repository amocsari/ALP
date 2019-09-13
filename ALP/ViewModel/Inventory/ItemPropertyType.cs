using System.ComponentModel;

namespace ALP.ViewModel.Inventory
{
    public enum ItemPropertyType
    {
        ItemID = 0,
        [Description("Leltári szám")]
        InventoryNumber = 1,
        [Description("Vonalkód")]
        OldInventoryNumber = 2,
        [Description("Gyártási szám")]
        SerialNumber = 3,
        [Description("Akkreditációs szám")]
        AccreditationNumber = 4,
        [Description("Eszköz azonosító")]
        YellowNumber = 5,
        [Description("Név")]
        ItemName = 6,
        [Description("Gyártmány és típus")]
        ManufacturerModelType = 7,
        [Description("Eszköz Jelleg")]
        ItemNature = 8,
        [Description("Eszköz Típus")]
        ItemType = 9,
        [Description("Gyártás éve")]
        ProductionYear = 10,
        [Description("Osztály")]
        Department = 11,
        [Description("Részleg")]
        Section = 12,
        [Description("Alkalmazott")]
        Employee = 13,
        [Description("Épület")]
        Building = 14,
        [Description("Emelet")]
        Floor = 15,
        [Description("Szoba")]
        Room = 16,
        [Description("Állapot")]
        ItemState = 17,
        [Description("Nyilvántartásba vétel ideje")]
        DateOfCreation = 18,
        [Description("Bruttó ár")]
        BruttoPrice = 19,
        [Description("Megjegyzés")]
        Comment = 20,
        [Description("Selejtezés dátuma")]
        DateOfScrap = 21,
    }
}
