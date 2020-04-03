using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Pages.Shared;

namespace NoName.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataDbManager manager;
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(ILogger<IndexModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            manager = DataDbManager.GetInstance();
        }
        [BindProperty]
        public BoardPreview Preview { get; set; }
        public async void OnGetAsync()
        {
            //Preview할 게시물 개수
            int listNumber = 4;
            Preview = BoardPreview.CreatePreviewList(listNumber);

            // Main 이 되는 index page cs 에서,
            // 페이지 로딩될 때, 최초 로딩 또는 다른데에서 redirection 등
            // OnGet / OnPost 선택 호출하는 방법 확인해서 로그인 동작 시에만 호출 되는 것 만들어서 옮기던지 해야함
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                UserDbManager.GetInstance().SetLoggedInUserInfoUsingEmail(user.Email);
                UserDbManager.GetInstance().CheckLoggedInUserInformation();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("user is null (It means 로그인 안되어있음)");
            }
        }
        public void OnPost()
        {
            System.Diagnostics.Debug.WriteLine("Main index page OnPost() called");
        }
    }
}
