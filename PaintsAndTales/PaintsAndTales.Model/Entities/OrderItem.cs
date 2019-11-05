using System;

namespace PaintsAndTales.Model.Entities
{
	public class OrderItem : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int SizeId { get; set; }
		public int ColorId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }

		public Order Order { get; set; }
		public Product Product { get; set; }
		public ProductSize ProductSize { get; set; }
		public ColorEntity ColorEntity { get; set; }
	}
}
