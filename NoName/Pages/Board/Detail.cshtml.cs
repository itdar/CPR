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
using NoName.Pages.Shared;

namespace NoName.Pages.Board
{
    //public class DetailModel : PageModel
    //{

    //    private readonly DataContext context;

    //    public DetailModel()
    //    {
    //        // context �޾Ƽ� ���� �ڵ� �״�� ����Ϸ��� dataContext public ���� �ٲ����
    //        // manager �� �Լ� �ű�� private ���� �ٲ����
    //        context = DataDbManager.GetInstance().dataContext;
    //    }

    //    [BindProperty]
    //    public TablePost TablePost { get; set; }

    //    public List<TableComment> CommentList { get; set; }
    //    [BindProperty]
    //    public List<TableComment> ParentCommentList { get; set; }
    //    [BindProperty]
    //    public List<TableComment> ChildCommentList { get; set; }

    //    // public int ParentCommentCount { get; set; }
    //    public async Task<IActionResult> OnGetAsync(int? postNumber)
    //    {
    //        if (postNumber == null)
    //        {
    //            return NotFound();
    //        }

    //        TablePost = await context.Post.FirstOrDefaultAsync(m => m.PostNumber == postNumber);

    //        ParentCommentList = context.Comment.Where(comment => comment.PostNumber==postNumber &&comment.ParentCommentNumber==0).
    //            OrderBy(comment=>comment.CreatedTime).ToList();
    //        ChildCommentList = context.Comment.Where(comment => comment.PostNumber==postNumber &&comment.ParentCommentNumber!=0).
    //            OrderBy(comment=>comment.ParentCommentNumber).ThenBy(comment=>comment.CreatedTime).ToList();

    //        //CommentList = _context.Comment.FromSqlRaw("SELECT * FROM dbo.Comment WHERE PostNumber= :postNumber").ToList();

    //        //IF((ParentNumber = 0), CommentNumber, ParentCommentNumber),"ORDER BY CreatedTime")
    //        if (TablePost == null)
    //        {
    //            return NotFound();
    //        }
    //        return Page();
    //    }

    //    [BindProperty]
    //    public TableComment TableComment { get; set; }

    //    [BindProperty]
    //    public int ParentCommentNumber { get; set; }
    //    public async Task<IActionResult> OnPostAsync()
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return Page();
    //        }

    //        TableComment.CreatedTime = DateTime.Now;
    //        TableComment.PostNumber = TablePost.PostNumber;
    //        // TableComment.ParentCommentNumber = ParentCommentNumber;

    //        context.Comment.Add(TableComment);
    //        await context.SaveChangesAsync();
    //        return RedirectToPage(TableComment.PostNumber);
    //    }


    //}

    public class DetailModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataDbManager manager;
        //private int boardCode;


        [BindProperty]
        public TablePost CurrentPost { get; set; }
        [BindProperty]
        public List<BoardPreview> SidebardLists { get; set; }
        public DetailModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            manager = DataDbManager.GetInstance();

            // UserManager ���� context �Ȳ���� �����ϴ��� Ȯ���Ϸ��� ���� Ȯ�� �� ��������
            UserDbManager.GetInstance();
        }
        [BindProperty]
        public List<TableComment> ParentCommentList { get; set; }
        [BindProperty]
        public List<TableComment> ChildCommentList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? postNumber)
        {
            if (postNumber == null)
            {
                return NotFound();
            }

            SidebardLists = new List<BoardPreview>();
            for (int i = 1; i < 4; i++)
            {
                var postlist = manager.GetPosts(i, 2);
                var Preview = BoardPreview.CreatePreviewList(postlist, i, manager.GetBoardName(i));
                SidebardLists.Add(Preview);
            }
            CurrentPost = manager.GetPostDetail((int)postNumber);
            //���&���� �ѹ��� �Լ� ����°� ���� ���ϰھ ���� ���� �߾��
            ParentCommentList = manager.GetParentComments((int)postNumber);
            ChildCommentList = manager.GetChildComments((int)postNumber);
            //CommentList = _context.Comment.FromSqlRaw("SELECT * FROM dbo.Comment WHERE PostNumber= :postNumber").ToList();

            //IF((ParentNumber = 0), CommentNumber, ParentCommentNumber),"ORDER BY CreatedTime")
            if (CurrentPost == null)
            {
                return NotFound();
            }
            return Page();
        }
        [BindProperty]
        public TableComment TableComment { get; set; }

        [BindProperty]
        public int ParentCommentNumber { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TableComment.CreatedTime = DateTime.Now;
            TableComment.PostNumber = CurrentPost.PostNumber;
            // TableComment.ParentCommentNumber = ParentCommentNumber;

            await manager.AddCommnetAsync(TableComment);
            return RedirectToPage(TableComment.PostNumber);
        }

        //public async Task<IActionResult> OnPostModifyPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    manager.EditPost(CurrentPost);

        //    try
        //    {
        //        await manager.dataContext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TableBoardExists(CurrentPost.PostNumber))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("./Detail");
        //}

        //private bool TableBoardExists(int id)
        //{
        //    return manager.dataContext.Board.Any(e => e.BoardNumber == id);
        //}
    }
}
