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

            // UserManager ���� context �Ȳ���� �����ϴ��� Ȯ���Ϸ��� ���� Ȯ�� �� ��������
            UserDbManager.GetInstance();
        }


        public void OnGet() { }

        //���� ������ ���ý� ȣ��
        public async Task<IActionResult> OnGetBoardAsync(int boardId)
        {
            //GetPost�� UserDb���� Jobname�� �´� Board���� BoardNumber �����;���
            CurrentBoard = manager.GetBoard(boardId);
            Pagination = await Pagination<PostModel>.CreateAsync(manager.GetPosts(boardId));
            return Page();
        }

        //������ ����� ȣ��
        public async Task<IActionResult> OnGetPageAsync(int pages, int boardId)
        {
            CurrentBoard = manager.GetBoard(boardId);
            Pagination = await Pagination<PostModel>.CreateAsync(manager.GetPosts(boardId), pages);
            return Page();
        }

        //���ۼ��� ȣ��
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
