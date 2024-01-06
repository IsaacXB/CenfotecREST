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

        public User GetUserById(int id)
        {
           return _userContext.Users.FirstOrDefault(x => x.Id == id); 
        }

        public IEnumerable<User> GetUsers()
        {
            return (IEnumerable<User>)_userContext.Users;
        }

        public void PostUser(User user)
        {
            _userContext.Users.Add(user);
            _userContext.SaveChanges(); 
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
    }
}
