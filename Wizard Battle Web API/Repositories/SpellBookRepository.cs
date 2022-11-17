namespace Wizard_Battle_Web_API.Repositories
{

	public interface ISpellBookRepository
	{
		Task<List<SpellBook>> GetAll();
		Task<SpellBook> GetById(int spellBookId);
		Task<SpellBook> Create(SpellBook request);
		Task<SpellBook> Update(int spellBookId, SpellBook request);
		Task<SpellBook> Delete(SpellBook spellBook);
	}

	public class SpellBookRepository : ISpellBookRepository
	{
		private readonly DatabaseContext m_context;


		public SpellBookRepository(DatabaseContext context)
		{
			m_context = context;
		}


		public async Task<List<SpellBook>> GetAll()
		{
			return await m_context.SpellBook
				.ToListAsync();
		}


		public async Task<SpellBook> GetById(int spellBookId)
		{
			return await m_context.SpellBook
				.Include(x => x.Player)
				.Include(x => x.SpellBookSlots)
				.ThenInclude(x => x.Spell)
				.FirstOrDefaultAsync(x => x.SpellBookID == spellBookId);
		}


		public async Task<SpellBook> Create(SpellBook request)
		{
			await m_context.SpellBook.AddAsync(request);
			await m_context.SaveChangesAsync();
			return await GetById(request.SpellBookID);
		}

		public async Task<SpellBook> Update(int spellBookId, SpellBook request)
		{
			SpellBook spellBook = await GetById(spellBookId);
			if (spellBook != null)
			{
				spellBook.SpellBookName = request.SpellBookName;
				spellBook.SpellBookSlots = request.SpellBookSlots;

				await m_context.SaveChangesAsync();
			}

			return spellBook;
		}


		public async Task<SpellBook> Delete(SpellBook spellBook)
		{
			m_context.SpellBook.Remove(spellBook);
			await m_context.SaveChangesAsync();
			return spellBook;
		}
	}
}
