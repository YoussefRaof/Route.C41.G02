using System.ComponentModel.DataAnnotations;

namespace Route.C41.G02.PL.ViewModels.Account
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
