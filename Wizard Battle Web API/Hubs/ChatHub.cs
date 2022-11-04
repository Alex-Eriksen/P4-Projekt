namespace Wizard_Battle_Web_API.Hubs
{
	[Authorize]
	public class ChatHub : Hub
	{
		public Task SendMessage(MessageRequest message)
		{
			var userId = (Context.User.Identity as ClaimsIdentity).Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).ToString();
			return Clients.User(userId).SendAsync("ReceiveOne", message, userId);
		}

		public async Task BroadcastToConnection(string data, string connectionId)
			=> await Clients.Client(connectionId).SendAsync("broadcasttoclient", data);

		public async Task BroadcastToUser(string data, string userId)
			=> await Clients.User(userId).SendAsync("broadcasttouser", data);

		public async Task BroadcastAll()
		{
			await Clients.All.SendAsync("Broadcast", "Hello");
		}
	}
}
