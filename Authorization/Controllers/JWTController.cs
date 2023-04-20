using Authorization.Handler;
using Dapper;
using Employee;
using Employee.Details;
using EmployeeBusinessInterface;
using EmployeeManagement.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        private readonly IEmployeeBusiness _employeeBusiness;
        private readonly JWTSettings _jwtSettings;
        private readonly EmployeeDbContext _employeeDbContext;
        public JWTController (IEmployeeBusiness employeeBusiness, IOptions<JWTSettings> options, EmployeeDbContext employeeDbContext)
        {
            _employeeBusiness = employeeBusiness;
            _jwtSettings = options.Value;
            _employeeDbContext = employeeDbContext;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authenticate([FromBody] Jwt jwt)
        {
           
            var query = "SELECT * FROM EmployeeDetails WHERE Email = '" + jwt.Email + "' AND FirstName = '" + jwt.password + "'";
            var connection = _employeeDbContext.CreateConnection();

            connection.Open();

            Employees employees = connection.QueryFirstOrDefault<Employees>(query);
            var _user =  employees.Email == jwt.Email && employees.FirstName == jwt.password;
            if (_user == null) 

                return Unauthorized();

            var tokenhandler = new JwtSecurityTokenHandler(); 
            var tokenkey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name,employees.LastName)
                    }
                ),
                Expires = DateTime.Now.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey),SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            return Ok(finaltoken);
        }
    }
}
