namespace Wizard_Battle_Web_API.DTOs.Spell
{
	public class SpellRequest
	{
		public string SpellName { get; set; }

		public string SpellDescription { get; set; }

		public int IconID { get; set; }

		public int SpellTypeID { get; set; }

		public int SchoolCategoryID { get; set; }

		public decimal DamageAmount { get; set; }

		public decimal ManaCost { get; set; }

		public decimal LifeTime { get; set; }

		public decimal CastTime { get; set; }
	}
}
