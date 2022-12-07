namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SpellController : ControllerBase
	{
		private readonly ISpellService m_spellService;


		public SpellController(ISpellService spellService)
		{
			m_spellService = spellService;
		}


		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				List<StaticSpellResponse> spells = await m_spellService.GetAll();
				if(spells == null)
				{
					return Problem("Could not fetch spells, something went wrong.");
				}

				if(spells.Count == 0)
				{
					return NoContent();
				}
				return Ok(spells);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				DirectSpellResponse spell = await m_spellService.GetById(id);
				if (spell == null)
				{
					return BadRequest();
				}

				return Ok(spell);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] SpellRequest request)
		{
			try
			{
				DirectSpellResponse spell = await m_spellService.Create(request);
				if (spell == null)
				{
					return BadRequest();
				}

				return Ok(spell);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] SpellRequest request)
		{
			try
			{
				DirectSpellResponse spell = await m_spellService.Update(id, request);
				if (spell == null)
				{
					return BadRequest();
				}

				return Ok(spell);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				DirectSpellResponse spell = await m_spellService.Delete(id);
				if (spell == null)
				{
					return BadRequest();
				}

				return Ok(spell);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
