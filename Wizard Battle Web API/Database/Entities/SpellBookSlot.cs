namespace Wizard_Battle_Web_API.Database.Entities
{
	public class SpellBookSlot
	{
		public int SpellBookID { get; set; }
		public SpellBook SpellBook { get; set; }

		public int SpellID { get; set; }
		public Spell Spell { get; set; }
	}
}
