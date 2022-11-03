namespace Wizard_Battle_Web_API.Repositories
{
	public interface IFriendshipRepository
	{
		Task<List<Friendship>> GetAllFriendships(int playerId);
		Task<Friendship> GetFriendship(int playerId, int friendPlayerID);
		Task<Friendship> AddFriend(Friendship friendship);
		Task<Friendship> RemoveFriend(Friendship friendship);
		Task<Friendship> AcceptFriend(int requesterId, int addreeseeId);
		Task<Message> SendMessage(Message message);
		Task<List<Message>> GetMessages(Friendship friendship);
	}

	public class FriendshipRepository : IFriendshipRepository
	{
		private readonly DatabaseContext _context;

		public FriendshipRepository(DatabaseContext context)
		{
			_context = context;
		}

		public async Task<Friendship> GetFriendship(int playerId, int friendId)
		{
			return await _context.Friendship
				.Include(x => x.MainPlayer).ThenInclude(x => x.Icon)
				.Include(x => x.FriendPlayer).ThenInclude(x => x.Icon)
				.FirstOrDefaultAsync(x => (x.MainPlayerID == playerId || x.MainPlayerID == friendId) && (x.FriendPlayerID == playerId || x.FriendPlayerID == friendId));
		}

		public async Task<List<Friendship>> GetAllFriendships(int playerId)
		{
			return await _context.Friendship
				.Include(x => x.MainPlayer).ThenInclude(x => x.Icon)
				.Include(x => x.FriendPlayer).ThenInclude(x => x.Icon)
				.Where(x => (x.MainPlayerID == playerId || x.FriendPlayerID == playerId) && (x.IsPending == false))
				.ToListAsync();
		}

		public async Task<Friendship> AcceptFriend(int mainPlayerId, int friendPlayerId)
		{
			Friendship friendship = await GetFriendship(mainPlayerId, friendPlayerId);

			// If the id that was used to make the friend request is used to accept it will return
			if(friendship.MainPlayerID == mainPlayerId)
			{
				return friendship;
			}

			if(friendship != null)
			{
				friendship.IsPending = false;

				await _context.SaveChangesAsync();
			}
			return friendship;
		}

		public async Task<Friendship> AddFriend(Friendship friendship)
		{
			_context.Friendship.Add(friendship);
			await _context.SaveChangesAsync();
			return await GetFriendship(friendship.MainPlayerID, friendship.FriendPlayerID);
		}

		public async Task<Friendship> RemoveFriend(Friendship friendship)
		{
			_context.Friendship.Remove(friendship);
			await _context.SaveChangesAsync();
			return friendship;
		}

		public async Task<Message> SendMessage(Message message)
		{
			_context.Message.Add(message);
			await _context.SaveChangesAsync();
			return message;
		}

		public async Task<List<Message>> GetMessages(Friendship friendship)
		{
			return await _context.Message.Where(x => (x.SenderID == friendship.MainPlayerID || x.SenderID == friendship.FriendPlayerID) 
			&& (x.ReceiverID == friendship.MainPlayerID || x.ReceiverID == friendship.FriendPlayerID)).ToListAsync();
		}
	}
}
