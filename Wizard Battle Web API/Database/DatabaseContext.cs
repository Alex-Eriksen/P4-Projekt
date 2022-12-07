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
		public DbSet<SpellType> SpellType { get; set; }
		public DbSet<SpellSchool> SpellSchool { get; set; }
		public DbSet<SchoolCategory> SchoolCategory { get; set; }
		public DbSet<SpellBook> SpellBook { get; set; }
		public DbSet<SpellBookSlot> SpellBookSlot { get; set; }
		public DbSet<Transaction> Transaction { get; set; }


		/// <summary>
		/// Creating models
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Capturing the datetime when the entities were created in the database.
			modelBuilder.Entity<RefreshToken>(entity => {
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
			});

			// Capturing the datetime when the entities were created in the database. Defines one to many relationships.
			modelBuilder.Entity<Message>(entity => {
				entity.HasOne(x => x.Sender).WithMany(x => x.Messages).HasForeignKey(x => x.SenderID).OnDelete(DeleteBehavior.Restrict);
				entity.HasOne(x => x.Receiver).WithMany(x => x.FriendMessages).HasForeignKey(x => x.ReceiverID).OnDelete(DeleteBehavior.Restrict);
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
			});

			// Defines one to many relationships.
			modelBuilder.Entity<SpellBookSlot>(entity =>
			{
				entity.HasKey(x => new { x.SpellID, x.SpellBookID });
				entity.HasOne(x => x.SpellBook).WithMany(x => x.SpellBookSlots).HasForeignKey(x => x.SpellBookID).OnDelete(DeleteBehavior.Restrict);
				entity.HasOne(x => x.Spell).WithMany(x => x.SpellBookSlots).HasForeignKey(x => x.SpellID).OnDelete(DeleteBehavior.Restrict);
			});

			// Creating accounts for developers to debug
			modelBuilder.Entity<Account>(entity =>
			{
				entity.HasIndex(e => e.Email).IsUnique();
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
				entity.HasData(
				new Account
				{
					AccountID = 1,
					Email = "nick@test.com",
					Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
				},
				new Account
				{
					AccountID = 2,
					Email = "alex@test.com",
					Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
				},
				new Account
				{
					AccountID = 3,
					Email = "mart@test.com",
					Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
				},
				new Account
				{
					AccountID = 4,
					Email = "marc@test.com",
					Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
				});
			});

			// Creating player entities associated with the accounts
			modelBuilder.Entity<Player>(entity =>
			{
				entity.HasIndex(e => e.PlayerName).IsUnique();
				entity.HasData(new Player
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
				},
				new Player
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
				},
				new Player
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
				},
				new Player
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
			});

			// Adding static icons
			modelBuilder.Entity<Icon>(entity =>
			{
				entity.HasData(new Icon
				{
					IconID = 1,
					IconName = "../../../../assets/player-icons/wizard1.png"
				},
				new Icon
				{
					IconID = 2,
					IconName = "../../../../assets/player-icons/wizard2.png"
				},
				new Icon
				{
					IconID = 3,
					IconName = "../../../../assets/player-icons/wizard3.png"
				},
				new Icon
				{
					IconID = 4,
					IconName = "../../../../assets/player-icons/wizard4.png"
				},
				new Icon
				{
					IconID = 5,
					IconName = "../../../../assets/player-icons/alex.png"
				},
				new Icon
				{
					IconID = 6,
					IconName = "../../../../assets/player-icons/alex-glasses.png"
				},
				new Icon
				{
					IconID = 7,
					IconName = "../../../../assets/player-icons/alex-mustache.png"
				},
				new Icon
				{
					IconID = 8,
					IconName = "../../../../assets/player-icons/alex-gangster.png"
				},
				new Icon
				{
					IconID = 9,
					IconName = "../../../../assets/player-icons/alex-impersonator.jpg"
				},
				new Icon
				{
					IconID = 10,
					IconName = "../../../../assets/player-icons/nick-gangster.png"
				},
				new Icon
				{
					IconID = 11,
					IconName = "../../../../assets/spell-icons/fireball.png"
				},
				new Icon
				{
					IconID = 12,
					IconName = "../../../../assets/spell-icons/firenova.png"
				},
				new Icon
				{
					IconID = 13,
					IconName = "../../../../assets/spell-icons/firewall.png"
				},
				new Icon
				{
					IconID = 14,
					IconName = "../../../../assets/spell-icons/teleport.png"
				},
				new Icon
				{
					IconID = 15,
					IconName = "../../../../assets/spell-icons/windslash.png"
				},
				new Icon
				{
					IconID = 16,
					IconName = "../../../../assets/spell-icons/watervortex.png"
				},
				new Icon
				{
					IconID = 17,
					IconName = "../../../../assets/spell-icons/dash.png"
				},
				new Icon
				{
					IconID = 18,
					IconName = "../../../../assets/spell-icons/placeholder-spell-icon-1.png"
				},
				new Icon
				{
					IconID = 19,
					IconName = "../../../../assets/spell-icons/placeholder-spell-icon-2.png"
				},
				new Icon
				{
					IconID = 20,
					IconName = "../../../../assets/spell-icons/placeholder-spell-icon-3.png"
				},
				new Icon
				{
					IconID = 21,
					IconName = "../../../../assets/spell-icons/placeholder-spell-icon-4.png"
				},
				new Icon
				{
					IconID = 22,
					IconName = "../../../../assets/spell-icons/placeholder-spell-icon-5.png"
				},
				new Icon
				{
					IconID = 23,
					IconName = "../../../../assets/spell-icons/placeholder-spell-icon-6.png"
				},
				new Icon
				{
					IconID = 24,
					IconName = "../../../../assets/spell-icons/placeholder-spell-icon-7.png"
				},
				new Icon
				{
					IconID = 25,
					IconName = "../../../../assets/spell-icons/placeholder-spell-icon-8.png"
				},
				new Icon
				{
					IconID = 26,
					IconName = "../../../../assets/spell-icons/rockspear.png"
				},
				new Icon
				{
					IconID = 27,
					IconName = "../../../../assets/spell-icons/invisible.png"
				});
			});

			// Creating friendships between created players
			modelBuilder.Entity<Friendship>(entity =>
			{
				entity.HasKey(x => new { x.MainPlayerID, x.FriendPlayerID });
				entity.HasOne(x => x.MainPlayer).WithMany(x => x.MainPlayerFriends).HasForeignKey(x => x.MainPlayerID).OnDelete(DeleteBehavior.Restrict);
				entity.HasOne(x => x.FriendPlayer).WithMany(x => x.Friends).HasForeignKey(x => x.FriendPlayerID).OnDelete(DeleteBehavior.Restrict);
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
				entity.HasData(new Friendship
				{
					MainPlayerID = 1,
					FriendPlayerID = 2,
					Created_At = DateTime.UtcNow,
					IsPending = false,
				},
				new Friendship
				{
					MainPlayerID = 1,
					FriendPlayerID = 3,
					Created_At = DateTime.UtcNow,
					IsPending = false,
				},
				new Friendship
				{
					MainPlayerID = 1,
					FriendPlayerID = 4,
					Created_At = DateTime.UtcNow,
					IsPending = false,
				},
				new Friendship
				{
					MainPlayerID = 2,
					FriendPlayerID = 3,
					Created_At = DateTime.UtcNow,
					IsPending = false,
				},
				new Friendship
				{
					MainPlayerID = 2,
					FriendPlayerID = 4,
					Created_At = DateTime.UtcNow,
					IsPending = false,
				},
				new Friendship
				{
					MainPlayerID = 3,
					FriendPlayerID = 4,
					Created_At = DateTime.UtcNow,
					IsPending = false,
				});
			});

			// Creating skins
			modelBuilder.Entity<SkinItem>(entity =>
			{
				entity.HasData(new SkinItem
				{
					SkinID = 1,
					SkinName = "Wise Wizard",
					SkinDescription = "A very wise wizard",
					SkinPrice = 125,
					ImageName = "../../../../assets/skin-images/wise-wizard.jpg",
				},
				new SkinItem
				{
					SkinID = 2,
					SkinName = "Evil Wizard",
					SkinDescription = "A very evil wizard",
					SkinPrice = 125,
					ImageName = "../../../../assets/skin-images/evil-wizard.jpg",
				},
				new SkinItem
				{
					SkinID = 3,
					SkinName = "Suspicious Wizard",
					SkinDescription = "A very suspicious wizard",
					SkinPrice = 125,
					ImageName = "../../../../assets/skin-images/suspicious-wizard.jpg",
				},
				new SkinItem
				{
					SkinID = 4,
					SkinName = "Robot Wizard",
					SkinDescription = "A very unhuman wizard",
					SkinPrice = 125,
					ImageName = "../../../../assets/skin-images/robot-wizard.jpg",
				});
			});

			// Creating trnasactions
			modelBuilder.Entity<Transaction>(entity =>
			{
				entity.Property(e => e.Created_At).HasDefaultValueSql("getdate()");
				entity.HasOne(x => x.SkinItem).WithMany(x => x.Transactions).HasForeignKey(x => x.SkinID).OnDelete(DeleteBehavior.Restrict);
				entity.HasOne(x => x.Player).WithMany(x => x.Transactions).HasForeignKey(x => x.PlayerID).OnDelete(DeleteBehavior.Restrict);
				entity.HasData(new Transaction
				{
					TransactionID = 1,
					SkinID = 1,
					PlayerID = 1,
					TotalCost = 125,
				});
			});

			// Creating SpellTypes
			modelBuilder.Entity<SpellType>(entity =>
			{
				entity.HasIndex(x => x.SpellTypeName).IsUnique();
				entity.HasData(new SpellType
				{
					SpellTypeID = 1,
					SpellTypeName = "Offensive"
				},
				new SpellType
				{
					SpellTypeID = 2,
					SpellTypeName = "Defensive"
				},
				new SpellType
				{
					SpellTypeID = 3,
					SpellTypeName = "Utility"
				},
				new SpellType
				{
					SpellTypeID = 4,
					SpellTypeName = "Ultimate"
				});
			});

			// Creating SpellSchools
			modelBuilder.Entity<SpellSchool>(entity =>
			{
				entity.HasData(new SpellSchool
				{
					SpellSchoolID = 1,
					SpellSchoolName = "Elemental",
				},
				new SpellSchool
				{
					SpellSchoolID = 2,
					SpellSchoolName = "Primal",
				},
				new SpellSchool
				{
					SpellSchoolID = 3,
					SpellSchoolName = "Void",
				},
				new SpellSchool
				{
					SpellSchoolID = 4,
					SpellSchoolName = "Ether",
				});
			});

			// Creating SpellSchoolCategories
			modelBuilder.Entity<SchoolCategory>(entity =>
			{
				entity.HasData(new SchoolCategory
				{
					SchoolCategoryID = 1,
					SchoolCategoryName = "Fire",
					SpellSchoolID = 1
				},
				new SchoolCategory
				{
					SchoolCategoryID = 2,
					SchoolCategoryName = "Water",
					SpellSchoolID = 1
				},
				new SchoolCategory
				{
					SchoolCategoryID = 3,
					SchoolCategoryName = "Earth",
					SpellSchoolID = 1
				},
				new SchoolCategory
				{
					SchoolCategoryID = 4,
					SchoolCategoryName = "Air",
					SpellSchoolID = 1
				},
				new SchoolCategory
				{
					SchoolCategoryID = 5,
					SchoolCategoryName = "Arcane",
					SpellSchoolID = 2
				},
				new SchoolCategory
				{
					SchoolCategoryID = 6,
					SchoolCategoryName = "Dimensional",
					SpellSchoolID = 3
				},
				new SchoolCategory
				{
					SchoolCategoryID = 7,
					SchoolCategoryName = "Dark",
					SpellSchoolID = 3
				},
				new SchoolCategory
				{
					SchoolCategoryID = 8,
					SchoolCategoryName = "Light",
					SpellSchoolID = 4
				},
				new SchoolCategory
				{
					SchoolCategoryID = 9,
					SchoolCategoryName = "Spirit",
					SpellSchoolID = 4
				});
			});

			// Creating spells 
			modelBuilder.Entity<Spell>(entity =>
			{
				entity.HasIndex(x => x.SpellName).IsUnique();
				entity.HasData(
				new Spell
				{
					SpellID = 1,
					SpellName = "Fireball",
					SpellDescription = "A ball of fire!",
					IconID = 11,
					SpellTypeID = 1,
					SchoolCategoryID = 1,
					DamageAmount = 15,
					ManaCost = 10,
					LifeTime = 4,
					CastTime = 0.8m,
				}, 
				new Spell
				{
					SpellID = 2,
					SpellName = "Fire Nova",
					SpellDescription = "Explodes a ring of fire around the wizard",
					IconID = 12,
					SpellTypeID = 1,
					SchoolCategoryID = 1,
					DamageAmount = 18,
					ManaCost = 18,
					LifeTime = 3,
					CastTime = 2,
				},
				new Spell
				{
					SpellID = 3,
					SpellName = "Fire Wall",
					SpellDescription = "A wall of fire!",
					IconID = 13,
					SpellTypeID = 1,
					SchoolCategoryID = 1,
					DamageAmount = 75,
					ManaCost = 30,
					LifeTime = 8,
					CastTime = 0.15m,
				},
				new Spell
				{
					SpellID = 4,
					SpellName = "Water Vortex",
					SpellDescription = "A water vortex is created at the target location.",
					IconID = 16,
					SpellTypeID = 1,
					SchoolCategoryID = 2,
					DamageAmount = 16,
					ManaCost = 12,
					LifeTime = 0.8m,
					CastTime = 0.5m,
				},
				new Spell
				{
					SpellID = 5,
					SpellName = "Rock Spear",
					SpellDescription = "Throw a spear made of solid rock that stuns on impact.",
					IconID = 26,
					SpellTypeID = 1,
					SchoolCategoryID = 3,
					DamageAmount = 12,
					ManaCost = 10,
					LifeTime = 4,
					CastTime = 0.75m,
				},
				new Spell
				{
					SpellID = 6,
					SpellName = "Wind Slash",
					SpellDescription = "Send out a slash of wind that damages enemies and speeds up the caster.",
					IconID = 15,
					SpellTypeID = 1,
					SchoolCategoryID = 4,
					DamageAmount = 6,
					ManaCost = 5,
					LifeTime = 2,
					CastTime = 0.2m,
				},
				new Spell
				{
					SpellID = 7,
					SpellName = "Dash",
					SpellDescription = "Dash a short distance.",
					IconID = 17,
					SpellTypeID = 3,
					SchoolCategoryID = 5,
					DamageAmount = 0,
					ManaCost = 10,
					LifeTime = 1,
					CastTime = 0.1m,
				},
				new Spell
				{
					SpellID = 8,
					SpellName = "Invisible",
					SpellDescription = "Makes you invisible to the naked eye.",
					IconID = 27,
					SpellTypeID = 1,
					SchoolCategoryID = 9,
					DamageAmount = 0,
					ManaCost = 25,
					LifeTime = 1,
					CastTime = 0.75m,
				},
				new Spell
				{
					SpellID = 9,
					SpellName = "Teleport",
					SpellDescription = "Teleport to a location instantly.",
					IconID = 14,
					SpellTypeID = 1,
					SchoolCategoryID = 5,
					DamageAmount = 0,
					ManaCost = 20,
					LifeTime = 1,
					CastTime = 0.5m,
				});
			});

			// Creating SpellBooks for players
			modelBuilder.Entity<SpellBook>(entity =>
			{
				entity.HasData(new SpellBook
				{
					SpellBookID = 1,
					SpellBookName = "Unnamed Spellbook 1",
					PlayerID = 1,
				},
				new SpellBook
				{
					SpellBookID = 2,
					SpellBookName = "Unnamed Spellbook 2",
					PlayerID = 1,
				},
				new SpellBook
				{
					SpellBookID = 3,
					SpellBookName = "Unnamed Spellbook 3",
					PlayerID = 1,
				},
				new SpellBook
				{
					SpellBookID = 4,
					SpellBookName = "Unnamed Spellbook 1",
					PlayerID = 2,
				},
				new SpellBook
				{
					SpellBookID = 5,
					SpellBookName = "Unnamed Spellbook 2",
					PlayerID = 2,
				},
				new SpellBook
				{
					SpellBookID = 6,
					SpellBookName = "Unnamed Spellbook 3",
					PlayerID = 2,
				},
				new SpellBook
				{
					SpellBookID = 7,
					SpellBookName = "Unnamed Spellbook 1",
					PlayerID = 3,
				},
				new SpellBook
				{
					SpellBookID = 8,
					SpellBookName = "Unnamed Spellbook 2",
					PlayerID = 3,
				},
				new SpellBook
				{
					SpellBookID = 9,
					SpellBookName = "Unnamed Spellbook 3",
					PlayerID = 3,
				},
				new SpellBook
				{
					SpellBookID = 10,
					SpellBookName = "Unnamed Spellbook 1",
					PlayerID = 4,
				},
				new SpellBook
				{
					SpellBookID = 11,
					SpellBookName = "Unnamed Spellbook 2",
					PlayerID = 4,
				},
				new SpellBook
				{
					SpellBookID = 12,
					SpellBookName = "Unnamed Spellbook 3",
					PlayerID = 4,
				});
			});
		}
	}
}
