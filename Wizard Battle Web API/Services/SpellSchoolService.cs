namespace Wizard_Battle_Web_API.Services
{
	public interface ISpellSchoolService
	{
		public Task<List<StaticSpellSchoolResponse>> GetAll();
		public Task<DirectSpellSchoolResponse> GetById(int id);
		public Task<DirectSpellSchoolResponse> Create(SpellSchoolRequest request);
		public Task<DirectSpellSchoolResponse> Update(int id, SpellSchoolRequest request);
		public Task<DirectSpellSchoolResponse> Delete(int id);
	}
	public class SpellSchoolService : ISpellSchoolService
	{
		private readonly ISpellSchoolRepository m_spellSchoolRepository;
		private readonly IMapper m_mapper;

		public SpellSchoolService(ISpellSchoolRepository spellSchoolRepository, IMapper mapper)
		{
			m_spellSchoolRepository = spellSchoolRepository;
			m_mapper = mapper;
		}


		public async Task<List<StaticSpellSchoolResponse>> GetAll()
		{
			List<SpellSchool> schools = await m_spellSchoolRepository.GetAll();
			if(schools != null)
			{
				return schools.Select(spellSchool => m_mapper.Map<SpellSchool, StaticSpellSchoolResponse>(spellSchool)).ToList();
			}

			return null;
		}


		public async Task<DirectSpellSchoolResponse> GetById(int id)
		{
			SpellSchool school = await m_spellSchoolRepository.GetById(id);
			if(school != null)
			{
				return m_mapper.Map<DirectSpellSchoolResponse>(school);
			}

			return null;
		}


		public async Task<DirectSpellSchoolResponse> Create(SpellSchoolRequest request)
		{
			SpellSchool school = await m_spellSchoolRepository.Create(m_mapper.Map<SpellSchool>(request));
			if(school != null)
			{
				return m_mapper.Map<DirectSpellSchoolResponse>(school);
			}

			return null;
		}


		public async Task<DirectSpellSchoolResponse> Update(int id, SpellSchoolRequest request)
		{
			SpellSchool school = await m_spellSchoolRepository.Update(id, m_mapper.Map<SpellSchool>(request));
			if (school != null)
			{
				return m_mapper.Map<DirectSpellSchoolResponse>(school);
			}

			return null;
		}


		public async Task<DirectSpellSchoolResponse> Delete(int id)
		{
			SpellSchool school = await m_spellSchoolRepository.GetById(id);
			if (school != null)
			{
				return m_mapper.Map<DirectSpellSchoolResponse>(await m_spellSchoolRepository.Delete(school));
			}

			return null;
		}
	}
}
