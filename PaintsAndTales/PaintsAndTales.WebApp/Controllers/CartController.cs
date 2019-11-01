using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintsAndTales.Model;
using PaintsAndTales.Model.Entities;
using PaintsAndTales.WebApp.Code;
using PaintsAndTales.WebApp.Models;

namespace PaintsAndTales.WebApp.Controllers
{
	[Route("cart")]
	public class CartController : Controller
    {
	    private readonly SoftDeletedApplicationContext _context;

	    public CartController(SoftDeletedApplicationContext context)
	    {
		    _context = context;
	    }

		[Route("")]
		public IActionResult Index()
		{
			List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");

			if (cart == null || !cart.Any(a => a.Quantity > 0))
				return View(new List<CardItemViewModel>());

			List<CardItemViewModel> result = new List<CardItemViewModel>();

			foreach (Item item in cart.Where(a => a.Quantity > 0))
			{
				Product product = _context.Set<Product>()
					.Include(a => a.Prices)
					.ThenInclude(a => a.ProductSize)
					.Include(a => a.ProductImages)
					.ThenInclude(a => a.Color)
					.First(a => a.Id == item.ProductId);
				
				CardItemViewModel resultItem = new CardItemViewModel
				{
					Product = product,
					ImageEntity = product.ProductImages.First(a => a.ColorId == item.ColorId),
					Price = product.Prices.First(a => a.ProductSizeId == item.SizeId),
					Quantity = item.Quantity,
					Id = item.Id
				};

				result.Add(resultItem);
			}

			ViewBag.cartCount = cart.Count;

			return View(result);
		}

		[Route("buy")]
		public IActionResult Buy(int id, int colorId, int sizeId, int quantity)
		{
			Product product = _context.Set<Product>()
				.Include(a => a.Prices)
				.ThenInclude(a => a.ProductSize)
				.Include(a => a.ProductImages)
				.SingleOrDefault(a => a.Id == id && a.Prices.Any(x => x.ProductSizeId == sizeId) && a.ProductImages.Any(x => x.ColorId == colorId));

			if (product == null)
				return NotFound();

			if (HttpContext.Session.GetObjectFromJson<List<Item>>("cart") == null)
			{
				List<Item> cart = new List<Item>();
				cart.Add(new Item { ProductId = id, Quantity = quantity, ColorId = colorId, SizeId = sizeId, Id = Guid.NewGuid()});
				HttpContext.Session.SetObjectAsJson("cart", cart);
			}
			else
			{
				List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");

				Item item = cart.FirstOrDefault(a => a.ProductId == id && a.ColorId == colorId && a.SizeId == sizeId);

				if (item != null)
				{
					item.Quantity = item.Quantity + quantity;
				}
				else
				{
					cart.Add(new Item { ProductId = id, Quantity = quantity, ColorId = colorId, SizeId = sizeId, Id = Guid.NewGuid() });
				}

				HttpContext.Session.SetObjectAsJson("cart", cart);
			}

			return Redirect(Request.Headers["Referer"].ToString());
		}

		[Route("remove")]
		public IActionResult Remove(Guid id)
		{
			List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");

			if (cart != null)
			{
				var item = cart.SingleOrDefault(a => a.Id == id);

				if (item != null)
					cart.Remove(item);

				HttpContext.Session.SetObjectAsJson("cart", cart);
			}

			return Redirect(Request.Headers["Referer"].ToString());
		}

		[Route("refresh")]
		public IActionResult Refresh(Guid id, int quantity)
		{
			List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");

			if (cart != null)
			{
				var item = cart.SingleOrDefault(a => a.Id == id);

				if (item != null)
					item.Quantity = quantity;

				HttpContext.Session.SetObjectAsJson("cart", cart);
			}

			return Redirect(Request.Headers["Referer"].ToString());
		}
	}
}