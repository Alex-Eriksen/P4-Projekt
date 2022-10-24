using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Wizard_Battle_Web_API.Helpers
{
	/// <summary>
	/// Handles JWT Tokens
	/// </summary>
	public class JWTHandler
    {
		/// <summary>
		/// Generates a Refresh Token
		/// </summary>
		/// <param name="ipAddress"></param>
		/// <returns>RefreshToken</returns>
		public static RefreshToken GenerateRefreshToken( string ipAddress )
        {
            // Uses RandomNumberGenerator to generate the token
            using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create()) 
            {
                byte[] randomBytes = new byte[64];
                randomNumberGenerator.GetBytes(randomBytes );
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes ),
                    Expires_At = DateTime.UtcNow.AddDays( 7 ),
                    Created_At = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

        /// <summary>
        /// Generates a JWT Token
        /// </summary>
        /// <param name="account"></param>
        /// <param name="appSettings"></param>
        /// <returns>token</returns>
        public static string GenerateJWTToken(Account account, AppSettings appSettings)
        {
            // Uses JwtSecurityTokenHandler to make it more secure
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Issuer = "WizardBattle",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.AccountID.ToString()),
                    new Claim(ClaimTypes.Email, account.Email.ToString()),
                }),
                Audience = "user",
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
