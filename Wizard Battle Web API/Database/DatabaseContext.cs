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
				entity.HasKey(x => new { x.SenderID, x.ReceiverID });
				entity.HasOne(x => x.Sender).WithMany(x => x.Messages).HasForeignKey(x => x.SenderID).OnDelete(DeleteBehavior.Restrict);
				entity.HasOne(x => x.Receiver).WithMany(x => x.FriendMessages).HasForeignKey(x => x.ReceiverID).OnDelete(DeleteBehavior.Restrict);
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
			});

			// Making a acount
			modelBuilder.Entity<Account>().HasData(new Account
			{
				  AccountID = 1,
				  Email = "test@test.com",
				  Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
			});
			modelBuilder.Entity<Account>().HasData(new Account
			{
				AccountID = 2,
				Email = "alex@test.com",
				Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
			});

			modelBuilder.Entity<Player>().HasData(new Player
			{
				PlayerID = 1,
				AccountID = 1,
				PlayerName = "NickTheG",
				PlayerImage = "../../../../assets/alex.png",
				PlayerStatus = "Online",
				ExperiencePoints = 167,
				KnowledgePoints = 10,
				MaxHealth = 10,
				MaxMana = 10,
				TimeCapsules = 10,
			}, new Player
			{
				PlayerID = 2,
				AccountID = 2,
				PlayerName = "AlexTheG",
				PlayerImage = "../../../../assets/alex.png",
				PlayerStatus = "Away",
				ExperiencePoints = 138,
				KnowledgePoints = 10,
				MaxHealth = 10,
				MaxMana = 10,
				TimeCapsules = 10,
			});

		}
	}
}
