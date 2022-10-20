namespace Wizard_Battle_Web_API.DTOs.Player
{
	public class PlayerAccountRequest
	{
		[Required(ErrorMessage = "* is required")]
		public AccountRequest Account { get; set; } = null!;

		[Required(ErrorMessage = "* is required")]
		public PlayerRequest Player { get; set; } = null!;
	}
}
