﻿namespace Wizard_Battle_Web_API.DTOs.Spell
{
	public class StaticSpellResponse
	{
		public int SpellID { get; set; } = 0;

		public string SpellName { get; set; } = string.Empty;

		public string SpellDescription { get; set; } = string.Empty;

		public IconResponse Icon { get; set; } = null!;
		
		public int SpellTypeID { get; set; } = 0;

		public int SchoolCategoryID { get; set; } = 0;
		
		public decimal? DamageAmount { get; set; } = 0;

		public decimal ManaCost { get; set; } = 0;

		public decimal LifeTime { get; set; } = 0;

		public decimal CastTime { get; set; } = 0;
	}
}
