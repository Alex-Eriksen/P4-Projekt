namespace Wizard_Battle_Web_API.Database.Entities
{
	public class Icon
	{
		[Key]
		public int IconID { get; set; }

		[Column(TypeName = "nvarchar(255)")]
		public string IconLocation { get; set; }
	}
}
