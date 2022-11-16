namespace Wizard_Battle_Web_API.Repositories
{
	public interface IFriendshipRepository
	{
		Task<List<Friendship>> GetAllById(int playerId);
		Task<Friendship> GetById(int playerId, int friendPlayerID);
		Task<Friendship> Create(Friendship friendship);
		Task<Friendship> Update(int requesterId, int addreeseeId);
		Task<Friendship> Delete(Friendship friendship);
	}

	public class FriendshipRepository : IFriendshipRepository
	{
		private readonly DatabaseContext _context;

		public FriendshipRepository(DatabaseContext context)
		{
			_context = context;
		}

		public async Task<List<Friendship>> GetAllById(int playerId)
		{
			return await _context.Friendship
				.Include(x => x.MainPlayer).ThenInclude(x => x.Icon)
				.Include(x => x.FriendPlayer).ThenInclude(x => x.Icon)
				.Where(x => x.MainPlayerID == playerId || x.FriendPlayerID == playerId)
				.ToListAsync();
		}

		public async Task<Friendship> GetById(int playerId, int friendId)
		{
			return await _context.Friendship
				.Include(x => x.MainPlayer).ThenInclude(x => x.Icon)
				.Include(x => x.FriendPlayer).ThenInclude(x => x.Icon)
				.FirstOrDefaultAsync(x => (x.MainPlayerID == playerId || x.MainPlayerID == friendId) && (x.FriendPlayerID == playerId || x.FriendPlayerID == friendId));
		}

		public async Task<Friendship> Create(Friendship friendship)
		{
			_context.Friendship.Add(friendship);
			await _context.SaveChangesAsync();
			return await GetById(friendship.MainPlayerID, friendship.FriendPlayerID);
		}

		public async Task<Friendship> Update(int mainPlayerId, int friendPlayerId)
		{
			Friendship friendship = await GetById(mainPlayerId, friendPlayerId);

			// If the id that was used to make the friend request is also used to accept the request, it will return
			if (friendship.MainPlayerID == mainPlayerId)
			{
				return friendship;
			}

			if (friendship != null)
			{
				friendship.IsPending = false;

				await _context.SaveChangesAsync();
			}
			return friendship;
		}

		public async Task<Friendship> Delete(Friendship friendship)
		{
			_context.Friendship.Remove(friendship);
			await _context.SaveChangesAsync();
			return friendship;
		}
	}
}
