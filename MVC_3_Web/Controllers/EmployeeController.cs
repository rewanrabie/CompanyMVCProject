using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_3.Helpers;
using MVC_3.ViewModels;
using MVC_3.ViewModels.Employees;
using MVC_3_BLL.Interfaces;
using MVC_3_DAL.Data.Contexts;
using MVC_3_DAL.Models;

namespace MVC_3.Controllers
{
   // [Authorize]

    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper,AppDbContext context) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Employee
       // [HttpGet("")] 1
       // [HttpGet("Index")] 2
        public async Task<IActionResult> Index(string? SearchInput)
        {
            /* var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
             var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
             return View(mappedEmployees);*/
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
            return View(employees);
        }

        // GET: Employee/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id ,string viewname = "Details")
        {
            /* if (id == null)
             {
                 return BadRequest();
             }

             var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
             if (employee == null)
             {
                 return NotFound();
             }
             var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
             return View(mappedEmployee);*/
            if (id is null) return BadRequest("Invalid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if (employee == null) return NotFound(new { statusCode = 404, messege = $"Employee With Id:{id} is Not Found" });

            return View(employee);
        }

        // GET: Employee/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            /* ViewBag.Departments*/
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
         [ValidateAntiForgeryToken]
          public async Task<IActionResult> Create(EmployeeViewModel model)
          {
            /*if (ModelState.IsValid)
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                await _unitOfWork.EmployeeRepository.AddAsync(mappedEmployee);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(employeeViewModel);*/
            model.DateOfCreation = DateTime.Now;

            if (ModelState.IsValid)
         {

            if (model.Image is not null)
             {
                 model.ImageName = DocumentSetting.UpLoad(model.Image, "image");
             }

                var employee = _mapper.Map<Employee>(model);

             await _unitOfWork.EmployeeRepository.AddAsync(employee);
             var count = await _unitOfWork.CompleteAsync();

             if (count > 0)
             {

                 TempData["Message"] = " Employee is Created !!";
                 return RedirectToAction(nameof(Index));
             }

         }
         ViewData["departments"] = await _unitOfWork.DepartmentRepository.GetAllAsync();
         return View(model);
     } 


        // GET: Employee/Edit/5

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest("Invalid Id");
            }

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee == null)
            {
                return NotFound(new { statusCode = 404, messege = $"Employee With Id:{id} is Not Found" });
            }
            var mappedEmployee = _mapper.Map</*Employee,*/ EmployeeViewModel>(employee);
          //  ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(mappedEmployee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageName is not null && model.Image is not null)
                {
                    DocumentSetting.Delete(model.ImageName, "images");
                }

                /* if (model.Image is not null)
                 {
                     model.ImageName = DocumentSetting.UpLoad(model.Image, "images");

                 }*/
                if (model.Image is not null)
                {
                    model.ImageName = DocumentSetting.UpLoad(model.Image, "images");
                }

                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Salary = model.Salary,
                    Address = model.Address,
                    IsActive = model.IsActive,
                    IsDelete = model.IsDeleted,
                    Age = model.Age,

                    Hiringdate = model.Hiringdate,
                    PhoneNumber = model.PhoneNumber,
                    DateOfCreation = model.DateOfCreation,
                    Email = model.Email,
                    ImageName = model.ImageName,
                    WorkForId = model.WorkForId,


                };
                {
                    _unitOfWork.EmployeeRepository.Update(employee);
                    var count = await _unitOfWork.CompleteAsync();



                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            return View(model);
            /*if (id != employeeViewModel.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                    _unitOfWork.EmployeeRepository.Update(mappedEmployee);
                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {
                    // Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the employee.");
                    // Consider specific exception handling if needed
                    return View(employeeViewModel);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(employeeViewModel);*/
        }

        // GET: Employee/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            /*if (id == null)
            {
                return BadRequest();
            }

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(mappedEmployee);*/
            return await Details(id, "Delete");
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id,EmployeeViewModel model)
        {
            /* var employee = await _unitOfWork.EmployeeRepository.GetAsync(id);
             if (employee != null)
             {
                 _unitOfWork.EmployeeRepository.Delete(employee);
                 await _unitOfWork.CompleteAsync();
             }

             return RedirectToAction(nameof(Index));*/
            if (ModelState.IsValid)
            {
                var employee = await _unitOfWork.EmployeeRepository.GetAsync(id);

                if (employee == null) return NotFound(new { statusCode = 400, messege = $"Employee With Id:{id} is Not Found" });


                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    if (model.Image is not null)
                    {
                        DocumentSetting.Delete(model.ImageName, "images");
                    }
                    return RedirectToAction(nameof(Index));
                }


            }
            return View(model);
        }
    }
}

