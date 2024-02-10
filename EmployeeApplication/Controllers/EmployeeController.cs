using EmployeeApplication.Dtos;
using EmployeeApplication.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeDataStore store;

        public EmployeeController(EmployeeDataStore store)
        {
            this.store = store;
        }
        [HttpPost("login")]

        public IActionResult Login([FromBody] LoginDto loginDto)
        {

            var obj = store.Login(loginDto.Name, loginDto.Password);

            if (obj != null)
            {
                var claims = new List<Claim>
                 {
                  new Claim(ClaimTypes.Name, obj.EmployeeId.ToString()),

                   new Claim(ClaimTypes.Role, obj.Role.ToString()),

                 };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Ok(obj);
            }
            return Unauthorized();
        }
        [HttpGet]
        public IActionResult GetDetails()
        {
            var idClaim = User.FindFirst(ClaimTypes.Name)?.Value;

            if (int.TryParse(idClaim, out int userId))
            {
                var employee = store.GetEmployee(userId);
                return Ok(employee);
            }
            return BadRequest();
        }
      
        [HttpPost("/leave")]
        public IActionResult ApplyLeave(int empid, int mngId)
        {
            var req = store.ApplyLeave(empid, mngId);
            return Ok(req);

        }
       
    }
}
