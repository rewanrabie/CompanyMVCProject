using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_3.ViewModels;
using MVC_3_DAL.Models;

namespace MVC_3.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        //Get , GetAll , Add , Update , Delete 
        //Index ,Create, Details , Create , Edit , Delete 
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? InputSearch)
        {
           // IEnumerable<RoleViewModel> roles;
            var users = Enumerable.Empty<RoleViewModel>();

            if (string.IsNullOrEmpty(InputSearch))
            {
                users = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                }).ToListAsync();
            }
            else
            {
                users = await _roleManager.Roles.Where(R => R.Name.ToLower().Contains(InputSearch.ToLower()))
                   .Select(R => new RoleViewModel()
                   {
                       Id = R.Id,
                       RoleName = R.Name
                   }).ToListAsync();
            }
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            /* if (ModelState.IsValid)
             {
                 var role = new IdentityRole()
                 {
                     Name = model.RoleName,
                 };
                 await _roleManager.CreateAsync(role);
                 return RedirectToAction(nameof(Index));
             }

             return View();*/
            if (ModelState.IsValid)
            {

                var role = await _roleManager.FindByNameAsync(model.RoleName);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.RoleName,
                    };

                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                }

            }
            return View(model);
        }
       // [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var roleFromDb = await _roleManager.FindByIdAsync(id);

            if (roleFromDb is null)
                return NotFound();

            var role = new RoleViewModel()
            {
                Id = roleFromDb.Id,
                RoleName = roleFromDb.Name
            };
            return View(ViewName, role);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (id != model.Id)
                    return BadRequest();
                var roleFromDb = await _roleManager.FindByIdAsync(id);
                if (roleFromDb is null)
                    return NotFound();

                roleFromDb.Name = model.RoleName;

                await _roleManager.UpdateAsync(roleFromDb);

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
        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                    return BadRequest();
                var roleFromDb = await _roleManager.FindByIdAsync(id);
                if (roleFromDb is null)
                    return NotFound();

                await _roleManager.DeleteAsync(roleFromDb);

                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            ViewData["RoleId"] = roleId;

            var usersInRole = new List<UsersInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInrole = new UsersInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInrole.IsSelected = true;
                }
                else
                {
                    userInrole.IsSelected = false;
                }
                usersInRole.Add(userInrole);
            }
            return View(usersInRole);

        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UsersInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }

                }
                return RedirectToAction(nameof(Edit), new { id = roleId });
            }
            return View(users);
        }
    }
}



