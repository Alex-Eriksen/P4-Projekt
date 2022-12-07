namespace Wizard_Battle_Web_API.DTOs.Spell
{
	public class DirectSpellResponse
	{
		public int SpellID { get; set; } = 0;

		public string SpellName { get; set; } = string.Empty;

		public string SpellDescription { get; set; } = string.Empty;

		public IconResponse Icon { get; set; } = null!;

		public StaticSpellTypeResponse SpellType { get; set; } = null!;

		public StaticSchoolCategoryResponse SchoolCategory { get; set; } = null!;

		public decimal? DamageAmount { get; set; } = 0;

		public decimal ManaCost { get; set; } = 0;

		public decimal LifeTime { get; set; } = 0;

		public decimal CastTime { get; set; } = 0;
	}
}
