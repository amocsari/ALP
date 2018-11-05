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
                DepartmentID = dto.ID,
                Locked = dto.Locked,
                EmployeeID = dto.EmployeeID,
                DepartmentName = dto.Name
            };
        }

        public static DepartmentDto EntityToDto(this Department entity)
        {
            return new DepartmentDto
            {
                Employee = entity.Employee?.EntityToDto(),
                ID = entity.DepartmentID,
                Locked = entity.Locked,
                EmployeeID = entity.EmployeeID,
                Name = entity.DepartmentName
            };
        }
    }
}
