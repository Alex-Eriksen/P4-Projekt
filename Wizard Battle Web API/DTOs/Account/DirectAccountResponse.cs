namespace Wizard_Battle_Web_API.DTOs.Account
{
	public class DirectAccountResponse
	{
		public int AccountID { get; set; } = 0;

		public string Email { get; set; } = string.Empty;

		public StaticPlayerResponse Player { get; set; } = null!;
	}
}
