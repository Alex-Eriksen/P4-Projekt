namespace Wizard_Battle_Web_API.Hubs
{
	[Authorize]
	public class ChatHub : Hub
	{
		public string GetConnectionId() => Context.ConnectionId;

		public string GetUserId() => Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		public override async Task OnConnectedAsync()
		{
			await Clients.All.SendAsync("ReceiveSystemMessage", $"UserID: {Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!} has joined.");
			await base.OnConnectedAsync();
		}

		public async Task UserMessage(string connectionId, string message)
		{
			try
			{
				await Clients.Client(connectionId).SendAsync("usermessage", message);

			}
			catch (HubException ex)
			{
				throw new HubException("Something went wrong", ex);
			}

		}
	}
}
