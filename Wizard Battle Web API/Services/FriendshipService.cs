namespace Wizard_Battle_Web_API.Services
{
	public interface IFriendshipService
	{
		Task<List<StaticPlayerResponse>> GetAllFriendship(int playerId);
		Task<DirectFriendshipResponse> GetFriendship(FriendshipRequest request);
		Task<DirectFriendshipResponse> AddFriend(FriendshipRequest request);
		Task<DirectFriendshipResponse> RemoveFriend(FriendshipRequest request);
		Task<DirectFriendshipResponse> AcceptFriend(FriendshipRequest request);
		Task<StaticMessageResponse> SendMessage(MessageRequest request);
		Task<List<StaticMessageResponse>> GetAllMessages(FriendshipRequest request);
	}
	public class FriendshipService : IFriendshipService
	{
		private readonly IFriendshipRepository m_friendshipRepository;
		private readonly IMapper m_mapper;

		public FriendshipService(IFriendshipRepository friendshipRepository, IMapper mapper) 
		{
			m_friendshipRepository = friendshipRepository;
			m_mapper = mapper;
		}

		public async Task<DirectFriendshipResponse> AcceptFriend(FriendshipRequest request)
		{
			Friendship friendship = await m_friendshipRepository.AcceptFriend(request.MainPlayerID, request.FriendPlayerID);
			if(friendship != null)
			{
				return m_mapper.Map<DirectFriendshipResponse>(friendship);
			}

			return null;
		}

		public async Task<DirectFriendshipResponse> AddFriend(FriendshipRequest request)
		{
			Friendship friendship = await m_friendshipRepository.AddFriend(m_mapper.Map<Friendship>(request));
			if(friendship != null)
			{
				return m_mapper.Map<DirectFriendshipResponse>(friendship);
			}

			return null;
		}

		public async Task<List<StaticPlayerResponse>> GetAllFriendship(int playerId)
		{
			List<Friendship> friendships = await m_friendshipRepository.GetAllFriendships(playerId);
			if(friendships != null)
			{
				List<StaticPlayerResponse> friends = new List<StaticPlayerResponse>();
				for(int i = 0; i < friendships.Count; i++)
				{
					// if player doesnt match client playerId, push friend into List
					if (friendships[i].MainPlayerID != playerId)
					{
						friends.Add(m_mapper.Map<StaticPlayerResponse>(friendships[i].MainPlayer));
					}

					// if player doesnt match client playerId, push friend into List
					if (friendships[i].FriendPlayerID != playerId)
					{
						friends.Add(m_mapper.Map<StaticPlayerResponse>(friendships[i].FriendPlayer));
					}
				}
				return friends;
			}

			return null;
		}

		public async Task<DirectFriendshipResponse> GetFriendship(FriendshipRequest request)
		{
			Friendship friendship = await m_friendshipRepository.GetFriendship(request.MainPlayerID, request.FriendPlayerID);
			if(friendship != null)
			{
				return m_mapper.Map<DirectFriendshipResponse>(friendship);
			}

			return null;
		}

		public async Task<DirectFriendshipResponse> RemoveFriend(FriendshipRequest request)
		{
			Friendship friendship = await m_friendshipRepository.GetFriendship(request.MainPlayerID, request.FriendPlayerID);
			if (friendship != null)
			{
				Friendship deletedFriendship = await m_friendshipRepository.RemoveFriend(friendship);
				return m_mapper.Map<DirectFriendshipResponse>(deletedFriendship);
			}

			return null;
		}

		public async Task<StaticMessageResponse> SendMessage(MessageRequest request)
		{
			Message message = await m_friendshipRepository.SendMessage(m_mapper.Map<Message>(request));
			if (message != null)
			{
				return m_mapper.Map<StaticMessageResponse>(message);
			}

			return null;
		}

		public async Task<List<StaticMessageResponse>> GetAllMessages(FriendshipRequest request)
		{
			List<Message> messages = await m_friendshipRepository.GetMessages(m_mapper.Map<Friendship>(request));
			if(messages != null)
			{
				return messages.Select(message => m_mapper.Map<StaticMessageResponse>(message)).ToList();
			}

			return null;
		}
	}
}
