using Dapper;
using Employee;
using EmployeeManagement.Data;
using EmployeeRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRepository
{
    public class EmployeessRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeessRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public List<Employees> GetAll()
        {
            string sql = "SELECT * FROM EmployeeDetails";
            using (var connection = _employeeDbContext.CreateConnection())
            {
                connection.Open();
                return connection.Query<Employees>(sql).ToList();
            }
        }
        public string Delete(int Id)
        {
            string sql = "delete from EmployeeDetails where id = " + Id;
            using (var connection = _employeeDbContext.CreateConnection())
            {
                connection.Open();
                connection.Execute(sql);
                return "Successfully Deleted";
            }
        }
        public List<Employees> GetById(int Id)
        {
            string sql = "select * from EmployeeDetails where Id = " + Id;
            using (var connection = _employeeDbContext.CreateConnection())
            {
                connection.Open();
                connection.Execute(sql);
                return connection.Query<Employees>("select * from EmployeeDetails Where Id = " + Id).ToList();
            }
        }

        public List<Employees> Update(int Id, Employees employees)
        {
            string sql = "Update EmployeeDetails Set "
                         + "FirstName = '"
                         + employees.FirstName
                         + "',Lastname = '"
                         + employees.LastName
                         + "',Qualification = '"
                         + employees.Qualification
                         + "',Email = '"
                         + employees.Email
                         + "', PhoneNumber = '"
                         + employees.PhoneNumber
                         + "',Salary = '"
                         + employees.Salary
                         + "',Role = '"
                         + employees.Role
                         + "' where Id = "
                         + Id
                         + "";
            using (var connection = _employeeDbContext.CreateConnection())
            {
                connection.Open();
                connection.Execute(sql);
                return connection.Query<Employees>("select * from EmployeeDetails Where Id = " + Id).ToList();
            }
        }
        public Employees Create(Employees employees)
        {
            string sql = "INSERT INTO EmployeeDetails VALUES('" + employees.FirstName + "','" + employees.LastName + "','" + employees.Qualification + "','" + employees.Email + "','" + employees.PhoneNumber + "','" + employees.Salary + "','" + employees.Role + "')";
            using (var connection = _employeeDbContext.CreateConnection())
            {
                connection.Open();
                connection.Execute(sql);
                return employees;
            }
        }
    }
}