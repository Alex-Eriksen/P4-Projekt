namespace Wizard_Battle_Web_API.Repositories
{
	public interface ISpellSchoolRepository
	{
		public Task<List<SpellSchool>> GetAll();
		public Task<SpellSchool> GetById(int id);
		public Task<SpellSchool> Create(SpellSchool school);
		public Task<SpellSchool> Update(int id, SpellSchool school);
		public Task<SpellSchool> Delete(SpellSchool school);
	}
	public class SpellSchoolRepository : ISpellSchoolRepository
	{
		private readonly DatabaseContext m_context;


		public SpellSchoolRepository(DatabaseContext context)
		{
			m_context = context;
		}


		public async Task<List<SpellSchool>> GetAll()
		{
			return await m_context.SpellSchool
				.ToListAsync();
		}


		public async Task<SpellSchool> GetById(int id)
		{
			return await m_context.SpellSchool
				.Include(x => x.SchoolCategories)
				.Where(x => x.SpellSchoolID == id)
				.FirstOrDefaultAsync();
		}


		public async Task<SpellSchool> Create(SpellSchool school)
		{
			await m_context.SpellSchool.AddAsync(school);
			await m_context.SaveChangesAsync();
			return await GetById(school.SpellSchoolID);
		}


		public async Task<SpellSchool> Update(int id, SpellSchool school)
		{
			SpellSchool oldSchool = await GetById(id);
			if(oldSchool != null)
			{
				oldSchool.SpellSchoolName = school.SpellSchoolName;

				await m_context.SaveChangesAsync();
			}

			return oldSchool;
		}


		public async Task<SpellSchool> Delete(SpellSchool school)
		{
			m_context.SpellSchool.Remove(school);
			await m_context.SaveChangesAsync();
			return school;
		}
	}
}
