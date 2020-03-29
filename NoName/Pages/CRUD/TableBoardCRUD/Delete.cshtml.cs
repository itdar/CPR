﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly NoName.Data.DataContext _context;

        public DeleteModel(NoName.Data.DataContext context)
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
                .Include(t => t.Job).FirstOrDefaultAsync(m => m.BoardNumber == id);

            if (TableBoard == null)
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

            TableBoard = await _context.Board.FindAsync(id);

            if (TableBoard != null)
            {
                _context.Board.Remove(TableBoard);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
