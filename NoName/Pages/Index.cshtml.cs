using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.BackendClass.Account;
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
        public  List<BoardPreview> PreviewBoards { get; set; }
        public async void OnGet(int numberOfBoard=7)
        {
            System.Diagnostics.Debug.WriteLine("Main index page OnGet(int numberOfBoard) called");

            PreviewBoards = new List<BoardPreview>();
            int boardId;
            for (boardId = 1; boardId < 4; boardId++)
            {
                var postlist = manager.GetPosts(boardId, 2);
                var Preview = BoardPreview.CreatePreviewList(postlist, boardId, manager.GetBoardName(boardId));
                PreviewBoards.Add(Preview);
            }
            for (int boardid = 4; boardid <= numberOfBoard; boardid++)
            {
                var postlist = manager.GetPosts(boardid, 4);
                var Preview = BoardPreview.CreatePreviewList(postlist, boardid, manager.GetBoardName(boardid));
                PreviewBoards.Add(Preview);
            }

            // Main 이 되는 index page cs 에서,
            // 페이지 로딩될 때, 최초 로딩 또는 다른데에서 redirection 등
            // OnGet / OnPost 선택 호출하는 방법 확인해서 로그인 동작 시에만 호출 되는 것 만들어서 옮겨야함
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var userName = await _userManager.GetUserNameAsync(user);
                var email = await _userManager.GetEmailAsync(user);
                var jobCodesQuery = UserDbManager.GetInstance().GetUserJobCodes(userId);
                var jobCodes = new List<int>();

                System.Diagnostics.Debug.WriteLine(jobCodesQuery.Count());

                foreach (var jobCode in jobCodesQuery)
                {
                    System.Console.WriteLine("JobCode : " + jobCode.JobCode);
                }

                var userInfo = UserInformation.GetInstance();
                userInfo.SetInformation(userId, userName, email, jobCodes);

                System.Diagnostics.Debug.WriteLine("UserId : " + userInfo.UserId);
                System.Diagnostics.Debug.WriteLine("UserName : " + userInfo.UserName);
                System.Diagnostics.Debug.WriteLine("Email : " + userInfo.Email);
                for (int i = 0; i < userInfo.JobCodes.Count(); ++i)
                {
                    System.Diagnostics.Debug.WriteLine("JobCode : {i}", userInfo.JobCodes.ElementAt(i));
                }
                System.Diagnostics.Debug.WriteLine(userInfo.JobCodes.ToString());
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
