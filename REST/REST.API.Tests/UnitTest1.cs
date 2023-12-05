

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





        [TestMethod]
        public async Task CanGetBooksAsync()
        {
            var url = "https://localhost:7190/api/Book/GetBooks/";

            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var client = new HttpClient())
            {
                // Get Books
                var response = client.GetAsync(url).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);

                var content = await response.Content.ReadAsStringAsync();

                var books = JsonSerializer.Deserialize<List<Book>>(content, options);

                Assert.IsNotNull(books);
                Assert.IsTrue(books.Count > 0);

            }
        }

        [TestMethod]
        [DataRow(2)]
        public async Task CanGetBookByIdAsync(int id)
        {
            var url = "https://localhost:7190/api/Book/GetBook/";

            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var client = new HttpClient())
            {
                // Get Book
                var response = client.GetAsync(url + id).Result;

                Assert.IsTrue(response.IsSuccessStatusCode);

                var content = await response.Content.ReadAsStringAsync();

                var book = JsonSerializer.Deserialize<Book>(content, options);

                Assert.IsNotNull(book);

            }
        }

        [TestMethod]
        public async Task CanPostBookAsync()
        {
            var url = "https://localhost:7190/api/Book/";

            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var client = new HttpClient())
            {
                Book book1 = new Book
                {
                    Id = 0,
                    Name = "Harry Potter and the Chamber of Secrets, Book 2",
                    Author = "J.K. Rowling",
                    Description = "There is a plot, Harry Potter. A plot to make most terrible things happen at Hogwarts School of Witchcraft and Wizardry this year"
                };

                var response = client.PostAsJsonAsync(url, book1).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    book1 = JsonSerializer.Deserialize<Book>(content, options);

                    if (book1 != null)
                        Console.WriteLine("New User Added successfully. Id: " + book1.Id + ", Nombre: " + book1.Name + ", Description: " + book1.Description);
                }
                else
                { Console.WriteLine(response.StatusCode); }

                Assert.IsTrue(response.IsSuccessStatusCode);


            }
        }

        [TestMethod]
        public async Task CanPutBookAsync()
        {
            var url = "https://localhost:7190/api/Book/";

            JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            using (var client = new HttpClient())
            {
                Book book1 = new Book
                {
                    Id = 0,
                    Name = "Harry Potter and the Chamber of Secrets, Book 2",
                    Author = "J.K. Rowling",
                    Description = "There is a plot, Harry Potter. A plot to make most terrible things happen at Hogwarts School of Witchcraft and Wizardry this year"
                };

                var response = client.PutAsJsonAsync(url + 1, book1).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    book1 = JsonSerializer.Deserialize<Book>(content, options);

                    if (book1 != null)
                        Console.WriteLine("User Updated successfully. Id: " + book1.Id + ", Nombre: " + book1.Name + ", Description: " + book1.Description);
                }
                else
                { Console.WriteLine(response.StatusCode); }

                Assert.IsTrue(response.IsSuccessStatusCode && book1 != null);
            }
        }

        [TestMethod]
        [DataRow(1)]
        public async Task CanDeleteBookAsync(int id)
        {
            var url = "https://localhost:7190/api/Book/";

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