using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Wizard_Battle_Web_API.Hubs;

namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ChatController : ControllerBase
	{
		private readonly IChatService m_chatService;
		private readonly IHubContext<ChatHub, IChatHub> m_hubContext;

		public ChatController(IChatService chatService, IHubContext<ChatHub, IChatHub> hubContext)
		{
			m_chatService = chatService;
			m_hubContext = hubContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetById([FromQuery] int mainPlayerId, [FromQuery] int friendPlayerId)
		{
			try
			{
				FriendshipRequest request = new FriendshipRequest { MainPlayerID = mainPlayerId, FriendPlayerID = friendPlayerId };
				List<StaticMessageResponse> messages = await m_chatService.GetById(request);

				if (messages == null)
				{
					return Problem("Message was not created, something failed...");
				}

				if (messages.Count == 0)
				{
					return NoContent();
				}

				return Ok(messages);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] MessageRequest request)
		{
			try
			{
				StaticMessageResponse message = await m_chatService.Create(request);

				if (message == null)
				{
					return Problem("Message was not created, something failed...");
				}

				await m_hubContext.Clients.User(request.ReceiverID.ToString()).ReceiveUserMessage(request);

				return Ok(message);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] MessageRequest request)
		{
			try
			{
				StaticMessageResponse message = await m_chatService.Delete(request);
				if (message == null)
				{
					return Problem("Messages were not deleted, something went wrong...");
				}

				return Ok(message);
			}
			catch(Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
