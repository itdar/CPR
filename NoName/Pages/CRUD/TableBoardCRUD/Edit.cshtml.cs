using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.CRUD.TableBoardCRUD
{
    public class EditModel : PageModel
    {
        private readonly NoName.Data.DataContext _context;

        public EditModel(NoName.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TableBoard TableBoard { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TableBoard = await _context.Board
                .Include(t => t.Job).FirstOrDefaultAsync(m => m.BoardId == id);

            if (TableBoard == null)
            {
                return NotFound();
            }
           ViewData["JobCode"] = new SelectList(_context.Job, "JobCode", "JobCode");
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

            _context.Attach(TableBoard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableBoardExists(TableBoard.BoardId))
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

        private bool TableBoardExists(int id)
        {
            return _context.Board.Any(e => e.BoardId == id);
        }
    }
}
