namespace Wizard_Battle_Web_API.DTOs.Player
{
	public class PlayerRequest
	{
		[Required(ErrorMessage = "* is required")]
		public int AccountID { get; set; }

		[Required(ErrorMessage = "* is required")]
		public string PlayerName { get; set; }
	}
}
