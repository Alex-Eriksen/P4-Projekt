namespace Wizard_Battle_Web_API.DTOs.Account
{
	public class AccountRequest
	{
		[Required(ErrorMessage = "* is required")]
		public string Email { get; set; }

		[Required(ErrorMessage = "* is required")]
		public string Password { get; set; }
	}
}
