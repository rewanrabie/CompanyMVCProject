using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_3.Helpers;
using MVC_3.ViewModels;
using MVC_3.ViewModels.Employees;
using MVC_3_DAL.Models;

namespace MVC_3.Controllers
{
	//[Authorize(Roles = "Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		//Get , GetAll , Add , Update , Delete 
		//Index , Details , Create , Edit , Delete 
		public UserController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<IActionResult> Index(string? InputSearch)
		{
			var users = Enumerable.Empty<UserViewModel>();

			if (string.IsNullOrEmpty(InputSearch))
			{
				users = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
			}
			else
			{
				users = await _userManager.Users.Where(U => U.Email.ToLower().Contains(InputSearch.ToLower()))
				   .Select(U => new UserViewModel()
				   {
					   Id = U.Id,
					   FirstName = U.FirstName,
					   LastName = U.LastName,
					   Email = U.Email,
					   Roles = _userManager.GetRolesAsync(U).Result
				   }).ToListAsync();
			}
			return View(users);
		}

		public async Task<IActionResult> Details(string? id, string ViewName = "Details")
		{
			if (id is null)
				return BadRequest("Invalid Id");

			var userFromDb = await _userManager.FindByIdAsync(id);

			if (userFromDb is null)
				return NotFound(new { statusCode = 404, messege = $"User With Id:{id} is Not Found" });

			var user = new UserViewModel()
			{
				Id = userFromDb.Id,
				UserName=userFromDb.UserName,
				FirstName = userFromDb.FirstName,
				LastName = userFromDb.LastName,
				Email = userFromDb.Email,
				Roles = _userManager.GetRolesAsync(userFromDb).Result
			};
			return View(ViewName,user);

		}


        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, UserViewModel model)
        {
                if (ModelState.IsValid)
                {
                if (id != model.Id)
                    return BadRequest("Invaild");
                var userFromDb = await _userManager.FindByIdAsync(id);
                if (userFromDb is null)
                    return NotFound();

				userFromDb.UserName = model.UserName;
                userFromDb.FirstName = model.FirstName;
                userFromDb.LastName = model.LastName;
                userFromDb.Email = model.Email;

                   await  _userManager.UpdateAsync(userFromDb);

                        return RedirectToAction(nameof(Index));
                    
                }
            return View(model);
        }
          
        
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (id != model.Id)
                    return BadRequest("Invalid");
                var userFromDb = await _userManager.FindByIdAsync(id);
                if (userFromDb is null)
                    return NotFound();

                await _userManager.DeleteAsync(userFromDb);

                return RedirectToAction(nameof(Index));

            }
            return View(model);

        }

    }
}
