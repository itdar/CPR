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
    public class DetailsModel : PageModel
    {
        private readonly NoName.Data.DataContext _context;

        public DetailsModel(NoName.Data.DataContext context)
        {
            _context = context;
        }

        public TableDataJob TableDataJob { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TableDataJob = await _context.Job.FirstOrDefaultAsync(m => m.DataJobSeq == id);

            if (TableDataJob == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
