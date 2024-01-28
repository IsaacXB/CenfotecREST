
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using REST.Web.ClientAPI.Data;

namespace REST.Web.ClientAPI.Pages.User
{
    public class EditModel : PageModel
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

            var user =  await _context.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.PutAsync(User);
            return RedirectToPage("./Index");
        }
    }
}
