namespace Wizard_Battle_Web_API.DTOs.SpellBook
{
	public class SpellBookRequest
	{
		public string SpellBookName { get; set; }

		public int PlayerID { get; set; }

		public List<StaticSpellResponse> Spells { get; set; }
	}
}
