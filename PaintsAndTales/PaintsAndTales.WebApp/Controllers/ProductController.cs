using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.WebApp.Controllers
{
	public class ProductController : Controller
    {
	    private readonly ILogger<ProductController> _logger;
	    private readonly SoftDeletedApplicationContext _context;
	    private readonly IOptions<ApplicationConfig> _config;

	    public ProductController(ILogger<ProductController> logger, SoftDeletedApplicationContext context, IOptions<ApplicationConfig> config)
	    {
		    _logger = logger;
		    _context = context;
		    _config = config;
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