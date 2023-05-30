using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MeetingRegistryClient.Models
{
	public class LoginPassword
	{
		[Required(ErrorMessage = "Введите логин")]
		[Display(Name = "Логин")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Введите пароль")]
		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
