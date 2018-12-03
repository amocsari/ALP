using DAL.Entity;
using DAL.Context;
using Common.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using Model.Model;

namespace DAL.Service
{
    public class DepartmentService : IDepartmentService
    {
        private IAlpContext _context;

        public DepartmentService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<AlpApiResponse> DeleteDepartmentById(int departmentId)
        {
            var response = new AlpApiResponse();
            try
            {
                var entity = await _context.Department.FirstOrDefaultAsync(department => department.DepartmentId == departmentId);
                _context.Department.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<DepartmentDto>>> GetAllDepartments()
        {
            var response = new AlpApiResponse<List<DepartmentDto>>();
            try
            {
                var departments = await _context.Department.AsNoTracking().Include(department => department.Employee).ToListAsync();
                response.Value = departments.Select(department => department.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<DepartmentDto>>> GetAvailableDepartments()
        {
            var response = new AlpApiResponse<List<DepartmentDto>>();
            try
            {
                var departments = await _context.Department.AsNoTracking().Include(department => department.Employee).Where(Department => !Department.Locked).ToListAsync();
                response.Value = departments.Select(Department => Department.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<AlpApiResponse<DepartmentDto>> GetDepartmentById(int departmentId)
        {
            var response = new AlpApiResponse<DepartmentDto>();
            try
            {
                var entity = await _context.Department.FirstOrDefaultAsync(department => department.DepartmentId == departmentId);
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<AlpApiResponse<DepartmentDto>> InsertNewDepartment(DepartmentDto department)
        {
            var response = new AlpApiResponse<DepartmentDto>();
            try
            {
                var entity = department.DtoToEntity();
                entity.Employee = null;
                await _context.Department.AddAsync(entity);
                await _context.SaveChangesAsync();
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<AlpApiResponse> UpdateDepartment(DepartmentDto dto)
        {
            var response = new AlpApiResponse();

            try
            {
                var updatedEntity = await _context.Department.Include(department => department.Employee).FirstOrDefaultAsync(Department => Department.DepartmentId == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<AlpApiResponse> ToggleDepartmentLockStateById(int departmentId)
        {
            var response = new AlpApiResponse();

            try
            {
                var getByIdResponse = await GetDepartmentById(departmentId);
                if (!getByIdResponse.Success)
                {
                    response.Success = getByIdResponse.Success;
                    response.Message = getByIdResponse.Message;
                    return response;
                }

                var department = getByIdResponse.Value;
                department.Locked = !department.Locked;
                var updateResponse = await UpdateDepartment(department);

                if (!updateResponse.Success)
                {
                    response.Success = updateResponse.Success;
                    response.Message = updateResponse.Message;
                    return response;
                }
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}