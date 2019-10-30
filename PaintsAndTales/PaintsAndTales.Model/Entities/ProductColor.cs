using System;

namespace PaintsAndTales.Model.Entities
{
	public class ProductColor : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public int ProductId { get; set; }
		public int ColorEntityId { get; set; }

		public ColorEntity ColorEntity { get; set; }
		public Product Product { get; set; }
	}
}
