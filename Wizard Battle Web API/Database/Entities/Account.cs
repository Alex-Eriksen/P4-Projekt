namespace Wizard_Battle_Web_API.Database.Entities
{
	public class Account
	{
		[Key]
		public int AccountID { get; set; }

		[Column(TypeName = "nvarchar(64)")]
		public string Email { get; set; } = string.Empty;

		[Column(TypeName = "nvarchar(255)")]
		public string Password { get; set; } = string.Empty;

		[Column(TypeName = "datetime2")]
		public DateTime Created_At { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Modified_At { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Last_Login { get; set; }

		public Player Player { get; set; }

		public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
	}
}
