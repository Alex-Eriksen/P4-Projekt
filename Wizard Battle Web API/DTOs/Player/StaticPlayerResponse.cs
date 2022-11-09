namespace Wizard_Battle_Web_API.DTOs.Player
{
	public class StaticPlayerResponse
	{
		public int PlayerID { get; set; } = 0;
		
		public int AccountID { get; set; } = 0;

		public string PlayerName { get; set; } = string.Empty;

		public IconResponse Icon { get; set; }

		public string PlayerStatus { get; set; } = string.Empty;

		public uint ExperiencePoints { get; set; } = 0;

		public uint MatchWins { get; set; } = 0;

		public uint MatchLosses { get; set; } = 0;

		public uint TimePlayedMin { get; set; } = 0;
	}
}
