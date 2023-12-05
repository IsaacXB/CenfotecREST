using REST.API.Models;
using System.Net.Http.Json;
using System.Text.Json;

var url = "https://localhost:7190/api/User/GetUsers/";
var url2 = "https://localhost:7190/api/User/GetUser/";
var url3 = "https://localhost:7190/api/User/";


JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

using (var client = new HttpClient())
{
    // GetUsers (HttpGet)
    var response = client.GetAsync(url).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        var users = JsonSerializer.Deserialize<List<User>>(content, options);

        foreach (var user in users)
        {
            Console.WriteLine("User Id: " + user.Id + ", Nombre: " + user.Name + ", Email: " + user.Email);
        }
    }
    else
    { Console.WriteLine(response.StatusCode);  }

    
    // Get specific user (Id = 2)
    response = client.GetAsync(url2 + 2).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        var user = JsonSerializer.Deserialize<User>(content, options);
        
        if (user != null)
        Console.WriteLine("User Id: " + user.Id + ", Nombre: " + user.Name + ", Email: " + user.Email);
    }
    else
    { Console.WriteLine(response.StatusCode); }


    // Add user (HttpPost)
    User user1 = new User()
    {
        Name = "Test",
        Email = "Test@gmail.com",
        EmailConfirmed  = "No",
        Password = "TestPa$$word"
    };

    response = client.PostAsJsonAsync(url3, user1).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        var user = JsonSerializer.Deserialize<User>(content, options);

        if (user != null)
            Console.WriteLine("New User Added successfully. Id: " + user.Id + ", Nombre: " + user.Name + ", Email: " + user.Email);
    }
    else
    { Console.WriteLine(response.StatusCode); }


    // Update User (HttpPut)
    User user2 = new User()
    {
        Name = "Test2",
        Email = "Test2@gmail.com",
        EmailConfirmed = "No",
        Password = "Test"
    };

    response = client.PutAsJsonAsync(url3 + 1, user2).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        user2 = JsonSerializer.Deserialize<User>(content, options);

        if (user2 != null)
            Console.WriteLine("User Updated successfully. Id: " + user2.Id + ", Nombre: " + user2.Name + ", Email: " + user2.Email);
    }
    else
    { Console.WriteLine(response.StatusCode); }

    // Delete User (HttpDelete)

    response = client.DeleteAsync(url3 + 1).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        var user3 = JsonSerializer.Deserialize<User>(content, options);

        if (user3 != null && user3.Id ==0)
            Console.WriteLine("User Deleted successfully. Id: " + 1);
    }
    else
    { Console.WriteLine(response.StatusCode); }
}

Console.WriteLine("---------Homework 1 ------------");

url = "https://localhost:7190/api/Book/GetBooks/";
url2 = "https://localhost:7190/api/Book/GetBook/";
url3 = "https://localhost:7190/api/Book/";


using (var client = new HttpClient())
{
    // GetBooks (HttpGet)
    var response = client.GetAsync(url).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        var books = JsonSerializer.Deserialize<List<Book>>(content, options);

        foreach (var book in books)
        {
            Console.WriteLine("Book Id: " + book.Id + ", Name: " + book.Name + ", Author: " + book.Author + "Description: " + book.Description);
        }
    }
    else
    { Console.WriteLine(response.StatusCode); }


    // Get specific book (Id = 2)
    response = client.GetAsync(url2 + 2).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        var book = JsonSerializer.Deserialize<Book>(content, options);

        if (book != null)
            Console.WriteLine("Book Id: " + book.Id + ", Name: " + book.Name + ", Author: " + book.Author + "Description: " + book.Description);
    }
    else
    { Console.WriteLine(response.StatusCode); }


    // Add book (HttpPost)
    Book book1 = new Book
    {
        Id = 0,
        Name = "Harry Potter and the Chamber of Secrets, Book 2",
        Author = "J.K. Rowling",
        Description = "There is a plot, Harry Potter. A plot to make most terrible things happen at Hogwarts School of Witchcraft and Wizardry this year"
    };

    response = client.PostAsJsonAsync(url3, book1).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        var user = JsonSerializer.Deserialize<Book>(content, options);

        if (user != null)
            Console.WriteLine("New Book Added successfully. Book Id: " + book1.Id + ", Name: " + book1.Name + ", Author: " + book1.Author + "Description: " + book1.Description);

    }
    else
    { Console.WriteLine(response.StatusCode); }


    // Update Book (HttpPut)

    response = client.PutAsJsonAsync(url3 + 1, book1).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        book1 = JsonSerializer.Deserialize<Book>(content, options);

        if (book1 != null)
            Console.WriteLine("New Book Updated successfully. Book Id: " + book1.Id + ", Name: " + book1.Name + ", Author: " + book1.Author + "Description: " + book1.Description);

    }
    else
    { Console.WriteLine(response.StatusCode); }

    // Delete Book (HttpDelete)

    response = client.DeleteAsync(url3 + 1).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        var book2= JsonSerializer.Deserialize<Book>(content, options);

        if (book2 != null && book2.Id == 0)
            Console.WriteLine("User Deleted successfully. Id: " + 1);
    }
    else
    { Console.WriteLine(response.StatusCode); }
    Console.ReadLine();

}