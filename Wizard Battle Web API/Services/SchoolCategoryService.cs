namespace Wizard_Battle_Web_API.Services
{
	public interface ISchoolCategoryService
	{
		Task<List<StaticSchoolCategoryResponse>> GetAll();
		Task<DirectSchoolCategoryResponse> GetById(int id);
		Task<DirectSchoolCategoryResponse> Create(SchoolCategoryRequest request);
		Task<DirectSchoolCategoryResponse> Update(int id, SchoolCategoryRequest request);
		Task<DirectSchoolCategoryResponse> Delete(int id);
	}
	public class SchoolCategoryService : ISchoolCategoryService
	{
		private readonly ISchoolCategoryRepository m_schoolCategoryRepository;
		private readonly IMapper m_mapper;


		public SchoolCategoryService(ISchoolCategoryRepository schoolCategoryRespository, IMapper mapper)
		{
			m_schoolCategoryRepository = schoolCategoryRespository;
			m_mapper = mapper;
		}


		public async Task<List<StaticSchoolCategoryResponse>> GetAll()
		{
			List<SchoolCategory> schoolCategories = await m_schoolCategoryRepository.GetAll();
			if(schoolCategories != null)
			{
				return schoolCategories.Select(schoolCategory => m_mapper.Map<SchoolCategory, StaticSchoolCategoryResponse>(schoolCategory)).ToList();
			}

			return null;
		}


		public async Task<DirectSchoolCategoryResponse> GetById(int id)
		{
			SchoolCategory category = await m_schoolCategoryRepository.GetById(id);
			if(category != null)
			{
				return m_mapper.Map<DirectSchoolCategoryResponse>(category);
			}

			return null;
		}


		public async Task<DirectSchoolCategoryResponse> Create(SchoolCategoryRequest request)
		{
			SchoolCategory category = await m_schoolCategoryRepository.Create(m_mapper.Map<SchoolCategory>(request));
			if (category != null)
			{
				return m_mapper.Map<DirectSchoolCategoryResponse>(category);
			}

			return null;
		}


		public async Task<DirectSchoolCategoryResponse> Update(int id, SchoolCategoryRequest request)
		{
			SchoolCategory category = await m_schoolCategoryRepository.Update(id, m_mapper.Map<SchoolCategory>(request));
			if (category != null)
			{
				return m_mapper.Map<DirectSchoolCategoryResponse>(category);
			}

			return null;
		}


		public async Task<DirectSchoolCategoryResponse> Delete(int id)
		{
			SchoolCategory category = await m_schoolCategoryRepository.GetById(id);
			if (category != null)
			{
				return m_mapper.Map<DirectSchoolCategoryResponse>(await m_schoolCategoryRepository.Delete(category));
			}

			return null;
		}
	}
}
