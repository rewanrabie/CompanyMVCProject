using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_3.ViewModels.Departments;
using MVC_3_BLL.Interfaces;
using MVC_3_BLL.Repositories;
using MVC_3_DAL.Models;
using System.Drawing;
using System.Linq.Expressions;

namespace MVC_3.Controllers
{
   // [Authorize]
    public class DepartmentController : Controller
    {
       // private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)  //ASK CLR Create Object From DepartmentRepository
        {
           // _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        //Get//Departments//Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartment model)
        {
            if (ModelState.IsValid) //Server Side Vailation
            {
                try
                {
                    var department = new Department()
                    {
                        Name = model.Name,
                        Code = model.Code,
                        DateOfCreation = model.DateOfCreation
                    };
                    await _unitOfWork.DepartmentRepository.AddAsync(department);
                    var count = await _unitOfWork.CompleteAsync();

                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
                return View(model);
            }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewname = "Details")  
        {
            try
            {
                if (id is null) return BadRequest(); //400

                var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

                if (department is null) return NotFound(new { statusCode = 400, messege = $"Department With Id:{id} is Not Found" }); //400

                return View(viewname,department);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty , ex.Message);
                return View("Error" , "Home");
            }    
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            /*  try
              {
                  if (id is null) return BadRequest(); //400
                  var department =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
                  if (department is null) return NotFound(); //400
                  return View(department);
              }
              catch (Exception ex)
              {
                  ModelState.AddModelError(string.Empty, ex.Message);
                  return View("Error", "Home");
              }      */
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [FromRoute] int id,CreateDepartment model) 
        {
            /* try
             {
                 if (id != model.Id) return BadRequest(); //400
                 if (ModelState.IsValid)
                 {
                      _unitOfWork.DepartmentRepository.Update(model);
                     var count =await _unitOfWork.CompleteAsync();
                     if (count > 0)
                     {
                         return RedirectToAction(nameof(Index));
                     }
                 }
             }
             catch (Exception EX)
             {
                 ModelState.AddModelError(string.Empty, EX.Message);
             }
             return View(model);*/
            if (ModelState.IsValid) // server side validation
            {
                var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

                if (department == null) return NotFound(new { statusCode = 400, messege = $"Department With Id:{id} is Not Found" });

                department.Name = model.Name;
                department.Code = model.Code;
                department.DateOfCreation = model.DateOfCreation;

                _unitOfWork.DepartmentRepository.Update(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);      
    }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            /* try
             {
                 if (id is null) return BadRequest(); //400
                 var department =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
                 if (department is null) return NotFound(); //400
                 return View(department);
             }
             catch (Exception ex)
             {
                 ModelState.AddModelError(string.Empty , ex.Message);
                 return View("Error" , "Home");
             }*/
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id,CreateDepartment model)  //100
        {
            /* try
             {
                 if (id != model.Id) return BadRequest(); //400

                 if (ModelState.IsValid)
                 {
                      _unitOfWork.DepartmentRepository.Delete(model);
                     var count =await _unitOfWork.CompleteAsync();
                     if (count > 0)
                     {
                         return RedirectToAction(nameof(Index));
                     }
                 }
             }
             catch (Exception EX)
             {
                 ModelState.AddModelError(string.Empty, EX.Message);
             }
             return View(model);
         }
     }*/
            if (ModelState.IsValid) // server side validation
            {
                var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

                if (department == null) return NotFound(new { statusCode = 400, messege = $"Department With Id:{id} is Not Found" });

                _unitOfWork.DepartmentRepository.Delete(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

    }
}
