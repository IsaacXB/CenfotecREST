using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using REST.Database.Context;
using REST.Database.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace REST.Database.Services
{
    public class UserService : IUserService
    {
        public UserContext _userContext;
        private readonly AppSettings _appSettings;


        public UserService(UserContext userContext) 
        {
            _userContext = userContext;

        }

        public UserService(UserContext userContext, IOptions<AppSettings> appSettings)
        {
            _userContext = userContext;
            _appSettings = appSettings.Value;

        }

        public void DeleteUser(int id)
        {
            var userToBeDeleted = _userContext.Users.FirstOrDefault(x => x.Id == id);
            if (userToBeDeleted != null)
            {
                _userContext.Remove(userToBeDeleted);
                _userContext.SaveChanges();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var userToBeDeleted = await _userContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userToBeDeleted != null)
            {
                _userContext.Remove(userToBeDeleted);
                await _userContext.SaveChangesAsync();
            }
        }

        public User GetUserById(int id)
        {
           return _userContext.Users.FirstOrDefault(x => x.Id == id); 
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userContext.Users;
        }

        public void PostUser(User user)
        {
            _userContext.Users.Add(user);
            _userContext.SaveChanges(); 
        }

        public async Task PostUserAsync(User user)
        {
            await _userContext.Users.AddAsync(user);
            await _userContext.SaveChangesAsync();
        }

        public void PutUser(User user)
        {
            var userToBeUpdated = _userContext.Users.FirstOrDefault(x => x.Id == user.Id );
            if (userToBeUpdated != null) 
            {
                _userContext.Entry(userToBeUpdated).CurrentValues.SetValues(user);
                _userContext.SaveChanges();
            }

        }

        public async Task PutUserAsync(User user)
        {
            var userToBeUpdated = await _userContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (userToBeUpdated != null)
            {
                _userContext.Entry(userToBeUpdated).CurrentValues.SetValues(user);
                await _userContext.SaveChangesAsync();

            }
        }

        public bool ValidateUser(string userId, string password)
        {
            return _userContext.Users.Where(x => x.Name.Equals(userId) && x.Password.Equals(password)).Count() > 0;   
        }

        public User? GetUserByName(string userName)
        {
            return _userContext.Users.FirstOrDefault(x => x.Name == userName);
        }

        public AuthResponse Authenticate(AuthRequest data)
        {

            AuthResponse authResponse = new AuthResponse();

            var usuario = _userContext.Users.FirstOrDefault(u =>
                u.Name == data.userID && u.Password == data.Password);

            if (usuario == null)
            {
                return null;

            }

            authResponse.userID = usuario.Name;
            // Token
            authResponse.Token = GetTokenKey(usuario);
            return authResponse;
        }

        private string GetTokenKey(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, user.Name.ToString()),
                        new Claim(ClaimTypes.Name, user.Name)
                       }
                    ),
                // Expira en 60 días
                Expires = DateTime.UtcNow.AddDays(30),
                // Encrypta la información
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }
    }
}
