namespace Wizard_Battle_Web_API.DTOs.SpellType
{
	public class SpellTypeRequest
	{
		[Required(ErrorMessage = "* is required")]
		public string SpellTypeName { get; set; }
	}
}
