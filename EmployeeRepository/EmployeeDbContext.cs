using Employee;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EmployeeManagement.Data
{
    public class EmployeeDbContext
    {
        private readonly IConfiguration _configuration;
        public EmployeeDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}
