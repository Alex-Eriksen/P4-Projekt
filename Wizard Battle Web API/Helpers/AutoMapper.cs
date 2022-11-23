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
			CreateMap<DirectSpellBookResponse, SpellBook>()
				.ForMember(dest => dest.SpellBookID, opt => opt.MapFrom(src => src.SpellBookID))
				.ForMember(dest => dest.SpellBookName, opt => opt.MapFrom(src => src.SpellBookName))
				.ForMember(dest => dest.Player, opt => opt.MapFrom(src => src.Player))
				.ForMember(dest => dest.SpellBookSlots, opt => opt.MapFrom(src => src.Spells))
				.AfterMap((src, dest) =>
				{
					foreach (var spells in dest.SpellBookSlots)
					{
						spells.SpellBookID = src.SpellBookID;
					}
				});
			CreateMap<SpellBook, DirectSpellBookResponse>()
				.ForMember(dest => dest.Spells, opt => opt.MapFrom(src => src.SpellBookSlots.Select(x => x.Spell).ToList()));

			CreateMap<SpellBookRequest, SpellBook>()
				.ForMember(dest => dest.SpellOrder, opt => opt.MapFrom(src => String.Join(",", src.SpellIDs.Select(id => id.ToString()).ToArray())));
		}
	}
}
