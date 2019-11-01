using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;
using PaintsAndTales.WebApp.Code;
using PaintsAndTales.WebApp.Models;

namespace PaintsAndTales.WebApp.Controllers
{
	public class ProductController : Controller
    {
	    private readonly SoftDeletedApplicationContext _context;

	    public ProductController(SoftDeletedApplicationContext context)
	    {
		    _context = context;
	    }

	    [Route("product")]
		public IActionResult Product(int id, int? sizeId, int? colorId)
		{
			Product product = _context.Set<Product>()
				.Include(a => a.ProductImages)
				.ThenInclude(a => a.Color)
				.ThenInclude(a => a.ImageEntity)
				.Include(a => a.Prices)
				.ThenInclude(a => a.ProductSize)
				.Single(a => a.Id == id);

			ProductViewModel model = new ProductViewModel
			{
				Product = product,
				ColorId = colorId,
				SizeId = sizeId ?? product.Prices.First(a => a.Value == product.Prices.Min(x => x.Value)).ProductSizeId
			};

			List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();

			ViewBag.cartCount = cart.Count;

			return View(model);
        }
    }
}