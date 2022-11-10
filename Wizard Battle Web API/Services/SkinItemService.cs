namespace Wizard_Battle_Web_API.Services
{
	public interface ISkinItemService
	{
		public Task<List<StaticSkinItemResponse>> GetAll();
		public Task<DirectSkinItemResponse> GetById(int id);
		public Task<DirectSkinItemResponse> Create(SkinItemRequest request);
		public Task<DirectSkinItemResponse> Update(int id, SkinItemRequest request);
		public Task<DirectSkinItemResponse> Delete(int id);
	}

	public class SkinItemService : ISkinItemService
	{
		private readonly ISkinItemRepository m_skinItemRepository;
		private readonly IMapper m_mapper;


		public SkinItemService(ISkinItemRepository skinItemRepository, IMapper mapper)
		{
			m_skinItemRepository = skinItemRepository;
			m_mapper = mapper;
		}


		public async Task<List<StaticSkinItemResponse>> GetAll()
		{
			List<SkinItem> skins = await m_skinItemRepository.GetAll();
			if(skins != null)
			{
				return skins.Select(skin => m_mapper.Map<SkinItem, StaticSkinItemResponse>(skin)).ToList();
			}

			return null;
		}


		public async Task<DirectSkinItemResponse> GetById(int id)
		{
			SkinItem skin = await m_skinItemRepository.GetById(id);
			if(skin != null)
			{
				return m_mapper.Map<DirectSkinItemResponse>(skin);
			}

			return null;
		}


		public async Task<DirectSkinItemResponse> Create(SkinItemRequest request)
		{
			SkinItem skin = await m_skinItemRepository.Create(m_mapper.Map<SkinItem>(request));
			if (skin != null)
			{
				return m_mapper.Map<DirectSkinItemResponse>(skin);
			}

			return null;
		}


		public async Task<DirectSkinItemResponse> Update(int id, SkinItemRequest request)
		{
			SkinItem skin = await m_skinItemRepository.Update(id, m_mapper.Map<SkinItem>(request));
			if (skin != null)
			{
				return m_mapper.Map<DirectSkinItemResponse>(skin);
			}

			return null;
		}


		public async Task<DirectSkinItemResponse> Delete(int id)
		{
			SkinItem skin = await m_skinItemRepository.GetById(id);
			if (skin != null)
			{
				return m_mapper.Map<DirectSkinItemResponse>(await m_skinItemRepository.Delete(skin));
			}

			return null;
		}
	}
}
