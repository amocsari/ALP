using System;

namespace Common.Model.Dto
{
    public class ItemDto
    {
        public int ItemID { get; set; }
        public string InventoryNumber { get; set; }
        public string OldInventoryNumber { get; set; }
        public string SerialNumber { get; set; }
        public string AccreditationNumber { get; set; }
        public int? YellowNumber { get; set; }
        public string ItemName { get; set; }
        public string Manufacturer { get; set; }
        public string ModelType { get; set; }
        public int ItemNatureID { get; set; }
        public int ItemTypeID { get; set; }
        public DateTime? ProductionYear { get; set; }
        public int? DepartementID { get; set; }
        public int? SectionID { get; set; }
        public int? EmployeeID { get; set; }
        public int? BuildingID { get; set; }
        public int? FloorID { get; set; }
        public string Room { get; set; }
        public int? ItemStateID { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public int? BruttoPrice { get; set; }
        public string Comment { get; set; }
        public DateTime? DateOfScrap { get; set; }

        public ItemNatureDto ItemNature { get; set; }
        public ItemTypeDto ItemType { get; set; }
        public DepartmentDto Department { get; set; }
        public SectionDto Section { get; set; }
        public EmployeeDto Employee { get; set; }
        public BuildingDto Building { get; set; }
        public FloorDto Floor { get; set; }
        public ItemStateDto ItemState { get; set; }
    }
}
