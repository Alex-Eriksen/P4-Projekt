namespace Wizard_Battle_Web_API.Repositories
{
	public interface ITransactionRepository
	{
		public Task<List<Transaction>> GetAll();
		public Task<Transaction> GetById(int transactionId);
		public Task<Transaction> Create(Transaction request);
		public Task<Transaction> Update(int transactionId, Transaction request);
		public Task<Transaction> Delete(Transaction transaction);
	}
	public class TransactionRepository : ITransactionRepository
	{
		private readonly DatabaseContext m_databaseContext;


		public TransactionRepository(DatabaseContext context)
		{
			m_databaseContext = context;
		}


		public async Task<List<Transaction>> GetAll()
		{
			return await m_databaseContext.Transaction
				.Include(x => x.Player)
				.Include(x => x.SkinItem)
				.ToListAsync();
		}


		public async Task<Transaction> GetById(int transactionId)
		{
			return await m_databaseContext.Transaction
				.Include(x => x.Player).ThenInclude(x => x.Icon)
				.Include(x => x.SkinItem)
				.FirstOrDefaultAsync(x => x.TransactionID == transactionId);
		}


		public async Task<Transaction> Create(Transaction request)
		{
			await m_databaseContext.Transaction.AddAsync(request);
			await m_databaseContext.SaveChangesAsync();
			return await GetById(request.TransactionID);
		}


		public async Task<Transaction> Update(int transactionId, Transaction request)
		{
			Transaction transaction = await GetById(transactionId);
			if(transaction != null)
			{
				transaction.PlayerID = request.PlayerID;
				transaction.SkinID = request.SkinID;
				transaction.TotalCost = request.TotalCost;

				await m_databaseContext.SaveChangesAsync();
			}

			return await GetById(transactionId);
		}


		public async Task<Transaction> Delete(Transaction transaction)
		{
			m_databaseContext.Transaction.Remove(transaction);
			await m_databaseContext.SaveChangesAsync();
			return transaction;
		}
	}
}
