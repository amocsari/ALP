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
using Microsoft.Extensions.Logging;

namespace API.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IAlpContext context, ILogger<DepartmentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AlpApiResponse> DeleteDepartmentById(int departmentId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(DeleteDepartmentById),
                    departmentId
                }.ToString());

                var entity = await _context.Department.FirstOrDefaultAsync(department => department.DepartmentId == departmentId);
                _context.Department.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
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
                _logger.LogDebug(new
                {
                    action = nameof(GetAllDepartments)
                }.ToString());

                var departments = await _context.Department.AsNoTracking().Include(department => department.Employee).ToListAsync();
                response.Value = departments.Select(department => department.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
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
                _logger.LogDebug(new
                {
                    action = nameof(GetAvailableDepartments)
                }.ToString());

                var departments = await _context.Department.AsNoTracking().Include(department => department.Employee).Where(Department => !Department.Locked).ToListAsync();
                response.Value = departments.Select(Department => Department.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
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
                _logger.LogDebug(new
                {
                    action = nameof(GetDepartmentById),
                    departmentId
                }.ToString());

                var entity = await _context.Department.FirstOrDefaultAsync(department => department.DepartmentId == departmentId);
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<AlpApiResponse<DepartmentDto>> InsertNewDepartment(DepartmentDto dto)
        {
            var response = new AlpApiResponse<DepartmentDto>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(InsertNewDepartment),
                    dto = dto.ToString()
                }.ToString());

                dto.Validate();

                var entity = dto.DtoToEntity();
                entity.Employee = null;
                await _context.Department.AddAsync(entity);
                await _context.SaveChangesAsync();
                response.Value = entity.EntityToDto();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
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
                _logger.LogDebug(new
                {
                    action = nameof(UpdateDepartment),
                    dto = dto.ToString()
                }.ToString());

                dto.Validate();

                var updatedEntity = await _context.Department.Include(department => department.Employee).FirstOrDefaultAsync(Department => Department.DepartmentId == dto.Id);
                updatedEntity.UpdateEntityByDto(dto);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
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
                _logger.LogDebug(new
                {
                    action = nameof(ToggleDepartmentLockStateById),
                    departmentId
                }.ToString());

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
                _logger.LogError(new
                {
                    exception = e,
                    message = e.Message,
                    innerException = e,
                    innerExceptionMessage = e.InnerException?.Message
                }.ToString());
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}