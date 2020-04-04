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
    public class IndexModel : PageModel
    {
        private readonly NoName.Data.DataContext _context;

        public IndexModel(NoName.Data.DataContext context)
        {
            _context = context;
        }

        public IList<TableBoard> TableBoard { get;set; }

        public async Task OnGetAsync()
        {
            TableBoard = await _context.Board
                .Include(t => t.Job).ToListAsync();
        }
    }
}
