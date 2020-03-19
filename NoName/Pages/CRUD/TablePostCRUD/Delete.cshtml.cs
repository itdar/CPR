﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data.DbData;

namespace NoName.Pages.CRUD.TablePostCRUD
{
    public class DeleteModel : PageModel
    {
        private readonly NoName.Data.DataContext _context;

        public DeleteModel(NoName.Data.DataContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TablePost = await _context.Post.FindAsync(id);

            if (TablePost != null)
            {
                _context.Post.Remove(TablePost);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
