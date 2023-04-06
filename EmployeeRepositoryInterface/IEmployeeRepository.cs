using Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRepositoryInterface
{
    public interface IEmployeeRepository
    {
        public List<Employees> GetAll();
        public string Delete(int Id);
        public List<Employees> GetById(int Id);
        public List<Employees> Update(int Id, Employees employees);
        public Employees Create(Employees employees);
    }
}
