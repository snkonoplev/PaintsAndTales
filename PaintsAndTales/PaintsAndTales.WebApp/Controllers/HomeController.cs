using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;
using PaintsAndTales.WebApp.Code;
using PaintsAndTales.WebApp.Models;

namespace PaintsAndTales.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly SoftDeletedApplicationContext _context;
		private readonly IOptions<ApplicationConfig> _config;

		public HomeController(ILogger<HomeController> logger, SoftDeletedApplicationContext context, IOptions<ApplicationConfig> config)
		{
			_logger = logger;
			_context = context;
			_config = config;
		}

		public async Task<IActionResult> Index()
		{
			List<Product> products = await _context.Set<Product>()
				.Include(a => a.ProductImages)
				.Include(a => a.Prices)
				.Where(a => a.IsActive && a.ProductImages.Any(x => x.IsTitleImage))
				.Take(8)
				.ToListAsync();

			List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();

			ViewBag.cartCount = cart.Count;

			return View(products);
		}
		
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Show(string name, string extension)
		{
			try
			{
				string imagePath = $"{Path.Combine(_config.Value.ImageFolder, name)}.{extension}";
				byte[] array = System.IO.File.ReadAllBytes(imagePath);
				return File(array, "image/jpg");
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Can't get image {name}.{extension}");
				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[Route("contacts")]
		public IActionResult Contacts()
		{
			return View();
		}

		[Route("sitemap")]
		[Route("sitemap.xml")]
		public ActionResult Sitemap()
		{
			Generator sitemapGenerator = new Generator(_context);
			var sitemapNodes = sitemapGenerator.GetSitemapNodes(Url);
			string xml = sitemapGenerator.GetSitemapDocument(sitemapNodes);
			return Content(xml, "text/xml", Encoding.UTF8);
		}
	}
}
