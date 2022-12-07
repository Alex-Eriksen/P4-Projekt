namespace Wizard_Battle_Web_API.Repositories
{
	public interface ISpellRepository
	{
		Task<List<Spell>> GetAll();
		Task<Spell> GetById(int spellId);
		Task<Spell> Create(Spell request);
		Task<Spell> Update(int spellId, Spell request);
		Task<Spell> Delete(Spell spell);
	}

	public class SpellRepository : ISpellRepository
	{
		private readonly DatabaseContext m_context;


		public SpellRepository(DatabaseContext context)
		{
			m_context = context;
		}


		public async Task<List<Spell>> GetAll()
		{
			return await m_context.Spell
				.Include(x => x.Icon)
				.ToListAsync();
		}


		public async Task<Spell> GetById(int spellId)
		{
			return await m_context.Spell
				.Include(x => x.Icon)
				.Include(x => x.SpellType)
				.Include(x => x.SchoolCategory)
				.ThenInclude(x => x.SpellSchool)
				.FirstOrDefaultAsync(x => x.SpellID == spellId);
		}


		public async Task<Spell> Create(Spell request)
		{
			await m_context.Spell.AddAsync(request);
			await m_context.SaveChangesAsync();
			return await GetById(request.SpellID);
		}


		public async Task<Spell> Update(int spellId, Spell request)
		{
			Spell spell = await GetById(spellId);
			if(spell != null)
			{
				spell.SpellName = request.SpellName;
				spell.SpellDescription = request.SpellDescription;
				spell.IconID = request.IconID;
				spell.SpellTypeID = request.SpellTypeID;
				spell.SchoolCategoryID = request.SchoolCategoryID;
				spell.DamageAmount = request.DamageAmount;
				spell.ManaCost = request.ManaCost;
				spell.LifeTime = request.LifeTime;
				spell.CastTime = request.CastTime;

				await m_context.SaveChangesAsync();
			} 

			return await GetById(spellId);
		}


		public async Task<Spell> Delete(Spell spell)
		{
			m_context.Spell.Remove(spell);
			await m_context.SaveChangesAsync();
			return spell;
		}
	}
}
