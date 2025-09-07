using MVC_3_BLL.Interfaces;
using MVC_3_DAL.Data.Contexts;
using MVC_3_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_3_BLL.Repositories
{
    //CLR
    public class DepartmentRepository : GenaricRepository<Department>,IDepartmentRepository
    {   
        public DepartmentRepository(AppDbContext context) : base(context) //Ask CLR Create Object Form AppDbContext
        {
          
        }
      

        

    }
}
