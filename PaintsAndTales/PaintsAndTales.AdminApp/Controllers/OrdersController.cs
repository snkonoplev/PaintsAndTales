using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintsAndTales.AdminApp.ViewModels;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.AdminApp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		private readonly SoftDeletedApplicationContext _context;

		public OrdersController(SoftDeletedApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			List<Order> orders = await _context.Set<Order>()
				.Include(a => a.Contact)
				.Include(a => a.User)
				.Include(a => a.OrderItems)
				.OrderByDescending(a => a.Created)
				.ToListAsync();

			var result = orders
				.Select(a => new OrderViewModel
				{
					Id = a.Id,
					Created = a.Created,
					Deleted = a.Deleted,
					FullName = a.User == null ? a.Contact?.Name : ($"{a.User?.LastName} {a.User?.FirstName} {a.User?.MiddleName}").Trim(),
					Phone = a.Contact?.MobilePhone,
					Email = a.User?.Email,
					Quantity = a.OrderItems.Sum(x => x.Quantity),
					Price = a.OrderItems.Sum(x => x.Price * x.Quantity)
				})
				.ToList();

			return Ok(result);
		}
	}
}
