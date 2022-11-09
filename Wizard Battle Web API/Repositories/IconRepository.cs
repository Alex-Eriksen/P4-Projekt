namespace Wizard_Battle_Web_API.Repositories
{
	public interface IIconRepository
	{
		Task<List<Icon>> GetAll();
		Task<Icon> GetById(int id);
		Task<Icon> Create(Icon request);
		Task<Icon> Update(int id, Icon request);
		Task<Icon> Delete(Icon icon);
	}

	public class IconRepository : IIconRepository
	{
		private readonly DatabaseContext m_context;
		
		
		public IconRepository(DatabaseContext context)
		{
			m_context = context;
		}


		public async Task<List<Icon>> GetAll()
		{
			return await m_context.Icon.ToListAsync();
		}


		public async Task<Icon> GetById(int id)
		{
			return await m_context.Icon.FirstOrDefaultAsync(x => x.IconID == id);
		}


		public async Task<Icon> Create(Icon request)
		{
			m_context.Icon.Add(request);
			await m_context.SaveChangesAsync();
			return await GetById(request.IconID);
		}


		public async Task<Icon> Update(int id, Icon request)
		{
			Icon icon = await GetById(id);
			if(icon != null)
			{
				icon.IconName = request.IconName;

				await m_context.SaveChangesAsync();
			}

			return icon;
		}


		public async Task<Icon> Delete(Icon icon)
		{
			m_context.Icon.Remove(icon);
			await m_context.SaveChangesAsync();
			return icon;
		}
	}
}
