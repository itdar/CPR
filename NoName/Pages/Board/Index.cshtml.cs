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
        private readonly DataContext _context;
        private readonly ILogger<IndexModel> _logger;
        public Pagination<TablePost> Pagination { get; set; }
        [BindProperty]
        public TablePost TablePost { get; set; }
        public IndexModel(DataContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            Pagination = await Pagination<TablePost>.CreateAsync(_context.Post, 1);
            return Page();
        }
        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            Pagination = await Pagination<TablePost>.CreateAsync(_context.Post, pages);
            return Page();
        }

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
