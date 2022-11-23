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
		private readonly ISpellBookSlotRepository m_spellBookSlotRepository;
		private readonly IMapper m_mapper;


		public SpellBookService(ISpellBookRepository spellRepository, ISpellBookSlotRepository spellBookSlotRepository, IMapper mapper)
		{
			m_spellBookRepository = spellRepository;
			m_spellBookSlotRepository = spellBookSlotRepository;
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
				DirectSpellBookResponse orderedSpellBook = m_mapper.Map<DirectSpellBookResponse>(spellBook);
				if(spellBook.SpellOrder != null)
				{
					int[] orderedIds = spellBook.SpellOrder.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
					orderedSpellBook.Spells = orderedSpellBook.Spells.OrderBy(spell => Array.IndexOf(orderedIds, spell.SpellID)).ToList();
				}
				return orderedSpellBook;
			}

			return null;
		}


		public async Task<DirectSpellBookResponse> Create(SpellBookRequest request)
		{
			SpellBook spellBook = await m_spellBookRepository.Create(m_mapper.Map<SpellBook>(request));
			if (spellBook != null)
			{
				List<SpellBookSlot> slots = new List<SpellBookSlot>();
				foreach (int id in request.SpellIDs)
				{
					slots.Add(await m_spellBookSlotRepository.Create(new SpellBookSlot() { SpellBookID = spellBook.SpellBookID, SpellID = id}));
				}
				if(slots.Count == request.SpellIDs.Count)
				{
					return m_mapper.Map<DirectSpellBookResponse>(await m_spellBookRepository.GetById(spellBook.SpellBookID));
				} 
			}

			return null;
		}


		public async Task<DirectSpellBookResponse> Update(int spellBookId, SpellBookRequest request)
		{
			SpellBook spellBook = await m_spellBookRepository.Update(spellBookId, m_mapper.Map<SpellBook>(request));
			if (spellBook != null)
			{
				List<SpellBookSlot> updatedSlots = await m_spellBookSlotRepository.Update(spellBookId, request.SpellIDs);
				if (updatedSlots != null)
				{
					return m_mapper.Map<DirectSpellBookResponse>(await m_spellBookRepository.GetById(spellBook.SpellBookID));
				}
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
