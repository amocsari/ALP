using System.Text;
using Model.Enum;

namespace Common.Model.Dto
{
    public class OperationDto
    {
        public int OperationId { get; set; }
        public OperationType OperationType { get; set; }
        public int ItemId { get; set; }
        public int? PayLoadId { get; set; }
        public bool Priority { get; set; }

        public virtual ItemDto Item { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{{ Id = {OperationId}");
            sb.Append($", OperationType = {OperationType}");
            sb.Append($", ItemId = {ItemId}");
            sb.Append($", Item = {Item.ToString()}");
            sb.Append($", PayLoadId = {PayLoadId}");
            sb.Append($", Priority = {Priority}");
            sb.Append(" }");
            return sb.ToString();
        }
    }
}
