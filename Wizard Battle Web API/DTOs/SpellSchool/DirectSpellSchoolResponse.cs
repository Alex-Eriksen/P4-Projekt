namespace Wizard_Battle_Web_API.DTOs.SpellSchool
{
	public class DirectSpellSchoolResponse
	{
		public int SpellSchoolID { get; set; } = 0;

		public string SpellSchoolName { get; set; } = string.Empty;

		public List<StaticSchoolCategoryResponse> SchoolCategories = new List<StaticSchoolCategoryResponse>();
	}
}
