using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model.Dto;

namespace DAL.Service
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllDepartments();
        Task<List<DepartmentDto>> GetAvailableDepartments();
        Task<DepartmentDto> GetDepartmentById(int departmentId);
        Task<DepartmentDto> InsertNewDepartment(DepartmentDto department);
        Task DeleteDepartmentById(int departmentId);
        Task ToggleDepartmentLockStateById(int departmentId);
        Task<DepartmentDto> UpdateDepartment(DepartmentDto dto);
    }
}
