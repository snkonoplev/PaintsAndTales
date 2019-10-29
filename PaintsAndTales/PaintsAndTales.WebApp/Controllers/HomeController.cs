using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;
using PaintsAndTales.WebApp.Models;

namespace PaintsAndTales.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			List<Product> products = await _context.Set<Product>()
				.Include(a => a.ProductImages)
				.Where(a => a.ProductImages.Any(x => x.IsTitleImage))
				.ToListAsync();

			return View(products);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Show(string path)
		{
			byte[] array = System.IO.File.ReadAllBytes(path);
			return File(array, "image/jpg");
		}
	}
}
