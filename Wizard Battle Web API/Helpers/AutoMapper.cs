namespace Wizard_Battle_Web_API.Helpers
{
	public class AutoMapper : Profile
	{
		public AutoMapper()
		{
			CreateMap<Account, DirectAccountResponse>();
			CreateMap<Account, StaticAccountResponse>();
			CreateMap<AccountRequest, Account>();

			CreateMap<Player, DirectPlayerResponse>();
			CreateMap<Player, StaticPlayerResponse>();
			CreateMap<PlayerRequest, Player>();

			CreateMap<Friendship, DirectFriendshipResponse>();
			CreateMap<Friendship, StaticFriendshipResponse>();
			CreateMap<FriendshipRequest, Friendship>();

			CreateMap<MessageRequest, Message>();
			CreateMap<Message, StaticMessageResponse>();

			CreateMap<AuthenticationResponse, StaticRefreshTokenResponse>();

			CreateMap<IconRequest, Icon>();
		}
	}
}
