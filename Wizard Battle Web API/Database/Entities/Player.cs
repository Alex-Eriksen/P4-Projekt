namespace Wizard_Battle_Web_API.Database.Entities
{
	public class Player
	{
		[Key]
		public int PlayerID { get; set; }

		[ForeignKey("Account.AccountID")]
		public int AccountID { get; set; }
		public Account Account { get; set; }

		[Column(TypeName = "nvarchar(32)")]
		public string PlayerName { get; set; }

		public int ExperiencePoints { get; set; }

		public int MaxHealth { get; set; }

		public int MaxMana { get; set; }

		[ForeignKey("PlayerStats.PlayerStatsID")]
		public int PlayerStatsID { get; set; }
		public PlayerStats PlayerStats { get; set; }

		public int KnowledgePoints { get; set; }

		public int TimeCapsules { get; set; }

		public DateTime Modified_At { get; set; }
	}
}
