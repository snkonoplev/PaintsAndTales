using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ShopController : Controller
    {
	    private readonly ILogger<ShopController> _logger;
	    private readonly SoftDeletedApplicationContext _context;
	    private readonly IOptions<ApplicationConfig> _config;

	    public ShopController(ILogger<ShopController> logger, SoftDeletedApplicationContext context, IOptions<ApplicationConfig> config)
	    {
		    _logger = logger;
		    _context = context;
		    _config = config;
	    }

	    [Route("shop")]
		public async Task<IActionResult> Shop(int? categoryId)
		{
			List<Product> products = await _context.Set<Product>()
				.Include(a => a.ProductImages)
				.Include(a => a.Prices)
				.Where(a => a.IsActive && a.ProductImages.Any(x => x.IsTitleImage) && (categoryId == null || a.CategoryId == categoryId))
				.ToListAsync();

			List<Category> categories = await _context.Set<Category>()
				.Include(a => a.Products)
				.Where(a => a.Products.Any())
				.ToListAsync();

			ProductGroupsViewModel model = new ProductGroupsViewModel
			{
				Products = products,
				Categories = categories,
				CurrentCategoryId = categoryId
			};

			List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();

			ViewBag.cartCount = cart.Count;

			return View(model);
		}
    }
}