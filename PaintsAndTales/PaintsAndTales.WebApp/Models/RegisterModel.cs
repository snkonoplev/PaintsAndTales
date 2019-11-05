using System.ComponentModel.DataAnnotations;

namespace PaintsAndTales.WebApp.Models
{
	public class RegisterModel
	{
		[Required(ErrorMessage = "Не указан Email")]
		[EmailAddress(ErrorMessage = "Не корректный Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Не указан пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Пароль введен неверно")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "Не указано имя")]
		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		[Required(ErrorMessage = "Не указан телефон")]
		[Phone(ErrorMessage = "Не корректный номер телефона")]
		[MinLength(10, ErrorMessage = "Не корректная длинна номера телефона")]
		public string MobilePhone { get; set; }
	}
}
