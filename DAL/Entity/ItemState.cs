using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class ItemState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemStateId { get; set; }

        [Required]
        public string ItemStateName { get; set; }

        [Required]
        public bool Locked { get; set; }
    }
}
