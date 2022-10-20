﻿namespace Wizard_Battle_Web_API.Database
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
		public DbSet<PlayerStats> PlayerStats { get; set; }

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
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
			});

			// Making a acount
			modelBuilder.Entity<Account>().HasData(
			new Account
			{
				  AccountID = 1,
				  Email = "test@test.com",
				  Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
			});

			modelBuilder.Entity<Player>().HasData(
				new Player
				{
					PlayerID = 1,
					AccountID = 1,
					PlayerName = "NickTheG"
				});
		}
	}
}
