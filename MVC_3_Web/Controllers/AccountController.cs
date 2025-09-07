using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_3.Helpers;
using MVC_3.ViewModels.Auth;
using MVC_3_DAL.Models;

namespace MVC_3.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}


		#region SignUp
		//Sign Up 

		[HttpGet] //Account // SignUp
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost] //Account // SignUp
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				//SignUp
				try
				{
					var User = await _userManager.FindByNameAsync(model.UserName);
					if (User is null)
					{
						User = await _userManager.FindByEmailAsync(model.Email);
						if (User is null)
						{
							User = new ApplicationUser()
							{
								UserName = model.UserName,
								Email = model.Email,
								FirstName = model.FirstName,
								LastName = model.LastName,
								IsAgree = model.IsAgree
							};
							var result = await _userManager.CreateAsync(User, model.Password);
							if (result.Succeeded)
							{
								return RedirectToAction("SignIn");
							}
							foreach (var error in result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
						}
						ModelState.AddModelError(string.Empty, "Email Is Already Exits !!");
						return View();
					}
					ModelState.AddModelError(string.Empty, "UserName Is Already Exits !!");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View();
		}
        #endregion

        #region SignIn
        //SignIn

        [HttpGet]
        public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						//checkpassword
						var flag = await _userManager.CheckPasswordAsync(user, model.Password);
						if (flag)
						{
							//SignIn

							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

							if (result.Succeeded)
							{
								return RedirectToAction(nameof(HomeController.Index), "Home");
							}
						}

					}

					ModelState.AddModelError(string.Empty, "Invaild Login ! !");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}

			return View(model);
		}
        #endregion

        #region SignOut
        [HttpGet]
        public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}
		#endregion

		#region ForgetPassword
		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					//Create Token 
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
					//Create Reset Password URL
					var url = Url.Action("Reset Password", "Account", new { email = model.Email }, Request.Scheme);
					//Create Email

					// https://localhost:44361/Account/ResetPassword?email=rewan@gmail.com&token

					var email = new Email()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = url
					};
					//Send Email

					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Invaild Operation.Plese Try Again !!");
			}
			return View(model);
		}
		[HttpGet]
		public IActionResult CheckYourInbox()
		{
			return View();
		}
		#endregion

		#region ResetPassword
		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded)
					{
						RedirectToAction(nameof(SignIn));
					}
				}

			}
			ModelState.AddModelError(string.Empty, "Invaild Operation.Plese Try Again !!");
			return View(); //model
        }
		#endregion


		public IActionResult AccessDenied()
		{
			return View();
		}
	}

}
