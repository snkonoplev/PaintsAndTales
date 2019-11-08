using System;

namespace PaintsAndTales.AdminApp.ViewModels
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public string FullName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public int? Quantity { get; set; }
		public decimal? Price { get; set; }
	}
}
