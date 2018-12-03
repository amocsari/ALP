using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Model.Dto;
using DAL.Context;
using DAL.Entity;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Model.Model;

namespace DAL.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAlpContext _context;

        public EmployeeService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<AlpApiResponse> AddNewEmployee(EmployeeDto dto)
        {
            var response = new AlpApiResponse();
            try
            {
                dto.Validate();

                var entity = dto.DtoToEntity();
                entity.Department = null;
                entity.Section = null;
                await _context.Employee.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //TODO: logging
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
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<AlpApiResponse<List<EmployeeDto>>> FilterEmployees(EmployeeFilterInfo info)
        {
            var response = new AlpApiResponse<List<EmployeeDto>>();
            try
            {
                var nameIsNull = string.IsNullOrEmpty(info.Name);
                var employees = await _context.Employee.AsNoTracking()
                    .Include(employee => employee.Department)
                    .Include(employee => employee.Section)
                    .Where(employee => (nameIsNull || employee.EmployeeName.Contains(info.Name))
                                       && (!info.DepartmentId.HasValue || employee.DepartmentId == info.DepartmentId)
                                       && (!info.SectionId.HasValue || employee.SectionId == info.SectionId))
                    .ToListAsync();

                response.Value = employees.Select(employee => employee.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
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
                var employees = await _context.Employee.AsNoTracking()
                    .Include(employee => employee.Department)
                    .Include(employee => employee.Section)
                    .ToListAsync();

                response.Value = employees.Select(employee => employee.EntityToDto()).ToList();
            }
            catch (Exception e)
            {
                //TODO: logging
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
