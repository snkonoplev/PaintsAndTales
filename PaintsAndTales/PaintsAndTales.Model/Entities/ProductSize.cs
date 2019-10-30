using System;
using System.Collections.Generic;

namespace PaintsAndTales.Model.Entities
{
	public class ProductSize : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public string Name { get; set; }
		public string Size { get; set; }

		public ICollection<Price> Prices { get; set; }

		public ProductSize()
		{
			Prices = new HashSet<Price>();
		}
	}
}
