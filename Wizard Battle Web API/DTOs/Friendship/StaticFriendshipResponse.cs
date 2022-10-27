namespace Wizard_Battle_Web_API.DTOs.Friendship
{
	public class StaticFriendshipResponse
	{
		public int MainPlayerID { get; set; } = 0;

		public int FriendPlayerID { get; set; } = 0;

		public StaticPlayerResponse FriendPlayer { get; set; }
	}
}
