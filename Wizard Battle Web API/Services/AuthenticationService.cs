namespace Wizard_Battle_Web_API.Services
{
	public interface IAuthenticationService
	{
		Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, string ipAddress);
		Task<AuthenticationResponse> RefreshToken(string token, string ipAddress);
		Task<bool> RevokeToken(string token, string ipAddress);
	}

	/// <summary>
	/// AuthenticationService is used to transfer data to and from m_authenticationRepository and AuthenticationController.
	/// </summary>
	public class AuthenticationService : IAuthenticationService
	{
        private readonly IAuthenticationRepository m_authenticationRepository;
        private readonly IMapper m_mapper;

        /// <summary>
        /// Constructor for AuthenticationService.
        /// </summary>
        /// <param name="accountRepository"></param>
        /// <param name="mapper"></param>
        public AuthenticationService(IAuthenticationRepository authenticationRepository, IMapper mapper)
        {
            m_mapper = mapper;
            m_authenticationRepository = authenticationRepository;
        }

        /// <summary>
        /// Making a response.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ipAddress"></param>
        /// <returns>authenticationResponse or null</returns>
        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, string ipAddress)
        {
            AuthenticationResponse authenticationResponse = await m_authenticationRepository.Authenticate(request.Email, request.Password, ipAddress); // Gets AuthenticationResponse from Repository

            if (authenticationResponse != null)
            {
                return authenticationResponse;
            }

            return null;
        }

        /// <summary>
        /// Making a response token.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ipAddress"></param>
        /// <returns>authenticationResponse or null</returns>
        public async Task<AuthenticationResponse> RefreshToken(string token, string ipAddress)
        {
            AuthenticationResponse authenticationResponse = await m_authenticationRepository.RefreshToken(token, ipAddress);

            if (authenticationResponse != null)
            {
                return authenticationResponse;
            }

            return null;
        }

        /// <summary>
        /// Revokes the token.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ipAddress"></param>
        /// <returns>token, ipAddress</returns>
        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            return await m_authenticationRepository.RevokeToken(token, ipAddress);
        }
    }
}
