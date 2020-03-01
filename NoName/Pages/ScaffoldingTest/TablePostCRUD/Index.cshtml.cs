using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data.DbData;

namespace NoName
{
    public class IndexModel : PageModel
    {
        private readonly NoName.Data.DbData.DataContext _context;

        public IndexModel(NoName.Data.DbData.DataContext context)
        {
            _context = context;
        }

        public IList<TablePost> TablePost { get;set; }

        public async Task OnGetAsync()
        {
            TablePost = await _context.Post.ToListAsync();
        }
    }
}
