using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Operation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperationId { get; set; }

        [Required]
        [ForeignKey("OperationType")]
        public int OperationTypeId { get; set; }

        [Required]
        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public int? PayLoadId { get; set; }

        [Required]
        public bool Priority { get; set; }


        public virtual OperationType OperationType { get; set; }
        public virtual Item Item { get; set; }
    }
}
