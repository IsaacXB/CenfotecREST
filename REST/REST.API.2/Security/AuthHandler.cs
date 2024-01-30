using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
//using Database.Services;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using REST.Database.Services;

namespace REST.API._2.Security
{
    public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;
        public AuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization data not found.");
            }

            bool result = false;

            var userName = "";
            var password = "";

            try
            {
                var auth = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(auth.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                userName = credentials[0];
                password = credentials[1];
                result = _userService.ValidateUser(userName, password);
            }
            catch
            {
                return AuthenticateResult.Fail("An error has occurred when validating user.");
            }

            if (result)
            {
                var usu = _userService.GetUserByName(userName);

                if (usu != null) {

                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userName),
                        new Claim(ClaimTypes.Name, usu.Name),
                        new Claim(ClaimTypes.Email, usu.Email)
                    };


                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }

                else
                {
                    return AuthenticateResult.Fail("An error ocurred when loading user data.");
                }

            }
            else
            {
                return AuthenticateResult.Fail("User name and/or password are incorrect.");
            }

        }

    }
}
