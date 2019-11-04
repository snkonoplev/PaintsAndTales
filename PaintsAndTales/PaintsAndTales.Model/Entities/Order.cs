using System;
using System.Collections.Generic;

namespace PaintsAndTales.Model.Entities
{
	public class Order : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public int? UserId { get; set; }
		public int? ContactId { get; set; }

		public User User { get; set; }
		public Contact Contact { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; }

		public Order()
		{
			OrderItems = new HashSet<OrderItem>();
		}
	}
}
