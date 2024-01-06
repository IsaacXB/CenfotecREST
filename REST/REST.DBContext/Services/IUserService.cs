using REST.Database.Models;

namespace REST.Database.Services
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();

        public User GetUserById(int id);

        public void PostUser(User user);

        public void PutUser(User user);

        public void DeleteUser(int id);
    }
}
