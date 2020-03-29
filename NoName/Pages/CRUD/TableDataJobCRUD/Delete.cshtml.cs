using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.CRUD.TableDataJobCRUD
{
    public class DeleteModel : PageModel
    {
        private readonly NoName.Data.DataContext _context;

        public DeleteModel(NoName.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TableDataJob TableDataJob { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TableDataJob = await _context.Job.FirstOrDefaultAsync(m => m.Number == id);

            if (TableDataJob == null)
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

            TableDataJob = await _context.Job.FindAsync(id);

            if (TableDataJob != null)
            {
                _context.Job.Remove(TableDataJob);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
