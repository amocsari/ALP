using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Operation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperationID { get; set; }
        [Required]
        [ForeignKey("OperationType")]
        public int OperationTypeID { get; set; }
        [Required]
        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public int? PayLoadID { get; set; }
        [Required]
        public bool Priority { get; set; }

        public virtual OperationType OperationType { get; set; }
        public virtual Item Item { get; set; }
    }
}
