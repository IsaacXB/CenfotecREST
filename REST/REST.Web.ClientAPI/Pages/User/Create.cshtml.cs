using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using REST.Web.ClientAPI.Data;

namespace REST.Web.ClientAPI.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly REST.Web.ClientAPI.Data.RESTWebClientAPIContext _context = new RESTWebClientAPIContext();

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public REST.Database.Models.User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context == null || User == null)
            {
                return Page();
            }

            await _context.PostAsync(User);

            return RedirectToPage("./Index");
        }
    }
}
