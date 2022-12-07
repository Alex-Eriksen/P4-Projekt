using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SpellSchoolController : ControllerBase
	{
		private readonly ISpellSchoolService m_spellSchoolService;


		public SpellSchoolController(ISpellSchoolService spellSchoolService)
		{
			m_spellSchoolService = spellSchoolService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				List<StaticSpellSchoolResponse> responses = await m_spellSchoolService.GetAll();
				if (responses == null)
				{
					return BadRequest();
				}

				if (responses.Count == 0)
				{
					return NoContent();
				}

				return Ok(responses);
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
				DirectSpellSchoolResponse response = await m_spellSchoolService.GetById(id);
				if (response == null)
				{
					return BadRequest();
				}

				return Ok(response);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] SpellSchoolRequest request)
		{
			try
			{
				DirectSpellSchoolResponse response = await m_spellSchoolService.Create(request);
				if (response == null)
				{
					return BadRequest();
				}

				return Ok(response);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] SpellSchoolRequest request)
		{
			try
			{
				DirectSpellSchoolResponse response = await m_spellSchoolService.Update(id, request);

				if (response == null)
				{
					return BadRequest();
				}

				return Ok(response);
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
				DirectSpellSchoolResponse response = await m_spellSchoolService.Delete(id);

				if (response == null)
				{
					return BadRequest();
				}

				return Ok(response);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
