using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NoName.Data.DbData;

namespace NoName.Pages.CRUD.TablePostCRUD
{
    public class EditModel : PageModel
    {
        private readonly NoName.Data.DataContext _context;

        public EditModel(NoName.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TablePost TablePost { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TablePost = await _context.Post.FirstOrDefaultAsync(m => m.PostNumber == id);

            if (TablePost == null)
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

            _context.Attach(TablePost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TablePostExists(TablePost.PostNumber))
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

        private bool TablePostExists(int id)
        {
            return _context.Post.Any(e => e.PostNumber == id);
        }
    }
}
