using EmployeeApplication.Dtos;
using EmployeeApplication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : Controller
    {

        private readonly EmployeeDataStore store;

        public ManagerController(EmployeeDataStore store)
        {
            this.store = store;
        }
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            Employee employee = new Employee();
            employee.Name = employeeDto.Name;
            employee.Password = employeeDto.Password;
            employee.Role = Roles.Employee;
            var obj = store.AddEmployee(employee);
            if (obj != null)
            {
                return Ok(obj);
            }
            return BadRequest();
        }
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult GetAllEmployee()
        {
            var idClaim = User.FindFirst(ClaimTypes.Name)?.Value;

            if (int.TryParse(idClaim, out int userId))
            {
                var employees = store.GetAllEmployee(userId);
                return Ok(employees);
            }
            else
            {

                return BadRequest("Invalid user ID claim");
            }
        }

        [HttpPost("/leave/{leaveId}")]
        [Authorize(Roles = "Manager")]

        public IActionResult ApproveLeave(int leaveId)
        {
            var idClaim = User.FindFirst(ClaimTypes.Name)?.Value;

            if (int.TryParse(idClaim, out int userId))
            {
                var Employee = store.ApproveLeave(leaveId, userId);
                return Ok(Employee);
            }
            return BadRequest();
        }

    }
}
