using System;
using System.Collections.Generic;

namespace PaintsAndTales.Model.Entities
{
	public class Product : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public bool IsActive { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int? CategoryId { get; set; }

		public Category Category { get; set; }

		public ICollection<ImageEntity> ProductImages { get; set; }
		public ICollection<Price> Prices { get; set; }

		public Product()
		{
			ProductImages = new HashSet<ImageEntity>();
			Prices = new HashSet<Price>();
		}
	}
}
