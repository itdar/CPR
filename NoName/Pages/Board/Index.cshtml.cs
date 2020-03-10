using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.Board
{
    public class IndexModel : PageModel
    {
        private readonly NoName.Data.DbData.DataContext _context;

        public  IndexModel(NoName.Data.DbData.DataContext context) {
            _context = context;
        }


        public IList<TablePost> Board { get; set; }
        public async Task OnGetAsync()
        {
                Board = await _context.Post.ToListAsync();
        }

        [BindProperty]
        public TablePost TablePost { get; set; }
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //DataDbManager.GetInstance().DataDB. 

            _context.Post.Add(TablePost);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Board/Index");
        }
        public void OnPost()
        {
            //정상 호출시 직업 게시판 타이틀 하단에 Message => Message + DateTime.Now 출력
            //Message += $" Server time is { DateTime.Now }";

        }
    }
}
