using System;

namespace PaintsAndTales.Model.Entities
{
	public class ImageEntity : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public int? ProductId { get; set; }
		public int? ColorId { get; set; }
		public bool IsTitleImage { get; set; }
		public string FileName { get; set; }
		public string FileExtension { get; set; }

		public Product Product { get; set; }
		public ColorEntity Color { get; set; }
	}
}
