using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.WebApp.Models
{
	public class ProductViewModel
	{
		public Product Product { get; set; }
		public int? ColorId { get; set; }
		public int SizeId { get; set; }
	}
}
