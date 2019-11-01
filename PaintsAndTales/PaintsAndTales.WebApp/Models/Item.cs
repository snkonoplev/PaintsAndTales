using System;

namespace PaintsAndTales.WebApp.Models
{
	public class Item
	{
		public Guid Id { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public int SizeId { get; set; }
		public int ColorId { get; set; }
	}
}
