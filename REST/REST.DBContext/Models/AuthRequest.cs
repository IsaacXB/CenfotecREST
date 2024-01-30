using System.ComponentModel.DataAnnotations;

namespace REST.Database.Models
{
    public class AuthRequest
    {
        [Required]
        public string userID { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
