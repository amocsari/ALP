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
                DepartmentId = dto.DepartmentID,
                SectionId = dto.SectionID,
                EmailAddress = dto.EmailAddress,
                EmployeeId = dto.Id,
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
                DepartmentID = entity.DepartmentId,
                SectionID = entity.SectionId,
                EmailAddress = entity.EmailAddress,
                Id = entity.EmployeeId,
                Name = entity.EmployeeName,
                RetirementDate = entity.RetirementDate
            };
        }

        public static void UpdateEntityByDto(this Employee entity, EmployeeDto dto)
        {
            if (dto == null || entity == null)
            {
                return;
            }

            entity.EmployeeName = dto.Name;
            entity.DepartmentId = dto.DepartmentID;
            entity.Department.UpdateEntityByDto(dto.Department);
            entity.SectionId = dto.SectionID;
            entity.Section.UpdateEntityByDto(dto.Section);
            entity.EmailAddress = dto.EmailAddress;
            entity.RetirementDate = dto.RetirementDate;
        }
    }
}
