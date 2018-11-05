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
        public static Employee DtoToEntity(this EmployeeDto dto)
        {
            return new Employee
            {
                Department = dto.Department?.DtoToEntity(),
                Section = dto.Section?.DtoToEntity(),
                DepartmentID = dto.DepartmentID,
                SectionID = dto.SectionID,
                EmailAddress = dto.EmailAddress,
                EmployeeID = dto.ID,
                EmployeeName = dto.Name,
                RetirementDate = dto.RetirementDate
            };
        }

        public static EmployeeDto EntityToDto(this Employee entity)
        {
            return new EmployeeDto
            {
                Department = entity.Department?.EntityToDto(),
                Section = entity.Section?.EntityToDto(),
                DepartmentID = entity.DepartmentID,
                SectionID = entity.SectionID,
                EmailAddress = entity.EmailAddress,
                ID = entity.EmployeeID,
                Name = entity.EmployeeName,
                RetirementDate = entity.RetirementDate
            };
        }
    }
}
