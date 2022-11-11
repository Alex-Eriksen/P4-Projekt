using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class SkinItemController : ControllerBase
	{
		private readonly ISkinItemService m_skinItemService;

		public SkinItemController(ISkinItemService skinItemService)
		{
			m_skinItemService = skinItemService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				List<StaticSkinItemResponse> skins = await m_skinItemService.GetAll();
				if(skins == null)
				{
					return NotFound();
				}

				if(skins.Count == 0)
				{
					return NoContent();
				}

				return Ok(skins);
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
				DirectSkinItemResponse skin = await m_skinItemService.GetById(id);
				if(skin == null)
				{
					return NotFound();
				}

				return Ok(skin);
			}
			catch (Exception ex)
			{

				return Problem(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create(SkinItemRequest request)
		{
			try
			{
				request.ImageName = $"../../../../assets/skin-images/{request.ImageName}";
				DirectSkinItemResponse skin = await m_skinItemService.Create(request);
				if (skin == null)
				{
					return BadRequest();
				}

				return Ok(skin);
			}
			catch (Exception ex)
			{

				return Problem(ex.Message);
			}
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, SkinItemRequest request)
		{
			try
			{
				DirectSkinItemResponse skin = await m_skinItemService.Update(id, request);
				if(skin == null)
				{
					return BadRequest();
				}

				return Ok(skin);
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
				DirectSkinItemResponse skin = await m_skinItemService.Delete(id);
				if(skin == null)
				{
					return NotFound();
				}

				return Ok(skin);
			}
			catch (Exception ex)
			{

				return Problem(ex.Message);
			}
		}
	}
}
