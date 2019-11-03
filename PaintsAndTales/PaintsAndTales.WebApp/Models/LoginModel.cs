using System.ComponentModel.DataAnnotations;

namespace PaintsAndTales.WebApp.Models
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Не указан Email")]
		[EmailAddress(ErrorMessage = "Не корректный Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Не указан пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
