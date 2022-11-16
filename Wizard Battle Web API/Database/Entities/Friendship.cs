namespace Wizard_Battle_Web_API.Database.Entities
{
	public class Friendship
	{
		public int MainPlayerID { get; set; }
		public virtual Player MainPlayer { get; set; }

		public int FriendPlayerID { get; set; }
		public virtual Player FriendPlayer { get; set; }

		[Column(TypeName = "datetime2")]
		public DateTime Created_At { get; set; }

		[DefaultValue(true)]
		public bool IsPending { get; set; } = true;
	}
}
