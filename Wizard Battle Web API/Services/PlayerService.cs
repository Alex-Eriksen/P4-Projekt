namespace Wizard_Battle_Web_API.Services
{
	public interface IPlayerService
	{
		Task<List<StaticPlayerResponse>> GetAll();
		Task<DirectPlayerResponse> Create(PlayerAccountRequest request);
		Task<DirectPlayerResponse> GetById(int playerId);
		Task<DirectPlayerResponse> Update(int playerId, PlayerRequest request);
		Task<DirectPlayerResponse> ChangeStatus(int playerId, string status);
		Task<List<Icon>> GetAllIcons();
	}
	public class PlayerService : IPlayerService
	{
		private readonly IPlayerRepository m_playerRepository;
		private readonly IAccountRepository m_accountRepository;
		private readonly IMapper m_mapper;


		/// <summary>
		/// Constructor of PlayerService.
		/// </summary>
		/// <param name="playerRepository"></param>
		/// <param name="accountRepository"></param>
		/// <param name="mapper"></param>
		public PlayerService(IPlayerRepository playerRepository, IAccountRepository accountRepository, IMapper mapper)
		{
			m_playerRepository = playerRepository;
			m_accountRepository = accountRepository;
			m_mapper = mapper;
		}


		/// <summary>
		/// Gets all players.
		/// </summary>
		/// <returns>player or null</returns>
		public async Task<List<StaticPlayerResponse>> GetAll()
		{
			List<Player> players = await m_playerRepository.GetAll();
			if (players != null)
			{
				return players.Select(player => m_mapper.Map<Player, StaticPlayerResponse>(player)).ToList();
			}

			return null;
		}


		/// <summary>
		/// Creates a request of PlayerAccountRequest.
		/// </summary>
		/// <param name="request"></param>
		/// <returns>player or null</returns>
		public async Task<DirectPlayerResponse> Create(PlayerAccountRequest request)
		{
			Account account = await m_accountRepository.Create(m_mapper.Map<Account>(request.Account));
			if (account == null)
			{
				return null;
			}

			request.Player.AccountID = account.AccountID;

			Player player = await m_playerRepository.Create(m_mapper.Map<Player>(request.Player));
			if (player != null)
			{
				return m_mapper.Map<DirectPlayerResponse>(player);
			}

			return null;
		}


		/// <summary>
		/// Gets a player by its id.
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns>player or null</returns>
		public async Task<DirectPlayerResponse> GetById(int playerId)
		{
			Player player = await m_playerRepository.GetById(playerId);

			if (player != null)
			{
				return m_mapper.Map<DirectPlayerResponse>(player);
			}

			return null;
		}


		/// <summary>
		/// Updates a player.
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="request"></param>
		/// <returns>player or null</returns>
		public async Task<DirectPlayerResponse> Update(int playerId, PlayerRequest request)
		{
			Player player = await m_playerRepository.Update(playerId, m_mapper.Map<Player>(request));
			if (player != null)
			{
				return m_mapper.Map<DirectPlayerResponse>(player);
			}

			return null;
		}

		public async Task<DirectPlayerResponse> ChangeStatus(int playerId, string status)
		{
			Player player = await m_playerRepository.ChangeStatus(playerId, status);
			if (player != null)
			{
				return m_mapper.Map<DirectPlayerResponse>(player);
			}

			return null;
		}

		public async Task<List<Icon>> GetAllIcons()
		{
			List<Icon> icons = await m_playerRepository.GetAllIcons();
			if(icons != null)
			{
				return icons;
			}

			return null;
		}
	}
}
