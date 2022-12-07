namespace Wizard_Battle_Web_API.DTOs.SpellType
{
	public class DirectSpellTypeResponse
	{
		public int SpellTypeID { get; set; } = 0;

		public string SpellTypeName { get; set; } = string.Empty;

		public ICollection<StaticSpellResponse> Spells { get; set; } = new List<StaticSpellResponse>();
	}
}
