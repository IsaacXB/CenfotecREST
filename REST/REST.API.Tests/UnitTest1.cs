

using System.Net.Http.Json;

namespace REST.API.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task CanGetUsersAsync()
        {
            var url = "https://localhost:7190/api/User/GetUsers/";

            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var client = new HttpClient())
            {
                // GetUsers
                var response = client.GetAsync(url).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);

                var content = await response.Content.ReadAsStringAsync();

                var users = JsonSerializer.Deserialize<List<User>>(content, options);

                Assert.IsNotNull(users);
                Assert.IsTrue(users.Count > 0);

            }
        }

        [TestMethod]
        [DataRow(2)]
        public async Task CanGetUserByIdAsync(int id)
        {
            var url = "https://localhost:7190/api/User/GetUser/";

            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var client = new HttpClient())
            {
                // GetUsers
                var response = client.GetAsync(url + id).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);

                var content = await response.Content.ReadAsStringAsync();

                var users = JsonSerializer.Deserialize<User>(content, options);

                Assert.IsNotNull(users);

            }
        }

        [TestMethod]
        public async Task CanPostUserAsync()
        {
            var url = "https://localhost:7190/api/User/";

            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var client = new HttpClient())
            {
                User user = new User()
                {
                    Name = "Test",
                    Email = "Test@gmail.com",
                    EmailConfirmed = "No",
                    Password = "Test"
                };

                var response = client.PostAsJsonAsync(url, user).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    user = JsonSerializer.Deserialize<User>(content, options);

                    if (user != null)
                        Console.WriteLine("New User Added successfully. Id: " + user.Id + ", Nombre: " + user.Name + ", Email: " + user.Email);
                }
                else
                { Console.WriteLine(response.StatusCode); }

                Assert.IsTrue(response.IsSuccessStatusCode);


            }
        }

        [TestMethod]
        public async Task CanPutUserAsync()
        {
            var url = "https://localhost:7190/api/User/";

            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var client = new HttpClient())
            {
                User user = new User()
                {
                    Name = "Test",
                    Email = "Test@gmail.com",
                    EmailConfirmed = "No",
                    Password = "Test"
                };

                var response = client.PutAsJsonAsync(url + 1, user).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    user = JsonSerializer.Deserialize<User>(content, options);

                    if (user != null)
                        Console.WriteLine("User Updated successfully. Id: " + user.Id + ", Nombre: " + user.Name + ", Email: " + user.Email);
                }
                else
                { Console.WriteLine(response.StatusCode); }

                Assert.IsTrue(response.IsSuccessStatusCode && user != null);
            }
        }

        [TestMethod]
        [DataRow(1)]
        public async Task CanDeleteUserAsync(int id)
        {
            var url = "https://localhost:7190/api/User/";

            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var client = new HttpClient())
            {
                // Delete User (HttpDelete)

                var response = client.DeleteAsync(url + id).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);
            }
        }


    }
}