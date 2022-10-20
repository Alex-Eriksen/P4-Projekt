namespace Wizard_Battle_Web_API.DTOs.Player
{
	public class DirectPlayerResponse
	{
		public int PlayerID { get; set; } = 0;

		public StaticAccountResponse Account { get; set; } = null!;

		public string PlayerName { get; set; } = string.Empty;
	}
}
