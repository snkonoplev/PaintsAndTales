namespace PaintsAndTales.Model.Entities
{
	public class ProductImage
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public bool IsTitleImage { get; set; }
		public string Path { get; set; }

		public Product Product { get; set; }
	}
}
