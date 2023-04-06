using Employee;
using EmployeeBusinessInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Authorization.Controllers
{
    [Authorize(Roles = "Owner,Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IEmployeeBusiness _employeeBusiness;
        public ManagerController(IEmployeeBusiness employeeBusiness)
        {
            _employeeBusiness = employeeBusiness;
        }

        [HttpGet("GetAll")]
        public List<Employees> GetAll()
        {
            return _employeeBusiness.GetAll();
        }

        [HttpPost("Create")]
        public Employees Create(Employees employees)
        {
            return _employeeBusiness.Create(employees);
        }

        [HttpPut("Update")]
        public List<Employees> Update(int Id, Employees employees)
        {
            return _employeeBusiness.Update(Id, employees);
        }

        [HttpGet("GetById")]
        public List<Employees> GetById(int Id)
        {
            return _employeeBusiness.GetById(Id);
        }
    }
}
