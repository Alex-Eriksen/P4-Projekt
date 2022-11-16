namespace Wizard_Battle_Web_API.Services
{
	public interface IFriendshipService
	{
		Task<List<StaticFriendshipResponse>> GetAllById(int playerId);
		Task<DirectFriendshipResponse> GetById(FriendshipRequest request);
		Task<DirectFriendshipResponse> Create(FriendshipRequest request);
		Task<DirectFriendshipResponse> Delete(FriendshipRequest request);
		Task<DirectFriendshipResponse> Update(FriendshipRequest request);
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

		public async Task<List<StaticFriendshipResponse>> GetAllById(int playerId)
		{
			List<Friendship> friendships = await m_friendshipRepository.GetAllById(playerId);
			if (friendships != null)
			{
				List<Friendship> friendshipsToSwap = friendships.Where(x => x.MainPlayerID != playerId).ToList();
				foreach(Friendship friendship in friendshipsToSwap)
				{
					Friendship tempFriendship = new Friendship();
					tempFriendship.MainPlayerID = friendship.FriendPlayerID;
					tempFriendship.MainPlayer = friendship.FriendPlayer;

					tempFriendship.FriendPlayer = friendship.MainPlayer;
					tempFriendship.FriendPlayerID = friendship.MainPlayerID;

					tempFriendship.Created_At = friendship.Created_At;
					tempFriendship.IsPending = friendship.IsPending;
					friendships.Add(tempFriendship);
				}
				return friendships.Where(x => x.FriendPlayerID != playerId).Select(x => m_mapper.Map<Friendship, StaticFriendshipResponse>(x)).ToList();
			}

			return null;
		}

		public async Task<DirectFriendshipResponse> GetById(FriendshipRequest request)
		{
			Friendship friendship = await m_friendshipRepository.GetById(request.MainPlayerID, request.FriendPlayerID);
			if (friendship != null)
			{
				return m_mapper.Map<DirectFriendshipResponse>(friendship);
			}

			return null;
		}

		public async Task<DirectFriendshipResponse> Create(FriendshipRequest request)
		{
			Friendship friendship = await m_friendshipRepository.Create(m_mapper.Map<Friendship>(request));
			if(friendship != null)
			{
				return m_mapper.Map<DirectFriendshipResponse>(friendship);
			}

			return null;
		}

		public async Task<DirectFriendshipResponse> Update(FriendshipRequest request)
		{
			Friendship friendship = await m_friendshipRepository.Update(request.MainPlayerID, request.FriendPlayerID);
			if (friendship != null)
			{
				return m_mapper.Map<DirectFriendshipResponse>(friendship);
			}

			return null;
		}

		public async Task<DirectFriendshipResponse> Delete(FriendshipRequest request)
		{
			Friendship friendship = await m_friendshipRepository.GetById(request.MainPlayerID, request.FriendPlayerID);
			if (friendship != null)
			{
				Friendship deletedFriendship = await m_friendshipRepository.Delete(friendship);
				return m_mapper.Map<DirectFriendshipResponse>(deletedFriendship);
			}

			return null;
		}
	}
}
