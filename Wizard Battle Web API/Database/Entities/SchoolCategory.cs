namespace Wizard_Battle_Web_API.Database.Entities
{
	public class SchoolCategory
	{
		[Key]
		public int SchoolCategoryID { get; set; }

		[Column(TypeName = "nvarchar(32)")]
		public string SchoolCategoryName { get; set; }

		[ForeignKey("SpellSchoolID")]
		public int SpellSchoolID { get; set; }
		public SpellSchool SpellSchool { get; set; }
	}
}
