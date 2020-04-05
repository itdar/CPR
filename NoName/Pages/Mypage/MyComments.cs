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
    public class CommentPostModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataDbManager manager;
        public CommentPostModel(ILogger<IndexModel> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;

            manager = DataDbManager.GetInstance();

            // UserManager 에서 context 안끊기고 동작하는지 확인하려고 만들어봄 확인 후 지워야함
        }
        [BindProperty]
        public Pagination<TableComment> Pagination { get; set; }

        public async Task OnGetAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var comments = manager.GetMyComments(UserInformation.GetInstance().Email);
                Pagination =await Pagination<TableComment>.CreateAsync(comments, 1);
            }
        }

        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var comments = manager.GetMyComments(UserInformation.GetInstance().Email);
                Pagination = await Pagination<TableComment>.CreateAsync(comments, pages);
            }
            return Page();

        }
    }
}
