using Common.Model.Dto;
using DAL.Entity;

namespace DAL.Extensions
{
    public static partial class Extensions
    {
        public static Department DtoToEntity(this DepartmentDto dto)
        {
            return new Department
            {
                Employee = dto.Employee?.DtoToEntity(),
                DepartmentId = dto.Id,
                Locked = dto.Locked,
                EmployeeId = dto.EmployeeId,
                DepartmentName = dto.Name
            };
        }

        public static DepartmentDto EntityToDto(this Department entity)
        {
            return new DepartmentDto
            {
                Employee = entity.Employee?.EntityToDto(),
                Id = entity.DepartmentId,
                Locked = entity.Locked,
                EmployeeId = entity.EmployeeId,
                Name = entity.DepartmentName
            };
        }

        public static void UpdateEntityByDto(this Department entity, DepartmentDto dto)
        {
            if (dto == null || entity == null)
            {
                return;
            }

            entity.DepartmentName = dto.Name;
            entity.EmployeeId = dto.EmployeeId;
            entity.Employee.UpdateEntityByDto(dto.Employee);
            entity.Locked = dto.Locked;
        }
    }
}
