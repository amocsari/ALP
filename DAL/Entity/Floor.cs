using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Floor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FloorId { get; set; }

        [Required]
        public string FloorName { get; set; }

        [Required]
        [ForeignKey("Building")]
        public int BuildingId { get; set; }

        [Required]
        public bool Locked { get; set; }


        public virtual Building Building { get; set; }
    }
}
