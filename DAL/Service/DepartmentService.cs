using DAL.Entity;
using DAL.Context;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Service
{
    public class DepartmentService : IDepartmentService
    {
        private IAlpContext _context;

        public DepartmentService(IAlpContext context)
        {
            _context = context;
        }

        public async Task DeleteDepartmentById(int departmentId)
        {
            try
            {
                var entity = await _context.Department.FirstOrDefaultAsync(department => department.DepartmentId == departmentId);
                _context.Department.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }

        public async Task<List<DepartmentDto>> GetAllDepartments()
        {
            try
            {
                var departments = await _context.Department.AsNoTracking().Include(department => department.Employee).ToListAsync();
                return departments.Select(department => department.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<List<DepartmentDto>> GetAvailableDepartments()
        {
            try
            {
                var departments = await _context.Department.AsNoTracking().Include(department => department.Employee).Where(Department => !Department.Locked).ToListAsync();
                return departments.Select(Department => Department.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<DepartmentDto> GetDepartmentById(int departmentId)
        {
            try
            {
                var entity = await _context.Department.FirstOrDefaultAsync(department => department.DepartmentId == departmentId);
                return entity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<DepartmentDto> InsertNewDepartment(DepartmentDto department)
        {
            try
            {
                var entity = department.DtoToEntity();
                entity.Employee = null;
                await _context.Department.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task<DepartmentDto> UpdateDepartment(DepartmentDto dto)
        {
            try
            {
                var updatedEntity = await _context.Department.Include(department => department.Employee).FirstOrDefaultAsync(Department => Department.DepartmentId == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
                return updatedEntity.EntityToDto();
            }
            catch (Exception)
            {
                //TODO: logging
                return null;
            }
        }

        public async Task ToggleDepartmentLockStateById(int departmentId)
        {
            try
            {
                var department = await GetDepartmentById(departmentId);
                department.Locked = !department.Locked;
                await UpdateDepartment(department);
            }
            catch (Exception)
            {
                //TODO: logging
            }
        }
    }
}
