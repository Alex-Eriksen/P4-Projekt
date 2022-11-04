namespace Wizard_Battle_Web_API.Database
{
	/// <summary>
	/// Inheriting from DbContext
	/// </summary>
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }

		public DbSet<RefreshToken> RefreshToken { get; set; }
		public DbSet<Account> Account { get; set; }
		public DbSet<Player> Player { get; set; }
		public DbSet<Friendship> Friendship { get; set; }
		public DbSet<Message> Message { get; set; }
		public DbSet<Icon> Icon { get; set; }
		public DbSet<SkinItem> Skin { get; set; }


		/// <summary>
		/// Creating models
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Capturing the datetime when the entities was createt in the database. Sets Created_At default to getdate()
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
				PlayerStatus = "Online",
				ExperiencePoints = 167,
				KnowledgePoints = 10,
				MaxHealth = 10,
				MaxMana = 10,
				TimeCapsules = 10,
			});
			modelBuilder.Entity<Player>().HasData(new Player
			{
				PlayerID = 2,
				AccountID = 2,
				PlayerName = "AlexTheG",
				IconID = 1,
				PlayerStatus = "Online",
				ExperiencePoints = 138,
				KnowledgePoints = 10,
				MaxHealth = 10,
				MaxMana = 10,
				TimeCapsules = 10,
			});
			modelBuilder.Entity<Player>().HasData(new Player
			{
				PlayerID = 3,
				AccountID = 3,
				PlayerName = "MartinTheG",
				IconID = 1,
				PlayerStatus = "Online",
				ExperiencePoints = 138,
				KnowledgePoints = 10,
				MaxHealth = 10,
				MaxMana = 10,
				TimeCapsules = 10,
			});
			modelBuilder.Entity<Player>().HasData(new Player
			{
				PlayerID = 4,
				AccountID = 4,
				PlayerName = "MarcoTheG",
				IconID = 1,
				PlayerStatus = "Online",
				ExperiencePoints = 138,
				KnowledgePoints = 10,
				MaxHealth = 10,
				MaxMana = 10,
				TimeCapsules = 10,
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
		}
	}
}
