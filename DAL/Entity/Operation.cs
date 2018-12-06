using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Operation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperationId { get; set; }

        public int OperationType { get; set; }

        [Required]
        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public int? PayLoadId { get; set; }

        [Required]
        public bool Priority { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; }

        public DateTime? DateOfCompletion { get; set; }


        public virtual Item Item { get; set; }
    }
}
