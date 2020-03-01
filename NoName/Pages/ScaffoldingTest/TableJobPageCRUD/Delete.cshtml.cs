using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data.DbData;

namespace NoName.Pages.ScaffoldingTest.TableJobPageCRUD
{
    public class DeleteModel : PageModel
    {
        private readonly NoName.Data.DbData.DataContext _context;

        public DeleteModel(NoName.Data.DbData.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TableJobPage TableJobPage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TableJobPage = await _context.JobPage.FirstOrDefaultAsync(m => m.JobPageId == id);

            if (TableJobPage == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TableJobPage = await _context.JobPage.FindAsync(id);

            if (TableJobPage != null)
            {
                _context.JobPage.Remove(TableJobPage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
