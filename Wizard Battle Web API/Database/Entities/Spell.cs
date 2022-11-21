namespace Wizard_Battle_Web_API.Database.Entities
{
	public class Spell
	{
		[Key]
		public int SpellID { get; set; }

		[Column(TypeName = "nvarchar(32)")]
		public string SpellName { get; set; }

		[Column(TypeName = "nvarchar(255)")]
		public string SpellDescription { get; set; }

		public int IconID { get; set; }
		public Icon Icon { get; set; }

		[Column(TypeName = "decimal(6, 2)")]
		public decimal ManaCost { get; set; }

		[Column(TypeName = "decimal(6, 2)")]
		public decimal DamageAmount { get; set; }

		[Column(TypeName = "decimal(6, 2)")]
		public decimal CastTime { get; set; }

		public ICollection<SpellBookSlot> SpellBookSlots { get; set; }
	}
}
