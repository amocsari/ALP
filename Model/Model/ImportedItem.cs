namespace Common.Model
{
    /// <summary>
    /// The data of a single item imported from an excel sheet
    /// </summary>
    public class ImportedItem
    {
        public int? YellowNumber { get; set; }
        public string InventoryNumber { get; set; }
        public string OldInventoryNumber { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string OwnerName { get; set; }
    }
}
