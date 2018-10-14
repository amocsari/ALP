using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Building
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuildingID { get; set; }
        [Required]
        public string BuildingName { get; set; }
        [Required]
        [ForeignKey("Location")]
        public int LocationID { get; set; }

        public virtual Location Location { get; set; }
    }
}
