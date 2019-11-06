using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;
using PaintsAndTales.WebApp.Code;
using PaintsAndTales.WebApp.Models;

namespace PaintsAndTales.WebApp.Controllers
{
    public class OrderController : Controller
    {
	    private readonly SoftDeletedApplicationContext _context;

		public OrderController(SoftDeletedApplicationContext context)
	    {
		    _context = context;
	    }

		public async Task<IActionResult> Index()
		{
			User user = await _context.Set<User>().SingleOrDefaultAsync(a => a.Email == User.Identity.Name) ?? new User();
			
			ContactViewModel model = new ContactViewModel
			{
				Name = user.FirstName,
				MobilePhone = user.MobilePhone
			};

			return View(model);
        }

		[HttpPost]
		public async Task<IActionResult> Register(ContactViewModel model)
		{
			List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
			
			if (ModelState.IsValid && cart != null && cart.Any())
			{
				User user = await _context.Set<User>().SingleOrDefaultAsync(a => a.Email == User.Identity.Name);

				Order order = new Order
				{
					Created = DateTime.UtcNow,
					User = user,
					Contact = new Contact
					{
						Created = DateTime.UtcNow,
						Name = model.Name,
						Address = model.Address,
						MobilePhone = model.MobilePhone,
						Comment = model.Comment
					}
				};

				foreach (Item item in cart)
				{
					Product product = await _context.Set<Product>()
						.Include(a => a.Prices)
						.ThenInclude(a => a.ProductSize)
						.Include(a => a.ProductImages)
						.ThenInclude(a => a.Color)
						.FirstAsync(a => a.Id == item.ProductId);

					order.OrderItems.Add(new OrderItem
					{
						ProductId = item.ProductId,
						SizeId = item.SizeId,
						ColorId = item.ColorId,
						Quantity = item.Quantity,
						Price = product.Prices.First(a => a.ProductSizeId == item.SizeId).Value
					});
				}

				_context.Set<Order>().Add(order);
				await _context.SaveChangesAsync();
				HttpContext.Session.SetObjectAsJson("cart", new List<Item>());

				return View();
			}

			return View("Index", model);
		}

		[Authorize]
		[Route("orders")]
		public async Task<IActionResult> UserOrders()
		{
			List<OrderViewModel> model = new List<OrderViewModel>();

			User user = await _context.Set<User>()
				.Include(a => a.Orders)
				.ThenInclude(a => a.OrderItems)
				.SingleAsync(a => a.Email == User.Identity.Name);

			foreach (var order in user.Orders)
			{
				OrderViewModel orderView = new OrderViewModel
				{
					Id = order.Id,
					CreateDateTime = order.Created,
					Total = order.OrderItems.Sum(a => a.Price * a.Quantity),
					Items = new List<OrderItemViewModel>()
				};

				foreach (var item in order.OrderItems)
				{
					OrderItem orderItem = await _context.Set<OrderItem>()
						.Include(a => a.ColorEntity)
						.Include(a => a.ProductSize)
						.Include(a => a.Product)
						.SingleAsync(a => a.Id == item.Id);

					OrderItemViewModel itemView = new OrderItemViewModel
					{
						ColorName = orderItem.ColorEntity.Name,
						Product = orderItem.Product,
						SizeName = orderItem.ProductSize.Name,
						Quantity = orderItem.Quantity,
						Price = orderItem.Price
					};

					orderView.Items.Add(itemView);
				}

				if(orderView.Items.Any())
					model.Add(orderView);
			}

			return View(model);
		}
    }
}