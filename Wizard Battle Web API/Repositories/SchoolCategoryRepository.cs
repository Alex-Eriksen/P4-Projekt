namespace Wizard_Battle_Web_API.Repositories
{
	public interface ISchoolCategoryRepository
	{
		public Task<List<SchoolCategory>> GetAll();
		public Task<SchoolCategory> GetById(int id);
		public Task<SchoolCategory> Create(SchoolCategory category);
		public Task<SchoolCategory> Update(int id, SchoolCategory category);
		public Task<SchoolCategory> Delete(SchoolCategory category);
	}
	public class SchoolCategoryRepository : ISchoolCategoryRepository
	{
		private readonly DatabaseContext m_context;

		public SchoolCategoryRepository(DatabaseContext context)
		{
			m_context = context;
		}


		public async Task<List<SchoolCategory>> GetAll()
		{
			return await m_context.SchoolCategory.ToListAsync();
		}


		public async Task<SchoolCategory> GetById(int id)
		{
			return await m_context.SchoolCategory
				.Include(x => x.SpellSchool)
				.Where(x => x.SchoolCategoryID == id)
				.FirstOrDefaultAsync();
		}


		public async Task<SchoolCategory> Create(SchoolCategory category)
		{
			await m_context.SchoolCategory.AddAsync(category);
			await m_context.SaveChangesAsync();
			return await GetById(category.SchoolCategoryID);
		}


		public async Task<SchoolCategory> Update(int id, SchoolCategory category)
		{
			SchoolCategory oldCategory = await GetById(id);
			if(oldCategory != null)
			{
				oldCategory.SchoolCategoryName = category.SchoolCategoryName;
				oldCategory.SpellSchoolID = category.SpellSchoolID;

				await m_context.SaveChangesAsync();
			}

			return category;
		}


		public async Task<SchoolCategory> Delete(SchoolCategory category)
		{
			m_context.SchoolCategory.Remove(category);
			await m_context.SaveChangesAsync();
			return category;
		}
	}
}
