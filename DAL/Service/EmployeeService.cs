using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Model.Dto;
using DAL.Context;
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

        public async Task<List<EmployeeDto>> FilterEmployees(EmployeeFilterInfo info)
        {
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

                return employees.Select(employee => employee.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: loggolás
                return null;
            }
        }

        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            try
            {
                var employees = await _context.Employee.AsNoTracking()
                    .Include(employee => employee.Department)
                    .Include(employee => employee.Section)
                    .ToListAsync();

                return employees.Select(employee => employee.EntityToDto()).ToList();
            }
            catch (Exception)
            {
                //TODO: loggolás
                return null;
            }
        }
    }
}
