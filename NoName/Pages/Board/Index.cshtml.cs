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
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataDbManager _manager;
        public Pagination<TablePost> Pagination { get; set; }
        [BindProperty]
        public TablePost TablePost { get; set; }
        public IndexModel(IDataDbManager manager, ILogger<IndexModel> logger)
        {
            _manager = manager;
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync(int? boardCode)
        {
            if (boardCode == null)
            {
                return NotFound();

            }
            //GetPost시 UserDb에서 Jobname에 맞는 Board에서 BoardNumber 가져와야함
            Pagination = await Pagination<TablePost>.CreateAsync(_manager.GetPosts(1), 1);
            return Page();
        }
        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            Pagination = await Pagination<TablePost>.CreateAsync(_manager.GetPosts(1), pages);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            TablePost.CreateTime = DateTime.Now;
            TablePost.BoardCode = 1;
            await _manager.AddPostAsync(TablePost);
            return RedirectToPage("/Board/Index");
        }
       
    }
}
