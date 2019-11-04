using System;
using System.Collections.Generic;

namespace PaintsAndTales.Model.Entities
{
	public class User : IEntity
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Deleted { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string MobilePhone { get; set; }

		public ICollection<Order> Orders { get; set; }

		public User()
		{
			Orders = new HashSet<Order>();
		}
	}
}
