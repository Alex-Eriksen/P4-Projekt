namespace Wizard_Battle_Web_API.Database.Entities
{
	public class Transaction
	{
		[Key]
		public int TransactionID { get; set; }

		[ForeignKey("SkinItem.SkinID")]
		public int SkinID { get; set; }
		public SkinItem SkinItem { get; set; }

		[ForeignKey("Player.PlayerID")]
		public int PlayerID { get; set; }
		public Player Player { get; set; }

		public int TotalCost { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Created_At { get; set; }
	}
}
