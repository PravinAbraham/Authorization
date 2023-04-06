using Employee;
using EmployeeBusinessInterface;
using EmployeeRepositoryInterface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBusiness
{
    public class EmployeesBusiness : IEmployeeBusiness
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesBusiness(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public List<Employees> GetAll()
        {
            return _employeeRepository.GetAll();
        }
        public string Delete(int Id)
        {
            return _employeeRepository.Delete(Id);
        }
        public List<Employees> GetById(int Id)
        {
            return _employeeRepository.GetById(Id);
        }
        public List<Employees> Update(int Id, Employees employees)
        {
            return _employeeRepository.Update(Id, employees);
        }
        public Employees Create(Employees employees)
        {
            return _employeeRepository.Create(employees);
        }
    }
}