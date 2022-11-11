namespace Wizard_Battle_Web_API.Services
{
	public interface IAccountService
	{
		Task<List<StaticAccountResponse>> GetAll();
		Task<DirectAccountResponse> GetById(int accountId);
		Task<DirectAccountResponse> Update(int accountId, AccountRequest request);

		Task<DirectAccountResponse> GetByToken(string Token);
	}

	public class AccountService : IAccountService
	{
		private readonly IAccountRepository m_accountRepository;
		private readonly IMapper m_mapper;


		/// <summary>
		/// Constructor of AccountService.
		/// </summary>
		/// <param name="accountRepository"></param>
		/// <param name="mapper"></param>
		public AccountService(IAccountRepository accountRepository, IMapper mapper) 
		{
			m_accountRepository = accountRepository;
			m_mapper = mapper;
		}


		/// <summary>
		/// Gets all Accounts.
		/// </summary>
		/// <returns>List of Accounts or null</returns>
		public async Task<List<StaticAccountResponse>> GetAll()
		{
			List<Account> accounts = await m_accountRepository.GetAll();
			if (accounts != null)
			{
				return accounts.Select(account => m_mapper.Map<Account, StaticAccountResponse>(account)).ToList();
			}

			return null;
		}


		/// <summary>
		/// Gets a Account by its id.
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns>Account or null</returns>
		public async Task<DirectAccountResponse> GetById(int accountId)
		{
			Account account = await m_accountRepository.GetById(accountId);

			if (account != null)
			{
				return m_mapper.Map<DirectAccountResponse>(account);
			}

			return null;
		}

        public async Task<DirectAccountResponse> GetByToken(string Token)
        {
			var Id = Convert.ToInt32(JWTHandler.GetAccountIdByToken(Token));
			Account account = await m_accountRepository.GetById(Id);
			if (account != null)
			{
				return m_mapper.Map<DirectAccountResponse>(account);
			}

			return null;
		}


        /// <summary>
        /// Updates a account.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="request"></param>
        /// <returns>player or null</returns>
        public async Task<DirectAccountResponse> Update(int accountId, AccountRequest request)
		{
			Account account = await m_accountRepository.Update(accountId, m_mapper.Map<Account>(request));
			if (account != null)
			{
				return m_mapper.Map<DirectAccountResponse>(account);
			}

			return null;
		}
	}
}
