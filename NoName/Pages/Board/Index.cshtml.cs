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
        private readonly DataDbManager manager;
        //private int boardCode;

        public Pagination<TablePost> Pagination { get; set; }

        [BindProperty]
        public TablePost TablePost { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            manager = DataDbManager.GetInstance();

            // UserManager 에서 context 안끊기고 동작하는지 확인하려고 만들어봄 확인 후 지워야함
            UserDbManager.GetInstance();
        }

        public async Task<IActionResult> OnGetAsync(int? boardCode)
        {
            if (boardCode == null)
            {
                return NotFound();

            }
            //GetPost시 UserDb에서 Jobname에 맞는 Board에서 BoardNumber 가져와야함
            //System.Diagnostics.Debug.WriteLine(manager.GetPosts(1));
            Pagination = await Pagination<TablePost>.CreateAsync(manager.GetPosts(1), 1);
            return Page();
        }
        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            Pagination = await Pagination<TablePost>.CreateAsync(manager.GetPosts(1), pages);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            TablePost.CreateTime = DateTime.Now;
            TablePost.BoardCode = 1;
            await manager.AddPostAsync(TablePost);
            return RedirectToPage("/Board/Index");
        }
       
    }
}
