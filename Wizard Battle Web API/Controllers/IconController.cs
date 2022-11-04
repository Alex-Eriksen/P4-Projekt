namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IconController : ControllerBase
	{
		private readonly IIconService m_iconService;


		public IconController(IIconService iconService)
		{
			m_iconService = iconService;
		}


		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				List<IconResponse> icons = await m_iconService.GetAll();
				if(icons == null)
				{
					return NotFound();
				}

				if(icons.Count == 0)
				{
					return NoContent();
				}

				return Ok(icons);
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
				IconResponse icon = await m_iconService.GetById(id);
				if(icon == null)
				{
					return NotFound();
				}

				return Ok(icon);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		[HttpPost]
		public async Task<IActionResult> Create([FromBody] IconRequest request)
		{
			try
			{
				request.IconName = $"../../../../assets/player-icons/{request.IconName}";
				IconResponse icon = await m_iconService.Create(request);
				if(icon == null)
				{
					return BadRequest();
				}

				return Ok(icon);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] IconRequest request)
		{
			try
			{
				request.IconName = $"../../../../assets/player-icons/{request.IconName}";
				IconResponse icon = await m_iconService.Update(id, request);
				if (icon == null)
				{
					return BadRequest();
				}

				return Ok(icon);
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
				IconResponse icon = await m_iconService.Delete(id);
				if (icon == null)
				{
					return BadRequest();
				}

				return Ok(icon);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
