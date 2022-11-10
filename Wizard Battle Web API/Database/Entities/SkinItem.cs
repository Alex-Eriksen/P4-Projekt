namespace Wizard_Battle_Web_API.Database.Entities
{
	public class SkinItem
	{
		[Key]
		public int SkinID { get; set; }

		[Column(TypeName = "nvarchar(60)")]
		public string SkinName { get; set; }

		[Column(TypeName = "nvarchar(255)")]
		public string SkinDescription { get; set; }

		[Column(TypeName = "nvarchar(255)")]
		public string ImageName { get; set; }
	}
}
