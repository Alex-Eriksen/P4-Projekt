namespace Wizard_Battle_Web_API.Database.Entities
{
	public class SpellBook
	{
		[Key]
		public int SpellBookID { get; set; }

		[Column(TypeName = "nvarchar(32)")]
		public string SpellBookName { get; set; }
		
		[ForeignKey("Player.PlayerID")]
		public int PlayerID { get; set; }
		public virtual Player Player { get; set; }

		public ICollection<SpellBookSlot> SpellBookSlots { get; set; }

		public string SpellOrder { get; set; }
	}
}
