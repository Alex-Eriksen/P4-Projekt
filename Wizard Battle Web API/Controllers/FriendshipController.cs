using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FriendshipController : ControllerBase
	{
		/// <summary>
		/// Using IFriendshipService to control Friendship
		/// </summary>
		private readonly IFriendshipService m_friendshipService;


		/// <summary>
		/// Constructor for FriendshipController
		/// </summary>
		/// <param name="friendshipService"></param>
		public FriendshipController(IFriendshipService friendshipService)
		{
			m_friendshipService = friendshipService;
		}


		/// <summary>
		/// Gets all friends associated with player.
		/// </summary>
		/// <returns>Friendships, message or exception</returns>
		[HttpGet]
		[Route("{playerId}")]
		public async Task<IActionResult> GetAll(int playerId)
		{
			try
			{
				List<StaticFriendshipResponse> friendships = await m_friendshipService.GetAllFriendship(playerId);

				if (friendships == null)
				{
					return Problem("Nothing was returned from service, this was unexpected");
				}

				if (friendships.Count == 0)
				{
					return NoContent();
				}

				return Ok(friendships);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		/// <summary>
		/// Gets frienship from database by its PlayerID & FriendID.
		/// </summary>
		/// <param name="mainPlayerId"></param>
		/// <param name="friendPlayerId"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{mainPlayerId}, {friendPlayerId}")]
		public async Task<IActionResult> GetById(int mainPlayerId, int friendPlayerId)
		{
			try
			{
				FriendshipRequest request = new FriendshipRequest{ MainPlayerID = mainPlayerId, FriendPlayerID = friendPlayerId};
				DirectFriendshipResponse friendship = await m_friendshipService.GetFriendship(request);

				if (friendship == null)
				{
					return NotFound();
				}

				return Ok(friendship);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		/// <summary>
		/// Creates a friendship.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] FriendshipRequest request)
		{
			try
			{
				DirectFriendshipResponse friendship = await m_friendshipService.AddFriend(request);

				if (friendship == null)
				{
					return Problem("Friendship was not created, something failed...");
				}

				return Ok(friendship);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		/// <summary>
		/// Updates a friendship.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] FriendshipRequest request)
		{
			try
			{
				DirectFriendshipResponse friendship = await m_friendshipService.AcceptFriend(request);

				if (friendship == null)
				{
					return Problem("Friendship was not accepted, something failed...");
				}

				return Ok(friendship);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		/// <summary>
		/// Deletes a friendship.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] FriendshipRequest request)
		{
			try
			{
				DirectFriendshipResponse friendship = await m_friendshipService.RemoveFriend(request);

				if (friendship == null)
				{
					return Problem("Friendship was not deleted, something failed...");
				}

				return Ok(friendship);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
		

		[HttpPost]
		[Route("messages/")]
		public async Task<IActionResult> CreateMessage([FromBody] MessageRequest request)
		{
			try
			{
				StaticMessageResponse message = await m_friendshipService.SendMessage(request);

				if (message == null)
				{
					return Problem("Message was not created, something failed...");
				}

				return Ok(message);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpGet]
		[Route("messages/{mainPlayerId}, {friendPlayerId}")]
		public async Task<IActionResult> GetMessages(int mainPlayerId, int friendPlayerId)
		{
			try
			{
				FriendshipRequest request = new FriendshipRequest { MainPlayerID = mainPlayerId, FriendPlayerID = friendPlayerId };
				List<StaticMessageResponse> messages = await m_friendshipService.GetAllMessages(request);

				if (messages == null)
				{
					return Problem("Message was not created, something failed...");
				}

				if(messages.Count == 0)
				{
					return NoContent();
				}

				return Ok(messages);
			}
			catch(Exception ex)
			{
				return Problem(ex.Message);
			}
		}

	}
}
