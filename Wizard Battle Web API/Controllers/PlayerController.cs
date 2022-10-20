namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlayerController : ControllerBase
	{
		/// <summary>
		/// Using IPlayerService as interface.
		/// </summary>
		private readonly IPlayerService m_playerService;


		/// <summary>
		/// Constructor for PlayerController
		/// </summary>
		/// <param name="playerService"></param>
		public PlayerController(IPlayerService playerService)
		{
			m_playerService = playerService;
		}


		/// <summary>
		/// Gets all Players.
		/// </summary>
		/// <returns>Players, HTTP 204 or exception</returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				List<StaticPlayerResponse> players = await m_playerService.GetAll();

				if(players == null)
				{
					return Problem("Nothing was returned from service, this was unexpected");
				}

				if(players.Count == 0)
				{
					return NoContent();
				}

				return Ok(players);
			}
			catch(Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		/// <summary>
		/// Gets Player by its id.
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns>Player or HTTP 204</returns>
		[HttpGet]
		[Route("{playerId}")]
		public async Task<IActionResult> GetById(int playerId)
		{
			try
			{
				DirectPlayerResponse player = await m_playerService.GetById(playerId);

				if (player == null)
				{
					return NotFound();
				}
				return Ok(player);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		/// <summary>
		/// Creates a Player.
		/// </summary>
		/// <param name="request"></param>
		/// <returns>Player or exception</returns>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] PlayerAccountRequest request)
		{
			try
			{
				DirectPlayerResponse player = await m_playerService.Create(request);

				if (player == null)
				{
					return Problem("Customer was not created, something failed...");
				}
				return Ok(player);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		/// <summary>
		/// Updates a Player.
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="request"></param>
		/// <returns>Player or exception</returns>
		[HttpPut]
		[Route("{playerId}")]
		public async Task<IActionResult> Update(int playerId, PlayerRequest request)
		{
			try
			{
				DirectPlayerResponse player = await m_playerService.Update(playerId, request);

				if (player == null)
				{
					return NotFound();
				}
				return Ok(player);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
