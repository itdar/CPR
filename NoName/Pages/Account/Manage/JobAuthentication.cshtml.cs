using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.Data.DbUser;

namespace NoName.Pages.Account.Manage
{
    public class JobAuthenticationModel : PageModel
    {
        private readonly Data.UserContext _context;

        [BindProperty]
        public TableUserJob UserJob { get; set; }

        public JobAuthenticationModel(Data.UserContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            //_context.User;//Add(UserJob);
            //await _context.SaveChangesAsync();

            //return RedirectToPage("./Index");
        }
    }
}
