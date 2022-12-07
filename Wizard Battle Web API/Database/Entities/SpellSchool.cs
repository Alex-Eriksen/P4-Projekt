namespace Wizard_Battle_Web_API.Database.Entities
{
	public class SpellSchool
	{
		[Key]
		public int SpellSchoolID { get; set; }

		[Column(TypeName = "nvarchar(32)")]
		public string SpellSchoolName { get; set; }

		public virtual ICollection<SchoolCategory> SchoolCategories { get; set; }
	}
}
