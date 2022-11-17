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
		public DbSet<SpellBook> SpellBookSlot { get; set; }
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
				SpellName = "Fireball",
				SpellDescription = "It's a fireball, does it really need a description?",
				IconID = 11,
				ManaCost = 0,
				DamageAmount = 0,
				CastTime = 0,

			});
		}
	}
}
