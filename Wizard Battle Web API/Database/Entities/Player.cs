namespace Wizard_Battle_Web_API.Database.Entities
{
	public class Player
	{
		[Key]
		public int PlayerID { get; set; }

		public int AccountID { get; set; }
		public Account Account { get; set; }

		[Column(TypeName = "nvarchar(32)")]
		public string PlayerName { get; set; }
		public int IconID { get; set; }
		public Icon Icon { get; set; }

		[Column(TypeName = "nvarchar(32)")]
		public string PlayerStatus { get; set; }

		public uint ExperiencePoints { get; set; }

		[Column(TypeName = "float")]
		public double MaxHealth { get; set; }

		[Column(TypeName = "float")]
		public double MaxMana { get; set; }

		public uint KnowledgePoints { get; set; }

		public uint TimeCapsules { get; set; }

		public uint MatchWins { get; set; }

		public uint MatchLosses { get; set; }

		public uint TimePlayedMin { get; set; }

		public uint AvgDamage { get; set; }

		public uint AvgSpellsHit { get; set; }

		public virtual ICollection<SpellBook> SpellBooks { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Modified_At { get; set; }

		[InverseProperty("MainPlayer")]
		public virtual ICollection<Friendship> MainPlayerFriends { get; set; }

		[InverseProperty("FriendPlayer")]
		public virtual ICollection<Friendship> Friends { get; set; }

		[InverseProperty("Sender")]
		public virtual ICollection<Message> Messages { get; set; }

		[InverseProperty("Receiver")]
		public virtual ICollection<Message> FriendMessages { get; set; }

		[InverseProperty("Player")]
		public virtual ICollection<Transaction> Transactions { get; set; }
	}
}
