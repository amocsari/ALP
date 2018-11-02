using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class ItemNature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemNatureID { get; set; }
        [Required]
        public string ItemNatureName { get; set; }
        [Required]
        public bool Locked { get; set; }
    }
}
