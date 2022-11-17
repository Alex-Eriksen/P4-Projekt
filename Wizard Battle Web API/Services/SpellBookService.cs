namespace Wizard_Battle_Web_API.Services
{
	public interface ISpellBookService
	{
		Task<List<StaticSpellBookResponse>> GetAll();
		Task<DirectSpellBookResponse> GetById(int spellBookId);
		Task<DirectSpellBookResponse> Create(SpellBookRequest request);
		Task<DirectSpellBookResponse> Update(int spellBookId, SpellBookRequest request);
		Task<DirectSpellBookResponse> Delete(int spellBookId);
	}
	public class SpellBookService : ISpellBookService
	{
		private readonly ISpellBookRepository m_spellBookRepository;
		private readonly IMapper m_mapper;


		public SpellBookService(ISpellBookRepository spellRepository, IMapper mapper)
		{
			m_spellBookRepository = spellRepository;
			m_mapper = mapper;
		}


		public async Task<List<StaticSpellBookResponse>> GetAll()
		{
			List<SpellBook> spellBooks = await m_spellBookRepository.GetAll();
			if (spellBooks != null)
			{
				return spellBooks.Select(spell => m_mapper.Map<SpellBook, StaticSpellBookResponse>(spell)).ToList();
			}

			return null;
		}


		public async Task<DirectSpellBookResponse> GetById(int spellBookId)
		{
			SpellBook spellBook = await m_spellBookRepository.GetById(spellBookId);
			if (spellBook != null)
			{
				return m_mapper.Map<DirectSpellBookResponse>(spellBook);
			}

			return null;
		}


		public async Task<DirectSpellBookResponse> Create(SpellBookRequest request)
		{
			SpellBook spellBook = await m_spellBookRepository.Create(m_mapper.Map<SpellBook>(request));
			if (spellBook != null)
			{
				return m_mapper.Map<DirectSpellBookResponse>(spellBook);
			}

			return null;
		}


		public async Task<DirectSpellBookResponse> Update(int spellBookId, SpellBookRequest request)
		{
			SpellBook spellBook = await m_spellBookRepository.Update(spellBookId, m_mapper.Map<SpellBook>(request));
			if (spellBook != null)
			{
				return m_mapper.Map<DirectSpellBookResponse>(spellBook);
			}

			return null;
		}


		public async Task<DirectSpellBookResponse> Delete(int spellBookId)
		{
			SpellBook spellBook = await m_spellBookRepository.GetById(spellBookId);
			if (spellBook != null)
			{
				return m_mapper.Map<DirectSpellBookResponse>(await m_spellBookRepository.Delete(spellBook));
			}

			return null;
		}
	}
}
