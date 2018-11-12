using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionId { get; set; }

        [Required]
        public string SectionName { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [Required]
        [ForeignKey("Floor")]
        public int FloorId { get; set; }

        [Required]
        public bool Locked { get; set; }


        public virtual Floor Floor { get; set; }
        public virtual Department Department { get; set; }
    }
}
