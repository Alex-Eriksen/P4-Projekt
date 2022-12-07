using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SchoolCategoryController : ControllerBase
	{
		private readonly ISchoolCategoryService m_schoolCategoryService;


		public SchoolCategoryController(ISchoolCategoryService schoolCategoryService)
		{
			m_schoolCategoryService = schoolCategoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				List<StaticSchoolCategoryResponse> responses = await m_schoolCategoryService.GetAll();
				if(responses == null)
				{
					return BadRequest();
				}

				if(responses.Count == 0)
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
				DirectSchoolCategoryResponse response = await m_schoolCategoryService.GetById(id);
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
		public async Task<IActionResult> Create([FromBody] SchoolCategoryRequest request)
		{
			try
			{
				DirectSchoolCategoryResponse response = await m_schoolCategoryService.Create(request);
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
		public async Task<IActionResult> Update(int id, [FromBody] SchoolCategoryRequest request)
		{
			try
			{
				DirectSchoolCategoryResponse response = await m_schoolCategoryService.Update(id, request);

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
				DirectSchoolCategoryResponse response = await m_schoolCategoryService.Delete(id);

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
