using System.Collections.Generic;
using PaintsAndTales.Model.Entities;

namespace PaintsAndTales.WebApp.Models
{
	public class ProductGroupsViewModel
	{
		public IList<Product> Products { get; set; }
		public IList<Category> Categories { get; set; }
		public int? CurrentCategoryId { get; set; }
	}
}
