using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.ScaffoldingTest.TableJobPageCRUD
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;

        public IndexModel(DataContext context)
        {
            _context = context;
        }

        public IList<TableJobPage> TableJobPage { get; set; }

        public async Task OnGetAsync()
        {
            TableJobPage = await _context.JobPage.ToListAsync();
        }
    }
}
