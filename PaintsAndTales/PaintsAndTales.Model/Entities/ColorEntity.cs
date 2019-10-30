using System;
using System.Collections.Generic;

namespace PaintsAndTales.Model.Entities
{
	public class ColorEntity : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public string Name { get; set; }
		public string ColorCode { get; set; }

		public ICollection<ProductColor> ProductColors { get; set; }
		public ColorEntity()
		{
			ProductColors = new HashSet<ProductColor>();
		}
	}
}
