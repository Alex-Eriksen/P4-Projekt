namespace Wizard_Battle_Web_API.DTOs.SkinItem
{
	public class StaticSkinItemResponse
	{
		public int SkinID { get; set; } = 0;

		public string SkinName { get; set; } = string.Empty;

		public string SkinDescription { get; set; } = string.Empty;

		public int SkinPrice { get; set; } = 0;

		public string ImageName { get; set; } = string.Empty;
	}
}
