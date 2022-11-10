namespace Wizard_Battle_Web_API.Services
{
	public interface IChatService
	{
		Task<List<StaticMessageResponse>> GetById(FriendshipRequest request);
		Task<StaticMessageResponse> Create(MessageRequest request);
		Task<StaticMessageResponse> Delete(MessageRequest request);
	}

	public class ChatService : IChatService
	{
		private readonly IChatRepository m_chatRepository;
		private readonly IMapper m_mapper;


		public ChatService(IChatRepository chatRepository, IMapper mapper)
		{
			m_chatRepository = chatRepository;
			m_mapper = mapper;
		}


		public async Task<List<StaticMessageResponse>> GetById(FriendshipRequest request)
		{
			List<Message> messages = await m_chatRepository.GetById(m_mapper.Map<Friendship>(request));
			if (messages != null)
			{
				return messages.Select(message => m_mapper.Map<StaticMessageResponse>(message)).ToList();
			}

			return null;
		}


		public async Task<StaticMessageResponse> Create(MessageRequest request)
		{
			Message message = await m_chatRepository.Create(m_mapper.Map<Message>(request));
			if (message != null)
			{
				return m_mapper.Map<StaticMessageResponse>(message);
			}

			return null;
		}


		public async Task<StaticMessageResponse> Delete(MessageRequest request)
		{
			Message message = await m_chatRepository.Delete(m_mapper.Map<Message>(request));
			if (message != null)
			{
				return m_mapper.Map<StaticMessageResponse>(message);
			}

			return null;
		}
	}
}
