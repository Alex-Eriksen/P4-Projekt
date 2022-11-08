namespace Wizard_Battle_Web_API.Repositories
{
	public interface IChatRepository
	{
		Task<List<Message>> GetById(Friendship friendship);
		Task<Message> Create(Message message);
		Task<Message> Delete(Message message);
	}

	public class ChatRepository : IChatRepository
	{
		private readonly DatabaseContext _context;


		public ChatRepository(DatabaseContext context)
		{
				_context = context;
		}


		public async Task<List<Message>> GetById(Friendship friendship)
		{
			return await _context.Message.Where(x => (x.SenderID == friendship.MainPlayerID || x.SenderID == friendship.FriendPlayerID)
				&& (x.ReceiverID == friendship.MainPlayerID || x.ReceiverID == friendship.FriendPlayerID)).ToListAsync();
		}


		public async Task<Message> Create(Message message)
		{
			_context.Message.Add(message);
			await _context.SaveChangesAsync();
			return message;
		}


		public async Task<Message> Delete(Message message)
		{
			_context.Message.RemoveRange(_context.Message.Where(x => (x.SenderID == message.SenderID || x.SenderID == message.ReceiverID)
				&& (x.ReceiverID == message.SenderID || x.ReceiverID == message.ReceiverID)));
			await _context.SaveChangesAsync();
			return message;
		}
	}
}
