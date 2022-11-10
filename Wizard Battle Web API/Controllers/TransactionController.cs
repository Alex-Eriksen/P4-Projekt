namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly ITransactionService m_transactionService;

		public TransactionController(ITransactionService transactionService)
		{
			m_transactionService = transactionService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				List<StaticTransactionResponse> transactions = await m_transactionService.GetAll();
				if(transactions == null)
				{
					return NotFound();
				}
				
				if(transactions.Count == 0)
				{
					return NoContent();
				}

				return Ok(transactions);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				DirectTransactionResponse transaction = await m_transactionService.GetById(id);
				if(transaction == null)
				{
					return BadRequest();
				}

				return Ok(transaction);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create(TransactionRequest request)
		{
			try
			{
				DirectTransactionResponse transaction = await m_transactionService.Create(request);
				if (transaction == null)
				{
					return BadRequest();
				}

				return Ok(transaction);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, TransactionRequest request)
		{
			try
			{
				DirectTransactionResponse transaction = await m_transactionService.Update(id, request);
				if (transaction == null)
				{
					return BadRequest();
				}

				return Ok(transaction);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				DirectTransactionResponse transaction = await m_transactionService.Delete(id);
				if (transaction == null)
				{
					return BadRequest();
				}

				return Ok(transaction);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
