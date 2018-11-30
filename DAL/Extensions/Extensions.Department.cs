using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
