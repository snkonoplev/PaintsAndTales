using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;
using PaintsAndTales.WebApp.Models;

namespace PaintsAndTales.WebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly SoftDeletedApplicationContext _context;
		public AccountController(SoftDeletedApplicationContext context)
		{
			_context = context;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
				if (user != null)
				{
					await Authenticate(model.Email);

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return View(model);
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == model.Email);
				if (user == null)
				{
					_context.Set<User>().Add(new User
					{
						Email = model.Email, 
						Password = model.Password,
						FirstName = model.FirstName,
						MiddleName = model.MiddleName,
						LastName = model.LastName,
						MobilePhone = model.MobilePhone
					});
					await _context.SaveChangesAsync();

					await Authenticate(model.Email);

					return RedirectToAction("Index", "Home");
				}
				else
					ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return View(model);
		}

		private async Task Authenticate(string userName)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
			};
			
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}
	}
}