namespace Wizard_Battle_Web_API.DTOs.Player
{
	public class PlayerRequest
	{
		public int? AccountID { get; set; }

		[Required(ErrorMessage = "* is required")]
		public string PlayerName { get; set; }

		public int IconID { get; set; }

		public uint ExperiencePoints { get; set; }

		public uint KnowledgePoints { get; set; }

		public uint TimeCapsules { get; set; }

		public uint MatchWins { get; set; }

		public uint MatchLosses { get; set; }

		public uint TimePlayedMin { get; set; }
	}
}
