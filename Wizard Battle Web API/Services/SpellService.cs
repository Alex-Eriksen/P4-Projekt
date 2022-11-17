namespace Wizard_Battle_Web_API.Services
{
	public interface ISpellService
	{
		Task<List<StaticSpellResponse>> GetAll();
		Task<DirectSpellResponse> GetById(int spellId);
		Task<DirectSpellResponse> Create(SpellRequest request);
		Task<DirectSpellResponse> Update(int spellId, SpellRequest request);
		Task<DirectSpellResponse> Delete(int spellId);
	}
	public class SpellService : ISpellService
	{
		private readonly ISpellRepository m_spellRepository;
		private readonly IMapper m_mapper;


		public SpellService(ISpellRepository spellRepository, IMapper mapper)
		{
			m_spellRepository = spellRepository;
			m_mapper = mapper;
		}


		public async Task<List<StaticSpellResponse>> GetAll()
		{
			List<Spell> spells = await m_spellRepository.GetAll();
			if(spells != null)
			{
				return spells.Select(spell => m_mapper.Map<Spell, StaticSpellResponse>(spell)).ToList();
			}

			return null;
		}


		public async Task<DirectSpellResponse> GetById(int spellId)
		{
			Spell spell = await m_spellRepository.GetById(spellId);
			if(spell != null)
			{
				return m_mapper.Map<DirectSpellResponse>(spell);
			}

			return null;
		}


		public async Task<DirectSpellResponse> Create(SpellRequest request)
		{
			Spell spell = await m_spellRepository.Create(m_mapper.Map<Spell>(request));
			if(spell != null)
			{
				return m_mapper.Map<DirectSpellResponse>(spell);
			}

			return null;
		}


		public async Task<DirectSpellResponse> Update(int spellId, SpellRequest request)
		{
			Spell spell = await m_spellRepository.Update(spellId, m_mapper.Map<Spell>(request));
			if (spell != null)
			{
				return m_mapper.Map<DirectSpellResponse>(spell);
			}

			return null;
		}


		public async Task<DirectSpellResponse> Delete(int spellId)
		{
			Spell spell = await m_spellRepository.GetById(spellId);
			if (spell != null)
			{
				return m_mapper.Map<DirectSpellResponse>(await m_spellRepository.Delete(spell));
			}

			return null;
		}
	}
}
