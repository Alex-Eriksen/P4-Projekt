namespace Wizard_Battle_Web_API.DTOs.SpellBook
{
	public class StaticSpellBookResponse
	{
		public int SpellBookID { get; set; } = 0;
		
		public string SpellBookName { get; set; } = string.Empty;

		public int PlayerID { get; set; } = 0;
	}
}
