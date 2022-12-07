namespace Wizard_Battle_Web_API.Database.Entities
{
	public class SpellType
	{
		[Key]
		public int SpellTypeID { get; set; }

		[Column(TypeName = "nvarchar(32)")]
		public string SpellTypeName { get; set; }
	}
}
