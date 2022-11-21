namespace Wizard_Battle_Web_API.Repositories
{
	public interface ISpellBookSlotRepository
	{
		public Task<SpellBookSlot> GetById(int id);
		public Task<List<SpellBookSlot>> GetAllById(int id);
		public Task<SpellBookSlot> Create(SpellBookSlot spellBookSlot);
		public Task<List<SpellBookSlot>> Update(int spellBookId, List<int> newSpellIds);

	}
	public class SpellBookSlotRepository : ISpellBookSlotRepository
	{
		private readonly DatabaseContext m_context;

		public SpellBookSlotRepository(DatabaseContext context)
		{
			m_context = context;
		}

		public async Task<SpellBookSlot> GetById(int id)
		{
			return await m_context.SpellBookSlot.FirstOrDefaultAsync(x => x.SpellBookID == id);
		}

		public async Task<List<SpellBookSlot>> GetAllById(int id)
		{
			return await m_context.SpellBookSlot.Where(x => x.SpellBookID == id).ToListAsync();
		}

		public async Task<SpellBookSlot> Create(SpellBookSlot spellBookSlot)
		{
			await m_context.AddAsync(spellBookSlot);
			await m_context.SaveChangesAsync();
			return await GetById(spellBookSlot.SpellBookID);
		}

		public async Task<List<SpellBookSlot>> Update(int spellBookId, List<int> newSpellIds)
		{
			List<SpellBookSlot> spellBookSlots = await GetAllById(spellBookId);
			if(spellBookSlots != null)
			{
				for (int i = 0; i < newSpellIds.Count; i++)
				{
					if(spellBookSlots.Count < i || spellBookSlots.Count == 0)
					{
						await Create(new SpellBookSlot() { SpellBookID = spellBookId, SpellID = newSpellIds[i] });
					}
					else
					{
						spellBookSlots[i].SpellID = newSpellIds[i];
					}
				}

				await m_context.SaveChangesAsync();
			}

			return spellBookSlots;
		}
	}
}
