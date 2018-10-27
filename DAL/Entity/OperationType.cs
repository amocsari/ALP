using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class OperationType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperationTypeID { get; set; }
        [Required]
        public string OperationTypeName { get; set; }
        [Required]
        public bool Locked { get; set; }
    }
}
