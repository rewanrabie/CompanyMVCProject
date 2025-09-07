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
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        public readonly AppDbContext _context;  //NULL

        public GenaricRepository(AppDbContext context) //Ask CLR Create Object Form AppDbContext
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E => E.WorkFor).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetAsync(int Id)
        {        
            return await _context.Set<T>().FindAsync(Id);
        }
        public async Task AddAsync(T Entity)
        {
           await _context.AddAsync(Entity);
            
        }
        public void Update(T Entity)
        {
           _context.Update(Entity);
          
        }
        public void Delete(T Entity)
        {
            _context.Remove(Entity);
           
        }
    }
}
