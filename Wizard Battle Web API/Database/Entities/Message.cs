namespace Wizard_Battle_Web_API.Database.Entities
{
	public class Message
	{
		[Key]
		public int MessageID { get; set; }

		public int SenderID  { get; set; }
		public virtual Player Sender { get; set; }

		public int ReceiverID { get; set; }
		public virtual Player Receiver { get; set; }

		[Column(TypeName = "nvarchar(255)")]
		public string Text { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Created_At { get; set; }

	}
}
