using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.WebApp.Models
{
	public class CardItemViewModel
	{
		public Product Product { get; set; }
		public ImageEntity ImageEntity { get; set; }
		public Price Price { get; set; }
		public int Quantity { get; set; }
	}
}
