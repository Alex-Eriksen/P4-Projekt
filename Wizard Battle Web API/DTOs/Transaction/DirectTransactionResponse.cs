namespace Wizard_Battle_Web_API.DTOs.Transaction
{
	public class DirectTransactionResponse
	{
		public int TransactionID { get; set; } = 0;

		public StaticPlayerResponse Player { get; set; } = null!;

		public StaticSkinItemResponse SkinItemID { get; set; } = null!;

		public int TotalCost = 0;

		public DateTime Created_At { get; set; }
	}
}
