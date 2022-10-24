namespace Wizard_Battle_Web_API.Repositories
{
	public interface IAuthenticationRepository
	{
		Task<AuthenticationResponse> Authenticate(string email, string password, string ipAddress);
		Task<AuthenticationResponse> RefreshToken(string token, string ipAddress);
		Task<bool> RevokeToken(string token, string ipAddress);
	}

	public class AuthenticationRepository : IAuthenticationRepository
	{
        private readonly DatabaseContext m_context;
        private readonly AppSettings m_appSettings;

        public AuthenticationRepository(DatabaseContext context, IOptions<AppSettings> appSettings)
        {
            m_appSettings = appSettings.Value;
            m_context = context;
        }


        /// <summary>
        /// Checking for the clients email & password and if you have a JWTToken and a RefreshToken.
        /// </summary>
        public async Task<AuthenticationResponse> Authenticate(string email, string password, string ipAddress)
        {
            Account account = await m_context.Account.Include(e => e.Player).FirstOrDefaultAsync(x => x.Email == email);

            if (account == null)
            {
                return null;
            }

            if (!BC.Verify(password, account.Password))
            {
                return null;
            }

            string accessToken = JWTHandler.GenerateJWTToken(account, m_appSettings);
            RefreshToken refreshToken = JWTHandler.GenerateRefreshToken(ipAddress);

            account.RefreshTokens.Add(refreshToken);

            m_context.Update(account);
            await m_context.SaveChangesAsync();

            return new AuthenticationResponse(refreshToken.Token, accessToken);
        }

        /// Crates a RefreshToken
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ipAddress"></param>
        /// <returns>AuthenticationResponse (newRefreshToken.Token, accessToken)</returns>
        public async Task<AuthenticationResponse> RefreshToken(string token, string ipAddress)
        {
            Account account = await m_context.Account.Include(e => e.RefreshTokens).Include(e => e.Player).FirstOrDefaultAsync(c => c.RefreshTokens.Any(t => t.Token == token));

            if (account == null)
            {
                return null;
            }

            RefreshToken refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                return null;
            }

            RefreshToken newRefreshToken = JWTHandler.GenerateRefreshToken(ipAddress);

            refreshToken.Revoked_At = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            account.RefreshTokens.Add(newRefreshToken);

            m_context.Update(account);
            m_context.Update(refreshToken);
            await m_context.SaveChangesAsync();

            string accessToken = JWTHandler.GenerateJWTToken(account, m_appSettings);

            return new AuthenticationResponse(newRefreshToken.Token, accessToken);
        }

        /// <summary>
        /// Updates the refreshToken so you can stay logged in, if the account exists and if refreshtoken is not active.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ipAddress"></param>
        /// <returns>true</returns>
        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            Account account = await m_context.Account.Include(e => e.RefreshTokens).SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (account == null)
            {
                return false;
            }

            RefreshToken refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
            {
                return false;
            }

            refreshToken.Revoked_At = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            m_context.Update(account);

            await m_context.SaveChangesAsync();

            return true;
        }
    }
}
