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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataDbManager manager;
        //private int boardCode;
        public MyPostsModel(ILogger<IndexModel> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;

            manager = DataDbManager.GetInstance();

            // UserManager ���� context �Ȳ���� �����ϴ��� Ȯ���Ϸ��� ���� Ȯ�� �� ��������
        }
        [BindProperty]
        public Pagination<TablePost> Pagination { get; set; }

        public async Task OnGetAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                //���� id �� email �� �Ǿ� �־ email �޾Ƽ���
                //���̵�� �Ұ�� GetInstance�� .Email �� Id�� �ٲٸ� ��
                var userId = UserInformation.GetInstance().Email;
                var myPosts = manager.GetMyPosts(userId);
                Pagination = await Pagination<TablePost>.CreateAsync(myPosts, 1);
            }
        }

        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userId = UserInformation.GetInstance().Email;
                var myPosts = manager.GetMyPosts(userId);
                Pagination = await Pagination<TablePost>.CreateAsync(myPosts, pages);
            }
            return Page();

        }
    }
}
