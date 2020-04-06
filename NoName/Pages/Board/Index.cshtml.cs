using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoName.BackendClass.Login;
using NoName.BackendClass.Paging;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Pages.Shared;

namespace NoName.Pages.Board
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataDbManager manager;

        [BindProperty]
        public TablePost TablePost { get; set; }
        [BindProperty]
        public TableBoard CurrentBoard { get; set; }

        public Pagination<PostModel> Pagination { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            manager = DataDbManager.GetInstance();

            // UserManager 에서 context 안끊기고 동작하는지 확인하려고 만들어봄 확인 후 지워야함
            UserDbManager.GetInstance();
        }


        public void OnGet() { }

        //최초 페이지 선택시 호출
        public async Task<IActionResult> OnGetBoardAsync(int boardId)
        {
            //GetPost시 UserDb에서 Jobname에 맞는 Board에서 BoardNumber 가져와야함
            CurrentBoard = manager.GetBoard(boardId);
            Pagination = await Pagination<PostModel>.CreateAsync(manager.GetPosts(boardId));
            return Page();
        }

        //페이지 변경시 호출
        public async Task<IActionResult> OnGetPageAsync(int pages, int boardId)
        {
            CurrentBoard = manager.GetBoard(boardId);
            Pagination = await Pagination<PostModel>.CreateAsync(manager.GetPosts(boardId), pages);
            return Page();
        }

        //글작성시 호출
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TablePost.Id = UserInformation.GetInstance().Email;
            TablePost.BoardId = CurrentBoard.BoardId;
            TablePost.CreateTime = DateTime.Now;
            await manager.AddPostAsync(TablePost);
            return RedirectToPage(TablePost.BoardId);
        }
    }
}
