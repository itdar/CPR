using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.ScaffoldingTest.TableDataJobCRUD
{
    public class DetailsModel : PageModel
    {
        private readonly DataContext _context;

        public DetailsModel()
        {
            _context = DataDbManager.GetInstance().dataContext;
        }

        public TableDataJob TableDataJob { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TableDataJob = await _context.Job.FirstOrDefaultAsync(m => m.JobCode == id);

            if (TableDataJob == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
