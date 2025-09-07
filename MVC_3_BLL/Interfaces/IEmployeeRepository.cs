using MVC_3_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_3_BLL.Interfaces
{
    public interface IEmployeeRepository :IGenaricRepository<Employee>
    {
       Task<List<Employee>> GetByNameAsync(string Name);

       /*IEnumerable<Employee> GetAll();
        Employee Get(int Id);
        int Add(Employee Entity);
        int Update(Employee Entity);
        int Delete(Employee Entity);*/
    }
}
