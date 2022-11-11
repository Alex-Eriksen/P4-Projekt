namespace Wizard_Battle_Web_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		/// <summary>
		/// Using IAccountService as interface.
		/// </summary>
		private readonly IAccountService m_accountService;


		/// <summary>
		/// Constructor for AccountController
		/// </summary>
		/// <param name="accountService"></param>
		public AccountController(IAccountService accountService)
		{
			m_accountService = accountService;
		}

		/// <summary>
		/// Gets all Accounts.
		/// </summary>
		/// <returns>Accounts, HTTP 204 or exception</returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				List<StaticAccountResponse> accounts = await m_accountService.GetAll();

				if (accounts == null)
				{
					return Problem("Nothing was returned from service, this was unexpected");
				}

				if (accounts.Count == 0)
				{
					return NoContent();
				}

				return Ok(accounts);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		/// <summary>
		/// Gets Account by its id.
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns>Account or HTTP 204</returns>
		[HttpGet]
		[Route("{accountId}")]
		public async Task<IActionResult> GetById(int accountId)
		{
			try
			{
				DirectAccountResponse account = await m_accountService.GetById(accountId);

				if (account == null)
				{
					return NotFound();
				}

				return Ok(account);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		/// <summary>
		/// Gets Account by Active Token.
		/// </summary>
		/// <param name="Token"></param>
		/// <returns>Account or HTTP 204</returns>
		[HttpGet("getbytoken/{Token}")]
		public async Task<IActionResult> GetByToken(string Token)
		{
			try
			{
				DirectAccountResponse account = await m_accountService.GetByToken(Token);

				if (account == null)
				{
					return NotFound();
				}

				return Ok(account);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		/// <summary>
		/// Updates a Account.
		/// </summary>
		/// <param name="accountId"></param>
		/// <param name="request"></param>
		/// <returns>Account or exception</returns>
		[HttpPut]
		[Route("{accountId}")]
		[Authorize]
		public async Task<IActionResult> Update(int accountId, AccountRequest request)
		{
			try
			{
				DirectAccountResponse account = await m_accountService.Update(accountId, request);

				if (account == null)
				{
					return NotFound();
				}

				return Ok(account);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
