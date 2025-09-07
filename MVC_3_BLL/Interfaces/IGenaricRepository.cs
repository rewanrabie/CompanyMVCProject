using MVC_3_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_3_BLL.Interfaces
{
    public interface IGenaricRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int Id);
       Task AddAsync(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
