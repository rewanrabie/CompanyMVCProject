using MVC_3_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_3_BLL.Interfaces
{
    public interface IDepartmentRepository :IGenaricRepository<Department>
    {
       /* IEnumerable<Department> GetAll();
        Department Get(int Id);
        int Add(Department Entity);
        int Update(Department Entity);
        int Delete(Department Entity);*/
    }
}
