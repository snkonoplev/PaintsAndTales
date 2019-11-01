using System;
using System.Collections.Generic;

namespace PaintsAndTales.Model.Entities
{
	public class ColorEntity : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public int ImageId { get; set; }
		public string Name { get; set; }

		public ImageEntity ImageEntity { get; set; }

		public ICollection<ImageEntity> ProductImages { get; set; }
		public ColorEntity()
		{
			ProductImages = new HashSet<ImageEntity>();
		}
	}
}
