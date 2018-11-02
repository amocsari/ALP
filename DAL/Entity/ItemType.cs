using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class ItemType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemTypeID { get; set; }
        [Required]
        public string ItemTypeName { get; set; }
        [Required]
        [ForeignKey("ItemNature")]
        public int ItemNatureID { get; set; }
        [Required]
        public bool Locked { get; set; }
        
        public virtual ItemNature ItemNature { get; set; }
    }
}
