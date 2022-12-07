namespace Wizard_Battle_Web_API.DTOs.SchoolCategory
{
	public class DirectSchoolCategoryResponse
	{
		public int SchoolCategoryID { get; set; } = 0;

		public string SchoolCategoryName { get; set; } = string.Empty;

		public StaticSpellSchoolResponse SpellSchool { get; set; } = null!;
	}
}
