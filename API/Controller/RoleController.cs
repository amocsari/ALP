using System.Collections.Generic;
using DAL.Entity;
using DAL.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public List<Role> GetAllRoles()
        {
            return _roleService.GetAll();
        }

        [HttpGet]
        public Role GetRoleById(int roleId)
        {
            return _roleService.GetSingle(b => b.RoleID == roleId);
        }

        [HttpPost]
        public void AddNewRole([FromBody] Role role)
        {
            _roleService.InsertNew(role);
        }

        [HttpDelete]
        public void DeleteRoleById(int roleId)
        {
            _roleService.Remove(b => b.RoleID == roleId);
        }
    }
}