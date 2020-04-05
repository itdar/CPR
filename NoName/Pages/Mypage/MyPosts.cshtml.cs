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

            // UserManager 에서 context 안끊기고 동작하는지 확인하려고 만들어봄 확인 후 지워야함
        }
        [BindProperty]
        public Pagination<PostModel> Pagination { get; set; }

        public async Task OnGetAsync()
        {
                //현재 id 가 email 로 되어 있어서 email 받아서함
                //아이디로 할경우 GetInstance의 .Email 만 Id로 바꾸면 댐
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
