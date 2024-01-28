
using Microsoft.AspNetCore.Mvc.RazorPages;
using REST.Database.Models;


namespace REST.Web.ClientAPI.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly REST.Web.ClientAPI.Data.RESTWebClientAPIContext _context = new REST.Web.ClientAPI.Data.RESTWebClientAPIContext();


        public IList<REST.Database.Models.User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context != null)
            {
                await _context.GetUsersAsync();
                if (_context.User != null)
                {
                    User = _context.User;
                }
            }
        }
    }
}
