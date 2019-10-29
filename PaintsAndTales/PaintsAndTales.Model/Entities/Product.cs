using System.Collections.Generic;

namespace PaintsAndTales.Model.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }

		public ICollection<ProductImage> ProductImages { get; set; }

		public Product()
		{
			ProductImages = new HashSet<ProductImage>();
		}
	}
}
