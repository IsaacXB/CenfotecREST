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


        // Async methods
        public Task<User> GetUserByIdAsync(int id);

        public Task PostUserAsync(User user);

        public Task PutUserAsync(User user);

        public Task DeleteUserAsync(int id);

        public bool ValidateUser(string userId, string password);

        public User? GetUserByName(string userName);

    }
}
