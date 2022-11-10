namespace Wizard_Battle_Web_API.Services
{
	public interface IIconService
	{
		Task<List<IconResponse>> GetAll();
		Task<IconResponse> GetById(int id);
		Task<IconResponse> Create(IconRequest request);
		Task<IconResponse> Update(int id, IconRequest request);
		Task<IconResponse> Delete(int id);
	}
	public class IconService : IIconService
	{
		private readonly IIconRepository m_iconRepository;
		private readonly IMapper m_mapper;


		public IconService(IIconRepository iconRepository, IMapper mapper)
		{
			m_iconRepository = iconRepository;
			m_mapper = mapper;
		}


		public async Task<List<IconResponse>> GetAll()
		{
			List<Icon> icons = await m_iconRepository.GetAll();
			if(icons != null)
			{
				return icons.Select(icon => m_mapper.Map<Icon, IconResponse>(icon)).ToList();
			}

			return null;
		}


		public async Task<IconResponse> GetById(int id)
		{
			Icon icon = await m_iconRepository.GetById(id);
			if(icon != null)
			{
				return m_mapper.Map<IconResponse>(icon);
			}

			return null;
		}


		public async Task<IconResponse> Create(IconRequest request)
		{
			Icon icon = await m_iconRepository.Create(m_mapper.Map<Icon>(request));
			if(icon != null)
			{
				return m_mapper.Map<IconResponse>(icon);
			}

			return null;
		}


		public async Task<IconResponse> Update(int id, IconRequest request)
		{
			Icon icon = await m_iconRepository.Update(id, m_mapper.Map<Icon>(request));
			if(icon != null)
			{
				return m_mapper.Map<IconResponse>(icon);
			}

			return null;
		}


		public async Task<IconResponse> Delete(int id)
		{
			Icon icon = await m_iconRepository.GetById(id);
			if(icon != null )
			{
				await m_iconRepository.Delete(icon);

				return m_mapper.Map<IconResponse>(icon);
			}

			return null;
		}
	}
}
