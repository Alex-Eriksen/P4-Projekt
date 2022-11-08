using System.Diagnostics;

namespace Wizard_Battle_Web_API.Hubs
{

	public interface IChatHub
	{
		Task OnConnect(string message);
		Task ReceiveUserMessage(MessageRequest request);
	}

	[Authorize]
	public class ChatHub : Hub<IChatHub>
	{
		public async Task SendMessageToUser(MessageRequest request)
			=> await Clients.User(request.ReceiverID.ToString()).ReceiveUserMessage(request);

		public override async Task OnConnectedAsync()
		{
			await Clients.All.OnConnect($"UserID: {Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!} has joined.");
			await base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			return base.OnDisconnectedAsync(exception);
		}

		public string GetConnectionId() => Context.ConnectionId;

		public string GetUserId() => Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	}
}
