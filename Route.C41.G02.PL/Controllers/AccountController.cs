using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.C4.G02.DAL.Models;
using Route.C41.G02.PL.ViewModels.Account;
using System.Threading.Tasks;

namespace Route.C41.G02.PL.Controllers
{
	
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region Sign Up

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.Username);

				if (user == null)
				{
					user = new ApplicationUser()
					{
						FName = model.FirstName,
						LName = model.LastName,
						UserName = model.Username,
						Email = model.Email,
						IsAgree = model.IsAgree,
					};

					var Result=  await _userManager.CreateAsync(user,model.Password);
					if (Result.Succeeded)
						return RedirectToAction(nameof(SignIn));

					foreach (var error in Result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);

				}
				ModelState.AddModelError(string.Empty, "This UserName Already Has An Account!!");




			}
			return View(model);

		}
		#endregion


		#region SignIn


		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if(user is not null)
				{
					bool flag = await _userManager.CheckPasswordAsync(user, model.Password);

					if(flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RemeberMe, false);

						if (result.Succeeded)
							return RedirectToAction(nameof(HomeController.Index), "Home");
						if (result.IsNotAllowed)
							ModelState.AddModelError(string.Empty, "Your Email Is Not Confirmed !!");
						if (result.IsLockedOut)
							ModelState.AddModelError(string.Empty, "Your Email Is Locked!!");

					}
				}

				ModelState.AddModelError(string.Empty, "Invalid Login");

			}
			return View(model);
		}
		#endregion
	}
}
