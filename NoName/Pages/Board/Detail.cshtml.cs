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
using NoName.Data;
using NoName.Data.DbData;
using NoName.Pages.Shared;

namespace NoName.Pages.Board
{
    public class DetailModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataDbManager manager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        [BindProperty]
        public TablePost CurrentPost { get; set; }
        [BindProperty]
        public List<List<TableComment>> CommentList { get; set; }
        [BindProperty]
        public int CommentCount { get; set; }
        //public List<TableComment> ParentCommentList { get; set; }
        //[BindProperty]
        //public List<TableComment> ChildCommentList { get; set; }
        [BindProperty]
        public TableComment TableComment { get; set; }
        [BindProperty]
        public int ParentCommentNumber { get; set; }

        public DetailModel(ILogger<IndexModel> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            manager = DataDbManager.GetInstance();

            // UserManager ���� context �Ȳ���� �����ϴ��� Ȯ���Ϸ��� ���� Ȯ�� �� ��������
            UserDbManager.GetInstance();
        }


        

        public IActionResult OnGet(int postNumber)
        {
            CurrentPost = manager.GetPostDetail(postNumber);

            //���
            var tempList = manager.GetCommentList(postNumber);
            CommentCount = tempList.Count();

            CommentList = new List<List<TableComment>>();
            foreach(var comment in tempList)
            {
                if (comment.ParentCommentNumber == 0)
                {
                    var parentComment = new List<TableComment>();
                    parentComment.Add(comment);
                    CommentList.Add(parentComment);
                }
                else
                {
                    CommentList[comment.ParentCommentNumber - 1].Add(comment);
                }
            }
            //CommentList = _context.Comment.FromSqlRaw("SELECT * FROM dbo.Comment WHERE PostNumber= :postNumber").ToList();

            //IF((ParentNumber = 0), CommentNumber, ParentCommentNumber),"ORDER BY CreatedTime")
            if (CurrentPost == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_signInManager.IsSignedIn(User))
            {
                TableComment.userId = UserInformation.GetInstance().Email;
            }
            TableComment.CreatedTime = DateTime.Now;
            TableComment.PostNumber = CurrentPost.PostNumber;
            // TableComment.ParentCommentNumber = ParentCommentNumber;

            await manager.AddCommnetAsync(TableComment);
            return RedirectToPage(TableComment.PostNumber);
        }
    }
}
