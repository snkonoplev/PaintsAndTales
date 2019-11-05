using System;

namespace PaintsAndTales.Model.Entities
{
	public class Contact : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string MobilePhone { get; set; }
		public string Comment { get; set; }

		public Order Order { get; set; }
	}
}
