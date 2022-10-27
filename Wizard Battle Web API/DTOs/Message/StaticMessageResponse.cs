namespace Wizard_Battle_Web_API.DTOs.Message
{
	public class StaticMessageResponse
	{
		public int SenderID { get; set; } = 0;

		public int ReceiverID { get; set; } = 0;
		
		public string Text { get; set; } = string.Empty;

		public DateTime Created_At { get; set; }
	}
}
