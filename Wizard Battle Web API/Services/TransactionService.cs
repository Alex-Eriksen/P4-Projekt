namespace Wizard_Battle_Web_API.Services
{
	public interface ITransactionService
	{
		public Task<List<StaticTransactionResponse>> GetAll();
		public Task<DirectTransactionResponse> GetById(int transactionId);
		public Task<DirectTransactionResponse> Create(TransactionRequest request);
		public Task<DirectTransactionResponse> Update(int transactionId, TransactionRequest request);
		public Task<DirectTransactionResponse> Delete(int transactionId);
	}
	public class TransactionService : ITransactionService
	{

		private readonly ITransactionRepository m_transactionRepository;
		private readonly IMapper m_mapper;


		public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
		{
			m_transactionRepository = transactionRepository;
			m_mapper = mapper;
		}


		public async Task<DirectTransactionResponse> Create(TransactionRequest request)
		{
			Transaction transaction = await m_transactionRepository.Create(m_mapper.Map<Transaction>(request));
			if(transaction != null)
			{
				return m_mapper.Map<DirectTransactionResponse>(transaction);
			}

			return null;
		}


		public async Task<DirectTransactionResponse> Delete(int transactionId)
		{
			Transaction transaction = await m_transactionRepository.GetById(transactionId);
			if(transaction != null)
			{
				return m_mapper.Map<DirectTransactionResponse>(await m_transactionRepository.Delete(transaction));
			}

			return null;
		}


		public async Task<List<StaticTransactionResponse>> GetAll()
		{
			List<Transaction> transactions = await m_transactionRepository.GetAll();
			if(transactions != null)
			{
				return transactions.Select(x => m_mapper.Map<StaticTransactionResponse>(x)).ToList();
			}

			return null;
		}


		public async Task<DirectTransactionResponse> GetById(int transactionId)
		{
			Transaction transaction = await m_transactionRepository.GetById(transactionId);
			if(transaction != null)
			{
				return m_mapper.Map<DirectTransactionResponse>(transaction);
			}

			return null;
		}


		public async Task<DirectTransactionResponse> Update(int transactionId, TransactionRequest request)
		{
			Transaction transaction = await m_transactionRepository.Update(transactionId, m_mapper.Map<Transaction>(request));
			if(transaction != null)
			{
				return m_mapper.Map<DirectTransactionResponse>(transaction);
			}

			return null;
		}
	}
}
