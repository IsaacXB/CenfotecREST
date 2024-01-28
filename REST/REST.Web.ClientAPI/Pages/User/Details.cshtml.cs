using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using REST.Database.Models;
using REST.Web.ClientAPI.Data;

namespace REST.Web.ClientAPI.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly REST.Web.ClientAPI.Data.RESTWebClientAPIContext _context = new RESTWebClientAPIContext();

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

    }
}
