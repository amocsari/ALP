using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

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
        [ForeignKey(nameof(ItemNature))]
        public int? ItemNatureId { get; set; }

        [Required]
        [ForeignKey(nameof(ItemType))]
        public int? ItemTypeId { get; set; }

        //TODO: int?
        public DateTime? ProductionYear { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(Section))]
        public int? SectionId { get; set; }

        [ForeignKey(nameof(Employee))]
        public int? EmployeeId { get; set; }

        [ForeignKey(nameof(Building))]
        public int? BuildingId { get; set; }

        [ForeignKey(nameof(Floor))]
        public int? FloorId { get; set; }

        public string Room { get; set; }

        [ForeignKey(nameof(ItemState))]
        public int? ItemStateId { get; set; }

        public DateTime? DateOfCreation { get; set; }

        public int? BruttoPrice { get; set; }

        public string Comment { get; set; }

        public DateTime? DateOfScrap { get; set; }


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
