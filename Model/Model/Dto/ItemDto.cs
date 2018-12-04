using System;
using System.Text;

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
        public int? ItemNatureID { get; set; }
        public int? ItemTypeID { get; set; }
        public DateTime? ProductionYear { get; set; }

        public int? DepartmentID { get; set; }
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

        public void Validate()
        {
            if(string.IsNullOrEmpty(InventoryNumber) && string.IsNullOrEmpty(OldInventoryNumber) && string.IsNullOrEmpty(SerialNumber) && !YellowNumber.HasValue && string.IsNullOrEmpty(AccreditationNumber))
            {
                throw new Exception("Legalább egy azonosítót kötelezően meg kell adni!");
            }

            if (string.IsNullOrEmpty(ItemName))
            {
                throw new Exception("A leltárelem nevét kötelező megadni!");
            }

            if(ItemState == null)
            {
                throw new Exception("A leltárelem állapotát kötelező megadni!");
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{{ ItemId = {ItemID}");
            sb.Append($", InventoryNumber = {InventoryNumber}");
            sb.Append($", OldInventoryNumber = {OldInventoryNumber}");
            sb.Append($", SerialNumber = {SerialNumber}");
            sb.Append($", AccreditationNumber = {AccreditationNumber}");
            sb.Append($", YellowNumber = {YellowNumber}");
            sb.Append($", ItemName = {ItemName}");
            sb.Append($", Manufacturer = {Manufacturer}");
            sb.Append($", ModelType = {ModelType}");
            sb.Append($", ItemNatureID = {ItemNatureID}");
            sb.Append($", ItemNature = {ItemNature.ToString()}");
            sb.Append($", ItemTypeID = {ItemTypeID}");
            sb.Append($", ItemType = {ItemType.ToString()}");
            sb.Append($", ProductionYear = {ProductionYear?.ToShortDateString()}");
            sb.Append($", DepartmentID = {DepartmentID}");
            sb.Append($", Department = {Department.ToString()}");
            sb.Append($", SectionID = {SectionID}");
            sb.Append($", Section = {Section.ToString()}");
            sb.Append($", EmployeeID = {EmployeeID}");
            sb.Append($", Employee = {Employee.ToString()}");
            sb.Append($", BuildingID = {BuildingID}");
            sb.Append($", Building = {Building.ToString()}");
            sb.Append($", FloorID = {FloorID}");
            sb.Append($", Floor = {Floor.ToString()}");
            sb.Append($", Room = {Room}");
            sb.Append($", ItemStateID = {ItemStateID}");
            sb.Append($", ItemState = {ItemState.ToString()}");
            sb.Append($", DateOfCreation = {DateOfCreation?.ToShortDateString()}");
            sb.Append($", BruttoPrice = {BruttoPrice}");
            sb.Append($", DateOfScrap = {DateOfScrap?.ToShortDateString()}");
            sb.Append($", Comment = {Comment}");
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
