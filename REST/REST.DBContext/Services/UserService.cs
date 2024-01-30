using Microsoft.EntityFrameworkCore;
using REST.Database.Context;
using REST.Database.Models;

namespace REST.Database.Services
{
    public class UserService : IUserService
    {
        public UserContext _userContext;

        public UserService(UserContext userContext) 
        {
            _userContext = userContext;
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
    }
}
