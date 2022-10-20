namespace Wizard_Battle_Web_API.Database.Entities
{
	public class PlayerStats
	{
		[Key]
		public int PlayerStatsID { get; set; }

		public int ElementalRes { get; set; }

		public int PrimalRes { get; set; }

		public int VoidRes { get; set; }

		public int EtherRes { get; set; }

		public Player Player { get; set; }
	}
}
