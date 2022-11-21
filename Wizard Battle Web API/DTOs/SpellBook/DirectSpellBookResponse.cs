namespace Wizard_Battle_Web_API.DTOs.SpellBook
{
	public class DirectSpellBookResponse
	{
		public int SpellBookID { get; set; } = 0;

		public string SpellBookName { get; set; } = string.Empty;

		public StaticPlayerResponse Player { get; set; } = null!;

		public List<StaticSpellResponse> Spells { get; set; }
	}
}
