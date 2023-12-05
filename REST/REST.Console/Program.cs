using REST.API.Models;
using System.Text.Json;

var url = "https://localhost:7190/api/User";

using (var client = new HttpClient())
{
    var response = client.GetAsync(url).Result;

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();

        var users = JsonSerializer.Deserialize<List<User>>(content);

        foreach (var user in users)
        {
            Console.WriteLine("User Id: " + user.Id + ", Nombre: " + user.Name + ", Email: " + user.Email);
        }
    }
}