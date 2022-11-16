namespace Wizard_Battle_Web_API.Repositories
{
	public interface IPlayerRepository
	{
		Task<List<Player>> GetAll();
		Task<Player> GetById(int playerId);
		Task<Player> Create(Player request);
		Task<Player> Update(int playerId, Player request);
        Task<Player> ChangeStatus(int playerId, string status);
    }

	public class PlayerRepository : IPlayerRepository
	{
		private readonly DatabaseContext m_context;

		/// <summary>
		/// Constructor for PlayerRepository.
		/// </summary>
		/// <param name="context"></param>
		public PlayerRepository(DatabaseContext context)
		{
			m_context = context;
		}


        /// <summary>
        /// Gets all players
        /// </summary>
        /// <returns>List of Player</returns>
        public async Task<List<Player>> GetAll()
        {
            return await m_context.Player
                .Include(x => x.Account)
                .Include(x => x.Icon)
                .ToListAsync();
        }


        /// <summary>
        /// Creates a player.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>request.PlayerID</returns>
        public async Task<Player> Create(Player request)
		{
			m_context.Player.Add(request);
			await m_context.SaveChangesAsync();
			return await GetById(request.PlayerID);
		}


        /// <summary>
        /// Gets a player by its Id.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>CustomerID</returns>
        public async Task<Player> GetById(int playerId)
        {
            return await m_context.Player
                .Include(x => x.Account)
                .Include(x => x.Icon)
                .Include(x => x.Transactions)
                .FirstOrDefaultAsync(x => x.PlayerID == playerId);
        }


        /// <summary>
        /// Updates player.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="request"></param>
        /// <returns>Player</returns>
        public async Task<Player> Update(int playerId, Player request)
        {
            Player player = await GetById(playerId);
            if (player != null)
            {
                player.PlayerName = request.PlayerName;
                player.ExperiencePoints = request.ExperiencePoints;
                player.KnowledgePoints = request.KnowledgePoints;
                player.TimeCapsules = request.TimeCapsules;
                player.PlayerStatus = request.PlayerStatus;
                player.IconID = request.IconID;
                player.MaxHealth = request.MaxHealth;
                player.MaxMana = request.MaxMana;
                player.Modified_At = DateTime.UtcNow;

                await m_context.SaveChangesAsync();
            }

            return player;
        }

        public async Task<Player> ChangeStatus(int playerId, string status)
        {
            Player player = await GetById(playerId);
            if(player != null)
			{
                player.PlayerStatus = status;

                await m_context.SaveChangesAsync();
			}

            return player;
        }
    }
}
