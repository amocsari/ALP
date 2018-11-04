using System;
using System.Collections.Generic;
using Model.Enum;

namespace Common.Model
{
    public class InventoryItemFilterInfo
    {
        public List<string> Id { get; set; }
        public string ManufacturerAndType { get; set; }
        public int? BruttoPriceMin { get; set; }
        public int? BruttoPriceMax { get; set; }
        public DateTime? DateOfCreationMin { get; set; }
        public DateTime? DateOfCreationMax { get; set; }
        //TODO: int?
        public DateTime? YearOfManufactureMin { get; set; }
        public DateTime? YearOfManufactureMax { get; set; }
        public DateTime? DateOfScrapMin { get; set; }
        public DateTime? DateOfScrapMax { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<ItemPropertyType> SelectedProperties { get; set; }
    }
}
