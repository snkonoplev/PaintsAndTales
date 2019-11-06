using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaintsAndTales.Common;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;
using PaintsAndTales.WebApp.Models;

namespace PaintsAndTales.WebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly SoftDeletedApplicationContext _context;
		private readonly IOptions<ApplicationConfig> _config;

		public AccountController(SoftDeletedApplicationContext context, IOptions<ApplicationConfig> config)
		{
			_context = context;
			_config = config;
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
				User user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == model.Email);

				if (user != null)
				{
					string password = PasswordEncrypt.DecryptStringAes(user.Password, _config.Value.Salt);

					if (model.Password == password)
					{
						await Authenticate(model.Email);
						return RedirectToAction("Index", "Home");
					}
				}

				ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View(new RegisterModel());
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

					string pas = PasswordEncrypt.EncryptStringAes(model.Password, _config.Value.Salt);


					_context.Set<User>().Add(new User
					{
						Email = model.Email, 
						Password = pas,
						FirstName = model.FirstName,
						MiddleName = model.MiddleName,
						LastName = model.LastName,
						MobilePhone = model.MobilePhone,
						Created = DateTime.UtcNow
					});
					await _context.SaveChangesAsync();

					await Authenticate(model.Email);

					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError("", "Некорректные логин и(или) пароль");
			}
			return View(model);
		}

		private async Task Authenticate(string userName)
		{
			List<Claim> claims = new List<Claim>
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