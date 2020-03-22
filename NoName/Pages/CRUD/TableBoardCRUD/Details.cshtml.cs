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
        private readonly DataContext _context;

        public DetailsModel()
        {
            _context = DataDbManager.GetInstance().dataContext;
        }

        public TableBoard TableBoard { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TableBoard = await _context.Board.FirstOrDefaultAsync(m => m.BoardNumber == id);

            if (TableBoard == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
