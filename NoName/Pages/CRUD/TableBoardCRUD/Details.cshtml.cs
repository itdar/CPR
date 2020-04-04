using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.CRUD.TableBoardCRUD
{
    public class DetailsModel : PageModel
    {
        private readonly NoName.Data.DataContext _context;

        public DetailsModel(NoName.Data.DataContext context)
        {
            _context = context;
        }

        public TableBoard TableBoard { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TableBoard = await _context.Board
                .Include(t => t.Job).FirstOrDefaultAsync(m => m.BoardSeq == id);

            if (TableBoard == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
