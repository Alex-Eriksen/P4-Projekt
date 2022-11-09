namespace Wizard_Battle_Web_API.DTOs.Transaction
{
	public class StaticTransactionResponse
	{
		public int TransactionID { get; set; } = 0;

		public int PlayerID { get; set; } = 0;

		public int SkinItemID { get; set; } = 0;

		public int TotalCost = 0;

		public DateTime Created_At { get; set; }
	}
}
