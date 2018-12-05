using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Model.Dto;
using DAL.Context;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Model;

namespace API.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAlpContext _context;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IAlpContext context, ILogger<EmployeeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AlpApiResponse> AddNewEmployee(EmployeeDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(AddNewEmployee),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                var entity = dto.DtoToEntity();
                entity.Department = null;
                entity.Section = null;
                await _context.Employee.AddAsync(entity);
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
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<EmployeeDto>>> GetByName(string name)
        {
            var response = new AlpApiResponse<List<EmployeeDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetByName),
                    name
                }.ToString());

                if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            name = name.ToLower();
            var entities = await _context.Employee.Where(employee => employee.EmployeeName.ToLower().Equals(name)).ToListAsync();
            response.Value = entities.Select(entity => entity.EntityToDto()).ToList();
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
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<EmployeeDto>>> FilterEmployees(EmployeeFilterInfo filterInfo)
        {
            var response = new AlpApiResponse<List<EmployeeDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(FilterEmployees),
                    info = filterInfo?.ToString()
                }.ToString());

                var nameIsNull = string.IsNullOrEmpty(filterInfo.Name);
                var employees = await _context.Employee.AsNoTracking()
                    .Include(employee => employee.Department)
                    .Include(employee => employee.Section)
                    .Where(employee => (nameIsNull || employee.EmployeeName.Contains(filterInfo.Name))
                                       && (!filterInfo.DepartmentId.HasValue || employee.DepartmentId == filterInfo.DepartmentId)
                                       && (!filterInfo.SectionId.HasValue || employee.SectionId == filterInfo.SectionId))
                    .ToListAsync();

                response.Value = employees.Select(employee => employee.EntityToDto()).ToList();
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
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<EmployeeDto>>> GetAllEmployees()
        {
            var response = new AlpApiResponse<List<EmployeeDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAllEmployees)
                }.ToString());

                var employees = await _context.Employee.AsNoTracking()
                    .Include(employee => employee.Department)
                    .Include(employee => employee.Section)
                    .ToListAsync();

                response.Value = employees.Select(employee => employee.EntityToDto()).ToList();
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
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<EmployeeDto>>> GetAvailableEmployees()
        {
            var response = new AlpApiResponse<List<EmployeeDto>>();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(GetAllEmployees)
                }.ToString());

                var employees = await _context.Employee.AsNoTracking()
                    .Include(employee => employee.Department)
                    .Include(employee => employee.Section)
                    .Where(employee => employee.RetirementDate == null)
                    .ToListAsync();

                response.Value = employees.Select(employee => employee.EntityToDto()).ToList();
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
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
