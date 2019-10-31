using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.WebApp.Controllers
{
	public class ProductController : Controller
    {
	    private readonly SoftDeletedApplicationContext _context;

	    public ProductController(SoftDeletedApplicationContext context)
	    {
		    _context = context;
	    }

	    [Route("product/{id:int}")]
		public IActionResult Product(int id)
		{
			Product product = _context.Set<Product>()
				.Include(a => a.ProductImages)
				.Include(a => a.Prices)
				.Single(a => a.Id == id);

			return View(product);
        }
    }
}