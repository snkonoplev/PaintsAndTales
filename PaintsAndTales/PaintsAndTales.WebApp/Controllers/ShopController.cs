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
		public async Task<IActionResult> Shop()
        {
			List<Product> products = await _context.Set<Product>()
				.Include(a => a.ProductImages)
				.Include(a => a.Prices)
				.Where(a => a.IsActive && a.ProductImages.Any(x => x.IsTitleImage))
				.Take(8)
				.ToListAsync();

			return View(products);
		}
    }
}