namespace Wizard_Battle_Web_API.DTOs.Player
{
	public class PlayerRequest
	{
		[Required(ErrorMessage = "* is required")]
		public string PlayerName { get; set; }
	}
}
