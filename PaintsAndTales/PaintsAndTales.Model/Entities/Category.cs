using System;
using System.Collections.Generic;

namespace PaintsAndTales.Model.Entities
{
	public class Category : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public string Name { get; set; }

		public ICollection<Product> Products { get; set; }

		public Category()
		{
			Products = new HashSet<Product>();
		}
	}
}
