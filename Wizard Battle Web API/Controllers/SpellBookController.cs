namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SpellBookController : ControllerBase
	{
		private readonly ISpellBookService m_spellBookService;


		public SpellBookController(ISpellBookService spellBookService)
		{
			m_spellBookService = spellBookService;
		}


		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				List<StaticSpellBookResponse> spells = await m_spellBookService.GetAll();
				if (spells == null)
				{
					return Problem("Could not fetch spells, something went wrong.");
				}

				if (spells.Count == 0)
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
				DirectSpellBookResponse spellBook = await m_spellBookService.GetById(id);
				if (spellBook == null)
				{
					return BadRequest();
				}

				return Ok(spellBook);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] SpellBookRequest request)
		{
			try
			{
				DirectSpellBookResponse spellBook = await m_spellBookService.Create(request);
				if (spellBook == null)
				{
					return BadRequest();
				}

				return Ok(spellBook);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] SpellBookRequest request)
		{
			try
			{
				DirectSpellBookResponse spellBook = await m_spellBookService.Update(id, request);
				if (spellBook == null)
				{
					return BadRequest();
				}

				return Ok(spellBook);
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
				DirectSpellBookResponse spellBook = await m_spellBookService.Delete(id);
				if (spellBook == null)
				{
					return BadRequest();
				}

				return Ok(spellBook);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
