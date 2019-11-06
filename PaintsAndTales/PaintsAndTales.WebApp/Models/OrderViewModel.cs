using System;
using System.Collections.Generic;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.WebApp.Models
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public DateTime? CreateDateTime { get; set; }
		public List<OrderItemViewModel> Items { get; set; }
		public decimal Total { get; set; }

	}
	public class OrderItemViewModel
	{
		public Product Product { get; set; }
		public string SizeName { get; set; }
		public string ColorName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
