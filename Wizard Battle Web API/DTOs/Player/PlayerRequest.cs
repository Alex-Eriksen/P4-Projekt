namespace Wizard_Battle_Web_API.DTOs.Player
{
	public class PlayerRequest
	{
		public int? AccountID { get; set; }

		[Required(ErrorMessage = "* is required")]
		public string PlayerName { get; set; }

		public int IconID { get; set; }
	}
}
