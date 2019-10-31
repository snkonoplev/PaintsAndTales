using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaintsAndTales.Model;

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
		public IActionResult Shop()
        {
            return View();
        }
    }
}