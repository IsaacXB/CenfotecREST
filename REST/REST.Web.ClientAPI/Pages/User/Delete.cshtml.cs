using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using REST.Web.ClientAPI.Data;

namespace REST.Web.ClientAPI.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly REST.Web.ClientAPI.Data.RESTWebClientAPIContext _context = new RESTWebClientAPIContext();

        [BindProperty]
      public REST.Database.Models.User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var user = await _context.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }
            var user = await _context.GetAsync(id);

            if (user != null)
            {
                await _context.DeleteAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
