using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.BackendClass.Login;
using NoName.BackendClass.Paging;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.Mypage
{
    public class MyPostsModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly DataDbManager manager;

        public MyPostsModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            manager = DataDbManager.GetInstance();

            // UserManager ���� context �Ȳ���� �����ϴ��� Ȯ���Ϸ��� ���� Ȯ�� �� ��������
        }
        [BindProperty]
        public Pagination<PostModel> Pagination { get; set; }

        public async Task OnGetAsync()
        {
                //���� id �� email �� �Ǿ� �־ email �޾Ƽ���
                //���̵�� �Ұ�� GetInstance�� .Email �� Id�� �ٲٸ� ��
                var userId = UserInformation.GetInstance().Email;
                var myPosts = manager.GetMyPosts(userId);
                Pagination = await Pagination<PostModel>.CreateAsync(myPosts, 1);
        }

        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
                var userId = UserInformation.GetInstance().Email;
                var myPosts = manager.GetMyPosts(userId);
                Pagination = await Pagination<PostModel>.CreateAsync(myPosts, pages);
            return Page();

        }
    }
}
