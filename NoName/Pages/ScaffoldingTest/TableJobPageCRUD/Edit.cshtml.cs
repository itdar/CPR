using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NoName.Data.DbData;

namespace NoName.Pages.ScaffoldingTest.TableJobPageCRUD
{
    public class EditModel : PageModel
    {
        private readonly NoName.Data.DbData.DataContext _context;

        public EditModel(NoName.Data.DbData.DataContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TableJobPage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableJobPageExists(TableJobPage.JobPageId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TableJobPageExists(int id)
        {
            return _context.JobPage.Any(e => e.JobPageId == id);
        }
    }
}
