namespace Wizard_Battle_Web_API.DTOs.Player
{
	public class DirectPlayerResponse
	{
		public int PlayerID { get; set; } = 0;

		public StaticAccountResponse Account { get; set; } = null!;

		public string PlayerName { get; set; } = string.Empty;

		public Icon Icon { get; set; } = null!;

		public string PlayerStatus { get; set; } = string.Empty;

		public uint ExperiencePoints { get; set; } = 0;

		public double MaxHealth { get; set; } = 0;

		public double MaxMana { get; set; } = 0;

		public uint KnowledgePoints { get; set; } = 0;

		public uint TimeCapsules { get; set; } = 0;

	}
}
