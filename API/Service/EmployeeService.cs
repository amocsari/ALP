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

        /// <summary>
        /// checks if the employee is in the table
        /// if yes, updates it
        /// if no adds it 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> AddOrEditEmployee(EmployeeDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(AddOrEditEmployee),
                    dto = dto?.ToString()
                }.ToString());

                dto.Validate();

                var oldEntity = await _context.Employee.FirstOrDefaultAsync(employee => employee.EmployeeId == dto.Id);
                if (oldEntity != null)
                {
                    oldEntity.UpdateEntityByDto(dto);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var entity = dto.DtoToEntity();
                    entity.Department = null;
                    entity.Section = null;
                    await _context.Employee.AddAsync(entity);
                    await _context.SaveChangesAsync();
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
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        /// <summary>
        /// gets an employee by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns the filtered employees depending onthe filterinfo
        /// </summary>
        /// <param name="filterInfo"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns all employees in the database
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// gets all non retured employees
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// sets the retirement date of an employee to today
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<AlpApiResponse> RetireEmployee(int employeeId)
        {
            var response = new AlpApiResponse();
            try
            {
                _logger.LogDebug(new
                {
                    action = nameof(RetireEmployee),
                    employeeId
                }.ToString());

                var entity = await _context.Employee.FirstOrDefaultAsync(employee => employee.EmployeeId == employeeId);

                if(entity == null)
                {
                    response.Success = false;
                    response.Message = "Nem található a munkavállaló az adatbázisban";
                    return response;
                }

                if(entity.RetirementDate != null)
                {
                    response.Success = false;
                    response.Message = "Ennek a munkavállalónak már megszűnt a munkaviszonya";
                    return response;
                }

                entity.RetirementDate = DateTime.Now;

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
    }
}
