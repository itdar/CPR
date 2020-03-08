using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.Data;

namespace NoName.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        public string CurrentPage { get; set; }

        public IndexModel()
        {

        }

        public void GetPage(string currentPage)
        {

        }

        private async Task LoadAsync(ApplicationUser user)
        {

        }

        public async Task<IActionResult> OnGetAsync()
        {
            return null;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return null;
        }
    }
}
