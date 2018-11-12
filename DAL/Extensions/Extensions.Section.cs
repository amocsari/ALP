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
        public static Section DtoToEntity(this SectionDto dto)
        {
            return new Section
            {
                Department = dto.Department?.DtoToEntity(),
                Floor = dto.Floor?.DtoToEntity(),
                Locked = dto.Locked,
                DepartmentId = dto.DepartmentID,
                FloorId = dto.FloorID,
                SectionId = dto.ID,
                SectionName = dto.Name
            };
        }

        public static SectionDto EntityToDto(this Section entity)
        {
            return new SectionDto
            {
                Department = entity.Department?.EntityToDto(),
                Floor = entity.Floor?.EntityToDto(),
                Locked = entity.Locked,
                DepartmentID = entity.DepartmentId,
                FloorID = entity.FloorId,
                ID = entity.SectionId,
                Name = entity.SectionName
            };
        }
    }
}
