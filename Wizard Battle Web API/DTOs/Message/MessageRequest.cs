namespace Wizard_Battle_Web_API.DTOs.Message
{
	public class MessageRequest
	{
		public int SenderID { get; set; } = 0;

		public int ReceiverID { get; set; } = 0;

		public string Text { get; set; } = string.Empty;
	}
}
