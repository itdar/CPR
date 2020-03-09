using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.Board
{
    public class IndexModel : PageModel
    {
        private readonly NoName.Data.DbData.DataContext _context;

        public  IndexModel(NoName.Data.DbData.DataContext context) {
            _context = context;
        }
        [BindProperty]
        public TablePost TablePost { get; set; }

        public IList<TablePost> Board { get; set; }
        public async void OnGetAsync()
        {
            Board = await _context.Post.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //DataDbManager.GetInstance().DataDB. 

            _context.Post.Add(TablePost);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
