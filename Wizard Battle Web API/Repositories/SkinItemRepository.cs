namespace Wizard_Battle_Web_API.Repositories
{
	public interface ISkinItemRepository
	{
		public Task<List<SkinItem>> GetAll();
		public Task<SkinItem> GetById(int id);
		public Task<SkinItem> Create(SkinItem skin);
		public Task<SkinItem> Update(int id, SkinItem skin);
		public Task<SkinItem> Delete(SkinItem skin);
	}

	public class SkinItemRepository : ISkinItemRepository
	{
		private readonly DatabaseContext m_context;


		public SkinItemRepository(DatabaseContext context)
		{
			m_context = context;
		}


		public async Task<List<SkinItem>> GetAll()
		{
			return await m_context.Skin.ToListAsync();
		}


		public async Task<SkinItem> GetById(int id)
		{
			return await m_context.Skin.Include(x => x.Transactions).FirstOrDefaultAsync(x => x.SkinID == id);
		}


		public async Task<SkinItem> Create(SkinItem skin)
		{
			await m_context.Skin.AddAsync(skin);
			await m_context.SaveChangesAsync();
			return await GetById(skin.SkinID);
		}


		public async Task<SkinItem> Update(int id, SkinItem request)
		{
			SkinItem skinItem = await GetById(id);
			if(skinItem != null)
			{
				skinItem.SkinName = request.SkinName;
				skinItem.SkinDescription = request.SkinDescription;
				skinItem.SkinPrice = request.SkinPrice;
				skinItem.ImageName = request.ImageName;

				await m_context.SaveChangesAsync();
			}

			return skinItem;
		}


		public async Task<SkinItem> Delete(SkinItem skin)
		{
			m_context.Skin.Remove(skin);
			await m_context.SaveChangesAsync();
			return skin;
		}
	}
}
