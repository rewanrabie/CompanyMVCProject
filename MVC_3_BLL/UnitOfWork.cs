using MVC_3_BLL.Interfaces;
using MVC_3_BLL.Repositories;
using MVC_3_DAL.Data.Contexts;

namespace MVC_3_BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public UnitOfWork(AppDbContext context)
        {
            _employeeRepository = new EmployeeRepository (context);
            _departmentRepository = new DepartmentRepository(context);
            _context = context;
        }
        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public IEmployeeRepository EmployeeRepository => _employeeRepository;
        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
