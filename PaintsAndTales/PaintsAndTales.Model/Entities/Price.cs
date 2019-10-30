using System;

namespace PaintsAndTales.Model.Entities
{
	public class Price : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public int ProductId { get; set; }
		public int ProductSizeId { get; set; }
		public decimal Value { get; set; }

		public Product Product { get; set; }
		public ProductSize ProductSize { get; set; }
	}
}
