using Microsoft.EntityFrameworkCore;
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
    public class EmployeeRepository :GenaricRepository<Employee> ,IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) : base(context) //Ask CLR Create Object Form AppDbContext
        {
            _context = context;
        }

        public async Task<List<Employee>> GetByNameAsync(string name)
        {
           return await _context.Employees.Include(E => E.WorkFor).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            
        }
    }
}
