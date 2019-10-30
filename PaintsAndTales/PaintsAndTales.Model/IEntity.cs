using System;

namespace PaintsAndTales.Model
{
	public interface IEntity
	{
		int Id { get; set; }
		DateTime Created { get; set; }
		DateTime? Deleted  { get; set; }
	}
}