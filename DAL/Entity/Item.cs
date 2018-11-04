using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemID { get; set; }

        [Index(IsUnique = true)]
        public string InventoryNumber { get; set; }

        [Index(IsUnique = true)]
        public string OldInventoryNumber { get; set; }

        [Index(IsUnique = true)]
        public string SerialNumber { get; set; }

        [Index(IsUnique = true)]
        public string AccreditationNumber { get; set; }

        [Index(IsUnique = true)]
        public int? YellowNumber { get; set; }

        [Required]
        public string ItemName { get; set; }

        public string Manufacturer { get; set; }

        public string ModelType { get; set; }

        [Required]
        [ForeignKey("ItemNature")]
        public int? ItemNatureID { get; set; }

        [Required]
        [ForeignKey("ItemType")]
        public int? ItemTypeID { get; set; }

        //TODO: int?
        public DateTime? ProductionYear { get; set; }

        [ForeignKey("Department")]
        public int? DepartementID { get; set; }

        [ForeignKey("Section")]
        public int? SectionID { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeID { get; set; }

        [ForeignKey("Building")]
        public int? BuildingID { get; set; }

        [ForeignKey("Floor")]
        public int? FloorID { get; set; }

        public int? Room { get; set; }

        [ForeignKey("ItemState")]
        public int? ItemStateID { get; set; }

        public DateTime? DateOfCreation { get; set; }

        public int? BruttoPrice { get; set; }

        public string Comment { get; set; }


        public virtual ItemNature ItemNature { get; set; }
        public virtual ItemType ItemType { get; set; }
        public virtual Department Department { get; set; }
        public virtual Section Section { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Building Building { get; set; }
        public virtual Floor Floor { get; set; }
        public virtual ItemState ItemState { get; set; }
    }
}
