using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.Board
{
    public class DetailModel : PageModel
    {

        private readonly DataContext _context;

        public DetailModel(DataContext context)
        {
            _context = context;
        }

        public TablePost TablePost { get; set; }

        public async Task<IActionResult> OnGetAsync(int? postNumber)
        {
            if (postNumber == null)
            {
                return NotFound();
            }

            TablePost = await _context.Post.FirstOrDefaultAsync(m => m.PostNumber == postNumber);

            if (TablePost == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
