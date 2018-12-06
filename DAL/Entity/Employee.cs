using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required]
        public string EmployeeName { get; set; }
        
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        [ForeignKey("Section")]
        public int? SectionId { get; set; }

        public string EmailAddress { get; set; }

        public DateTime? RetirementDate { get; set; }


        public virtual Department Department { get; set; }
        public virtual Section Section { get; set; }
    }
}
