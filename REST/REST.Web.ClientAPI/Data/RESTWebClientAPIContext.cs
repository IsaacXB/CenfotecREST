
using System.Net;
using System.Security.Policy;
using System.Text.Json;

using REST.Database.Models;

namespace REST.Web.ClientAPI.Data
{
    public class RESTWebClientAPIContext
    {
        static string _apiUrl = "https://localhost:7117/api/users";
        JsonSerializerOptions _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
        public RESTWebClientAPIContext ()
        {

        }

        public async Task GetUsersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(_apiUrl).Result;
                if (response != null && response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    User = JsonSerializer.Deserialize<List<User>>(content, _serializerOptions);
                }
            };

        }

        public async Task<User> GetAsync(int? id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(_apiUrl + "/" + id).Result;
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var contenido = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<User>(contenido, _serializerOptions);
                    }
                }
            }
            return null;
        }

        public async Task<Uri> PostAsync(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(_apiUrl, user, _serializerOptions);
                response.EnsureSuccessStatusCode();
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Headers.Location;
                    }
                }
            }
            return null;
        }

        public async Task<HttpStatusCode> DeleteAsync(int? id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync(_apiUrl + "/" + id);
                return response.StatusCode;
            }
        }

        public async Task<Uri> PutAsync(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PutAsJsonAsync(_apiUrl + "/" + user.Id, user);
                response.EnsureSuccessStatusCode();
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Headers.Location;
                    }
                }
            }
            return null;
        }

        public List<User> User { get; set; } = default!;
    }
}
