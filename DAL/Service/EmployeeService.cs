using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Model.Dto;
using DAL.Context;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Service
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IAlpContext _context;

        public EmployeeService(IAlpContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            var employees = await _context.Employee.AsNoTracking()
                .Include(employee => employee.Department)
                .Include(employee => employee.Section)
                .ToListAsync();

            return employees.Select(employee => employee.EntityToDto()).ToList();
        }
    }
}
