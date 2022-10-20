namespace Wizard_Battle_Web_API.Repositories
{
	public interface IAccountRepository
	{
		Task<List<Account>> GetAll();
		Task<Account> GetById(int accountId);
		Task<Account> Create(Account request);
		Task<Account> Update(int accountId, Account request);
	}

	public class AccountRepository : IAccountRepository
	{
		private readonly DatabaseContext m_context;
		private readonly AppSettings m_appSettings;

		/// <summary>
		/// Constructor for AccountRepository.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="appSettings"></param>
		public AccountRepository(DatabaseContext context, IOptions<AppSettings> appSettings)
		{
			m_appSettings = appSettings.Value;
			m_context = context;
		}


		/// <summary>
		/// Gets all Accounts
		/// </summary>
		/// <returns>List of Accounts</returns>
		public async Task<List<Account>> GetAll()
		{
			return await m_context.Account
				.Include(x => x.Player)
				.ToListAsync();
		}


		/// <summary>
		/// Creates an Account in the database.
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Account</returns>
		public async Task<Account> Create(Account request)
		{
			request.Password = BC.HashPassword(request.Password);
			m_context.Account.Add(request);
			await m_context.SaveChangesAsync();
			return await GetById(request.AccountID);
		}


		/// <summary>
		/// Gets an Account by its AccountID
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns>Account</returns>
		public async Task<Account> GetById(int accountId)
		{
			return await m_context.Account.Include(x => x.Player).FirstOrDefaultAsync(x => x.AccountID == accountId);
		}


		/// <summary>
		/// Updates password on Account
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns>Account</returns>
		public async Task<Account> Update(int accountId, Account request)
		{
			Account account = await GetById(accountId);
			if (account != null)
			{
				request.Password = BC.HashPassword(request.Password);
				account.Modified_At = DateTime.UtcNow;

				await m_context.SaveChangesAsync();
			}
			return account;
		}
	}
}
