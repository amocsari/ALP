using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model.Dto;
using Model.Model;

namespace DAL.Service
{
    public interface IDepartmentService
    {
        Task<AlpApiResponse<List<DepartmentDto>>> GetAllDepartments();
        Task<AlpApiResponse<List<DepartmentDto>>> GetAvailableDepartments();
        Task<AlpApiResponse<DepartmentDto>> GetDepartmentById(int departmentId);
        Task<AlpApiResponse<DepartmentDto>> InsertNewDepartment(DepartmentDto department);
        Task<AlpApiResponse> DeleteDepartmentById(int departmentId);
        Task<AlpApiResponse> ToggleDepartmentLockStateById(int departmentId);
        Task<AlpApiResponse> UpdateDepartment(DepartmentDto dto);
    }
}
