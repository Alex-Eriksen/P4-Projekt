using Wizard_Battle_Web_API.DTOs.Icon;

namespace Wizard_Battle_Web_API.Helpers
{
	public class AutoMapper : Profile
	{
		public AutoMapper()
		{
			CreateMap<Account, DirectAccountResponse>();
			CreateMap<Account, StaticAccountResponse>();
			CreateMap<AccountRequest, Account>();

			CreateMap<Player, DirectPlayerResponse>();
			CreateMap<Player, StaticPlayerResponse>();
			CreateMap<PlayerRequest, Player>();

			CreateMap<Friendship, DirectFriendshipResponse>();
			CreateMap<Friendship, StaticFriendshipResponse>();
			CreateMap<FriendshipRequest, Friendship>();

			CreateMap<MessageRequest, Message>();
			CreateMap<Message, StaticMessageResponse>();

			CreateMap<AuthenticationResponse, StaticRefreshTokenResponse>();

			CreateMap<Icon, IconResponse>();
			CreateMap<IconResponse, Icon>();
			CreateMap<IconRequest, Icon>();

			CreateMap<Transaction, StaticTransactionResponse>();
			CreateMap<Transaction, DirectTransactionResponse>();
			CreateMap<TransactionRequest, Transaction>();


			CreateMap<SkinItem, StaticSkinItemResponse>();
			CreateMap<SkinItem, DirectSkinItemResponse>();
			CreateMap<SkinItemRequest, SkinItem>();

			CreateMap<Spell, StaticSpellResponse>();
			CreateMap<Spell, DirectSpellResponse>();
			CreateMap<SpellRequest, Spell>();

			CreateMap<SpellBook, StaticSpellBookResponse>();
			CreateMap<SpellBook, DirectSpellBookResponse>()
				.ForMember(dto => dto.Spells, opt => opt.MapFrom(x => x.SpellBookSlots.Select(y => y.Spell).ToList()));
			CreateMap<SpellBookRequest, SpellBook>()
				.ForMember(dto => dto.SpellBookSlots, opt => opt.MapFrom(x => x.Spells));
		}
	}
}
