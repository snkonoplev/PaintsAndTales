using System.ComponentModel.DataAnnotations;

namespace PaintsAndTales.WebApp.Models
{
	public class ContactViewModel
	{
		[Required(ErrorMessage = "Не указано имя")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Не указан адрес")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Не указан телефон")]
		[Phone(ErrorMessage = "Не корректный номер телефона")]
		[MinLength(10, ErrorMessage = "Не корректная длинна номера телефона")]
		public string MobilePhone { get; set; }
		public string Comment { get; set; }
	}
}
