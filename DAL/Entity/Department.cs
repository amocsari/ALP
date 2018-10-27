using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        [Required]
        public bool Locked { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
