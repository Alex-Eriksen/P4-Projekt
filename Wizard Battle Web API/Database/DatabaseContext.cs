namespace Wizard_Battle_Web_API.Database
{
	/// <summary>
	/// Inheriting from DbContext
	/// </summary>
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }

		public DbSet<Account> Account { get; set; }
		public DbSet<Friendship> Friendship { get; set; }
		public DbSet<Icon> Icon { get; set; }
		public DbSet<Message> Message { get; set; }
		public DbSet<Player> Player { get; set; }
		public DbSet<RefreshToken> RefreshToken { get; set; }
		public DbSet<SkinItem> Skin { get; set; }
		public DbSet<Spell> Spell { get; set; }
		public DbSet<SpellBook> SpellBook { get; set; }
		public DbSet<SpellBookSlot> SpellBookSlot { get; set; }
		public DbSet<Transaction> Transaction { get; set; }


		/// <summary>
		/// Creating models
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Capturing the datetime when the entities was created in the database. Sets Created_At default to getdate()
			modelBuilder.Entity<RefreshToken>(entity => {
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
			});

			modelBuilder.Entity<Account>(entity => {
				entity.HasIndex(e => e.Email).IsUnique();
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
			});

			modelBuilder.Entity<Player>(entity => {
				entity.HasIndex(e => e.PlayerName).IsUnique();
			});

			modelBuilder.Entity<Friendship>(entity => {
				entity.HasKey(x => new { x.MainPlayerID, x.FriendPlayerID });
				entity.HasOne(x => x.MainPlayer).WithMany(x => x.MainPlayerFriends).HasForeignKey(x => x.MainPlayerID).OnDelete(DeleteBehavior.Restrict);
				entity.HasOne(x => x.FriendPlayer).WithMany(x => x.Friends).HasForeignKey(x => x.FriendPlayerID).OnDelete(DeleteBehavior.Restrict);
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
			});

			modelBuilder.Entity<Message>(entity => {
				entity.HasOne(x => x.Sender).WithMany(x => x.Messages).HasForeignKey(x => x.SenderID).OnDelete(DeleteBehavior.Restrict);
				entity.HasOne(x => x.Receiver).WithMany(x => x.FriendMessages).HasForeignKey(x => x.ReceiverID).OnDelete(DeleteBehavior.Restrict);
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
			});

			modelBuilder.Entity<Transaction>(entity => {
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
				entity.HasOne(x => x.SkinItem).WithMany(x => x.Transactions).HasForeignKey(x => x.SkinID).OnDelete(DeleteBehavior.Restrict);
				entity.HasOne(x => x.Player).WithMany(x => x.Transactions).HasForeignKey(x => x.PlayerID).OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<SpellBookSlot>(entity =>
			{
				entity.HasKey(x => new { x.SpellID, x.SpellBookID });
				entity.HasOne(x => x.SpellBook).WithMany(x => x.SpellBookSlots).HasForeignKey(x => x.SpellBookID).OnDelete(DeleteBehavior.Restrict);
				entity.HasOne(x => x.Spell).WithMany(x => x.SpellBookSlots).HasForeignKey(x => x.SpellID).OnDelete(DeleteBehavior.Restrict);
			});

			// Creating accounts for developers to debug
			modelBuilder.Entity<Account>().HasData(new Account
			{
				  AccountID = 1,
				  Email = "nick@test.com",
				  Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
			});
			modelBuilder.Entity<Account>().HasData(new Account
			{
				AccountID = 2,
				Email = "alex@test.com",
				Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
			});
			modelBuilder.Entity<Account>().HasData(new Account
			{
				AccountID = 3,
				Email = "mart@test.com",
				Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
			});
			modelBuilder.Entity<Account>().HasData(new Account
			{
				AccountID = 4,
				Email = "marc@test.com",
				Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
			});


			// Creating player entities associated with the accounts
			modelBuilder.Entity<Player>().HasData(new Player
			{
				PlayerID = 1,
				AccountID = 1,
				PlayerName = "NickTheG",
				IconID = 1,
				PlayerStatus = "Offline",
				ExperiencePoints = 167,
				KnowledgePoints = 10,
				MaxHealth = 10,
				MaxMana = 10,
				TimeCapsules = 1000,
				MatchWins = 20,
				MatchLosses = 10,
				TimePlayedMin = 120,
				AvgDamage = 150,
				AvgSpellsHit = 13,
				SpellBookID = 1
			});
			modelBuilder.Entity<Player>().HasData(new Player
			{
				PlayerID = 2,
				AccountID = 2,
				PlayerName = "AlexTheG",
				IconID = 2,
				PlayerStatus = "Offline",
				ExperiencePoints = 139,
				KnowledgePoints = 10,
				MaxHealth = 10,
				MaxMana = 10,
				TimeCapsules = 10,
				MatchWins = 12,
				MatchLosses = 7,
				TimePlayedMin = 75,
				AvgDamage = 122,
				AvgSpellsHit = 11,
				SpellBookID = 4
			});
			modelBuilder.Entity<Player>().HasData(new Player
			{
				PlayerID = 3,
				AccountID = 3,
				PlayerName = "MartinTheG",
				IconID = 3,
				PlayerStatus = "Offline",
				ExperiencePoints = 138,
				KnowledgePoints = 10,
				MaxHealth = 10,
				MaxMana = 10,
				TimeCapsules = 10,
				MatchWins = 9,
				MatchLosses = 5,
				TimePlayedMin = 59,
				AvgDamage = 133,
				AvgSpellsHit = 12,
				SpellBookID = 7
			});
			modelBuilder.Entity<Player>().HasData(new Player
			{
				PlayerID = 4,
				AccountID = 4,
				PlayerName = "MarcoTheG",
				IconID = 4,
				PlayerStatus = "Offline",
				ExperiencePoints = 137,
				KnowledgePoints = 10,
				MaxHealth = 10,
				MaxMana = 10,
				TimeCapsules = 10,
				MatchWins = 4,
				MatchLosses = 7,
				TimePlayedMin = 43,
				AvgDamage = 99,
				AvgSpellsHit = 7,
				SpellBookID = 10
			});

			// Adding static icons
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 1,
				IconName = "../../../../assets/player-icons/wizard1.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 2,
				IconName = "../../../../assets/player-icons/wizard2.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 3,
				IconName = "../../../../assets/player-icons/wizard3.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 4,
				IconName = "../../../../assets/player-icons/wizard4.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 5,
				IconName = "../../../../assets/player-icons/alex.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 6,
				IconName = "../../../../assets/player-icons/alex-glasses.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 7,
				IconName = "../../../../assets/player-icons/alex-mustache.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 8,
				IconName = "../../../../assets/player-icons/alex-gangster.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 9,
				IconName = "../../../../assets/player-icons/alex-impersonator.jpg"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 10,
				IconName = "../../../../assets/player-icons/nick-gangster.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 11,
				IconName = "../../../../assets/spell-icons/fireball.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 12,
				IconName = "../../../../assets/spell-icons/firenova.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 13,
				IconName = "../../../../assets/spell-icons/firewall.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 14,
				IconName = "../../../../assets/spell-icons/teleport.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 15,
				IconName = "../../../../assets/spell-icons/windslash.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 16,
				IconName = "../../../../assets/spell-icons/watervortex.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 17,
				IconName = "../../../../assets/spell-icons/dash.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 18,
				IconName = "../../../../assets/spell-icons/placeholder-spell-icon-1.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 19,
				IconName = "../../../../assets/spell-icons/placeholder-spell-icon-2.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 20,
				IconName = "../../../../assets/spell-icons/placeholder-spell-icon-3.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 21,
				IconName = "../../../../assets/spell-icons/placeholder-spell-icon-4.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 22,
				IconName = "../../../../assets/spell-icons/placeholder-spell-icon-5.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 23,
				IconName = "../../../../assets/spell-icons/placeholder-spell-icon-6.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 24,
				IconName = "../../../../assets/spell-icons/placeholder-spell-icon-7.png"
			});
			modelBuilder.Entity<Icon>().HasData(new Icon
			{
				IconID = 25,
				IconName = "../../../../assets/spell-icons/placeholder-spell-icon-8.png"
			});

			// Creating friendships between created players
			modelBuilder.Entity<Friendship>().HasData(new Friendship
			{
				MainPlayerID = 1,
				FriendPlayerID = 2,
				Created_At = DateTime.UtcNow,
				IsPending = false,
			});
			modelBuilder.Entity<Friendship>().HasData(new Friendship
			{
				MainPlayerID = 1,
				FriendPlayerID = 3,
				Created_At = DateTime.UtcNow,
				IsPending = false,
			});
			modelBuilder.Entity<Friendship>().HasData(new Friendship
			{
				MainPlayerID = 1,
				FriendPlayerID = 4,
				Created_At = DateTime.UtcNow,
				IsPending = false,
			});
			modelBuilder.Entity<Friendship>().HasData(new Friendship
			{
				MainPlayerID = 2,
				FriendPlayerID = 3,
				Created_At = DateTime.UtcNow,
				IsPending = false,
			}); 
			modelBuilder.Entity<Friendship>().HasData(new Friendship
			{
				MainPlayerID = 2,
				FriendPlayerID = 4,
				Created_At = DateTime.UtcNow,
				IsPending = false,
			});
			modelBuilder.Entity<Friendship>().HasData(new Friendship
			{
				MainPlayerID = 3,
				FriendPlayerID = 4,
				Created_At = DateTime.UtcNow,
				IsPending = false,
			});

			modelBuilder.Entity<SkinItem>().HasData(new SkinItem
			{
				SkinID = 1,
				SkinName = "Wise Wizard",
				SkinDescription = "A very wise wizard",
				SkinPrice = 125,
				ImageName = "../../../../assets/skin-images/wise-wizard.jpg",
			});

			modelBuilder.Entity<SkinItem>().HasData(new SkinItem
			{
				SkinID = 2,
				SkinName = "Evil Wizard",
				SkinDescription = "A very evil wizard",
				SkinPrice = 125,
				ImageName = "../../../../assets/skin-images/evil-wizard.jpg",
			});

			modelBuilder.Entity<SkinItem>().HasData(new SkinItem
			{
				SkinID = 3,
				SkinName = "Suspicious Wizard",
				SkinDescription = "A very suspicious wizard",
				SkinPrice = 125,
				ImageName = "../../../../assets/skin-images/suspicious-wizard.jpg",
			});

			modelBuilder.Entity<SkinItem>().HasData(new SkinItem
			{
				SkinID = 4,
				SkinName = "Robot Wizard",
				SkinDescription = "A very unhuman wizard",
				SkinPrice = 125,
				ImageName = "../../../../assets/skin-images/robot-wizard.jpg",
			});

			modelBuilder.Entity<Transaction>().HasData(new Transaction
			{
				TransactionID = 1,
				SkinID = 1,
				PlayerID = 1,
				TotalCost = 125,
			});

			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 1,
				SpellName = "Test spell 1",
				SpellDescription = "Lorem ipsum",
				IconID = 18,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 2,
				SpellName = "Test spell 2",
				SpellDescription = "Lorem ipsum",
				IconID = 19,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 3,
				SpellName = "Test spell 3",
				SpellDescription = "Lorem ipsum",
				IconID = 20,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 4,
				SpellName = "Test spell 4",
				SpellDescription = "Lorem ipsum",
				IconID = 21,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 5,
				SpellName = "Test spell 5",
				SpellDescription = "Lorem ipsum",
				IconID = 22,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 6,
				SpellName = "Test spell 6",
				SpellDescription = "Lorem ipsum",
				IconID = 23,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 7,
				SpellName = "Test spell 7",
				SpellDescription = "Lorem ipsum",
				IconID = 24,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 8,
				SpellName = "Test spell 8",
				SpellDescription = "Lorem ipsum",
				IconID = 25,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 9,
				SpellName = "Fireball",
				SpellDescription = "Lorem ipsum",
				IconID = 11,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 10,
				SpellName = "Firenova",
				SpellDescription = "Lorem ipsum",
				IconID = 12,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 11,
				SpellName = "Firewall",
				SpellDescription = "Lorem ipsum",
				IconID = 13,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 12,
				SpellName = "Teleport",
				SpellDescription = "Lorem ipsum",
				IconID = 14,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 13,
				SpellName = "Windslash",
				SpellDescription = "Lorem ipsum",
				IconID = 15,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 14,
				SpellName = "Water Vortex",
				SpellDescription = "Lorem ipsum",
				IconID = 16,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 15,
				SpellName = "Dash",
				SpellDescription = "Lorem ipsum",
				IconID = 17,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
			modelBuilder.Entity<Spell>().HasData(new Spell
			{
				SpellID = 16,
				SpellName = "Fireball 2",
				SpellDescription = "It's a fireball, does it really need a description?",
				IconID = 11,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});

			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 1,
				SpellBookName = "Unnamed Spellbook 1",
				PlayerID = 1,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 2,
				SpellBookName = "Unnamed Spellbook 2",
				PlayerID = 1,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 3,
				SpellBookName = "Unnamed Spellbook 3",
				PlayerID = 1,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 4,
				SpellBookName = "Unnamed Spellbook 1",
				PlayerID = 2,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 5,
				SpellBookName = "Unnamed Spellbook 2",
				PlayerID = 2,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 6,
				SpellBookName = "Unnamed Spellbook 3",
				PlayerID = 2,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 7,
				SpellBookName = "Unnamed Spellbook 1",
				PlayerID = 3,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 8,
				SpellBookName = "Unnamed Spellbook 2",
				PlayerID = 3,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 9,
				SpellBookName = "Unnamed Spellbook 3",
				PlayerID = 3,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 10,
				SpellBookName = "Unnamed Spellbook 1",
				PlayerID = 4,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 11,
				SpellBookName = "Unnamed Spellbook 2",
				PlayerID = 4,
			});
			modelBuilder.Entity<SpellBook>().HasData(new SpellBook
			{
				SpellBookID = 12,
				SpellBookName = "Unnamed Spellbook 3",
				PlayerID = 4,
			});

			modelBuilder.Entity<SpellBookSlot>().HasData(new SpellBookSlot
			{
				SpellBookID = 1,
				SpellID = 1,
			});

			modelBuilder.Entity<SpellBookSlot>().HasData(new SpellBookSlot
			{
				SpellBookID = 1,
				SpellID = 2,
			});
			modelBuilder.Entity<SpellBookSlot>().HasData(new SpellBookSlot
			{
				SpellBookID = 1,
				SpellID = 3,
			});
			modelBuilder.Entity<SpellBookSlot>().HasData(new SpellBookSlot
			{
				SpellBookID = 1,
				SpellID = 4,
			});
			modelBuilder.Entity<SpellBookSlot>().HasData(new SpellBookSlot
			{
				SpellBookID = 1,
				SpellID = 5,
			});
			modelBuilder.Entity<SpellBookSlot>().HasData(new SpellBookSlot
			{
				SpellBookID = 1,
				SpellID = 6,
			});
			modelBuilder.Entity<SpellBookSlot>().HasData(new SpellBookSlot
			{
				SpellBookID = 1,
				SpellID = 7,
			});
			modelBuilder.Entity<SpellBookSlot>().HasData(new SpellBookSlot
			{
				SpellBookID = 1,
				SpellID = 8,
			});
		}
	}
}
