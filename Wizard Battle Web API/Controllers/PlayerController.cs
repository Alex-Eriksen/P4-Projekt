using Microsoft.AspNetCore.SignalR;
using Wizard_Battle_Web_API.Hubs;
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
		private readonly IAccountService m_accountService;
		private readonly IFriendshipService m_friendshipService;
		private readonly IHubContext<ChatHub, IChatHub> m_hubContext;


		/// <summary>
		/// Constructor for PlayerController
		/// </summary>
		/// <param name="playerService"></param>
		/// <param name="accountService"></param>
		public PlayerController(IPlayerService playerService, IAccountService accountService, IHubContext<ChatHub, IChatHub> hubContext, IFriendshipService friendshipService)
		{
			m_playerService = playerService;
			m_accountService = accountService;
			m_friendshipService = friendshipService;
			m_hubContext = hubContext;
		}


		/// <summary>
		/// Gets all Players.
		/// </summary>
		/// <returns>Players, HTTP 204 or exception</returns>
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				List<StaticPlayerResponse> players = await m_playerService.GetAll();

				if (players == null)
				{
					return Problem("Nothing was returned from service, this was unexpected");
				}

				if (players.Count == 0)
				{
					return NoContent();
				}

				return Ok(players);
			}
			catch (Exception ex)
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
		[Authorize]
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
				List<StaticAccountResponse> accounts = await m_accountService.GetAll();
				if (accounts.Any(x => x.Email == request.Account.Email))
				{
					return Conflict();
				}

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
		[Authorize]
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

		[HttpPut]
		[Route("status")]
		[Authorize]
		public async Task<IActionResult> ChangeStatus([FromQuery] int playerId, [FromQuery] string status)
		{
			try
			{
				DirectPlayerResponse player = await m_playerService.ChangeStatus(playerId, status);

				if (player == null)
				{
					return NotFound();
				}

				List<StaticFriendshipResponse> friendships = await m_friendshipService.GetAllById(playerId);
				
				string[] friends = new string[friendships.Count];
				
				for(int i = 0; i < friendships.Count; i++)
				{
					if(friendships[i].MainPlayerID == playerId)
					{
						friends[i] = friendships[i].FriendPlayer.PlayerID.ToString();
						continue;
					}
					friends[i] = friendships[i].MainPlayerID.ToString();
				}

				await m_hubContext.Clients.Users(friends).ChangeFriendStatus("A friend changed his status");

				return Ok(player);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
