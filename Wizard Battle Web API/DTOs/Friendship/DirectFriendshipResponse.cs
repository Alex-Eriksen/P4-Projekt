namespace Wizard_Battle_Web_API.DTOs.Friendship
{
	public class DirectFriendshipResponse
	{
		public StaticPlayerResponse MainPlayer { get; set; }

		public StaticPlayerResponse FriendPlayer { get; set; }

		public DateTime Created_At { get; set; }

		public bool IsPending { get; set; } = true;
	}
}
