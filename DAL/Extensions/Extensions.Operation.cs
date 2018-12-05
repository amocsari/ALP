using Common.Model.Dto;
using DAL.Entity;
using Model.Enum;

namespace DAL.Extensions
{
    public partial class Extensions
    {
        public static OperationDto EntityToDto(this Operation entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new OperationDto
            {
                Item = entity.Item.EntityToDto(),
                OperationType = (OperationType)entity.OperationType,
                ItemId = entity.ItemId,
                OperationId = entity.OperationId,
                Priority = entity.Priority,
                PayLoadId = entity.PayLoadId
            };
        }

        public static Operation DtoToEntity(this OperationDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new Operation
            {
                Item = dto.Item?.DtoToEntity(),
                OperationType = (int)dto.OperationType,
                ItemId = dto.ItemId,
                OperationId = dto.OperationId,
                Priority = dto.Priority,
                PayLoadId = dto.PayLoadId
            };
        }
    }
}
