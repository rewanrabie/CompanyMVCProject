using AutoMapper;
using MVC_3.ViewModels;
using MVC_3.ViewModels.Employees;
using MVC_3_DAL.Models;

namespace MVC_3.Mapping.Employees
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>();
            CreateMap<Employee, EmployeeViewModel>();
        }
    }
}
