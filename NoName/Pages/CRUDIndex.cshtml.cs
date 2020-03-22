using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages
{
    public class CRUDIndexModel : PageModel
    {
        private readonly DataContext _context;
        private readonly DataDbManager manager;
        [BindProperty]
        public TableDataJob TableDataJob { get; set; }
        public IList<TablePost> TablePost { get; set; }
        public CRUDIndexModel(DataContext context)
        {
            manager = DataDbManager.GetInstance();
            _context = context;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Create Job
            _context.Job.Add(TableDataJob);
            _context.SaveChanges();
            //Job생성시 기본 게시판 생성
            //핫게=1,실시간 인기글2, 주간 인기글 3, 자유게시판 4, 비밀게시판 5, 정보게시판 6 자유/홍보/정보/비밀
            var defaultBoard = new string[] { "HOT게시판", "실시간 인기글", "주간 인기글", "자유게시판", "비밀게시판", "정보게시판", "비밀게시판" };
            for (var i = 0; i < 6; i++)
            {
                //Create Default 6 Boards
                var board = new TableBoard
                {
                    //PK값임으로 자동입력 BoardId = i+1,
                    JobCode = TableDataJob.JobCode,
                    BoardName = defaultBoard[i]
                };
                _context.Board.Add(board);
                _context.SaveChanges();
                //Create 100 Posts
                for (int j = 1; j <= 10; j++)
                {
                    _context.Post.Add(new TablePost
                    {
                        //UserId 임시
                        UserId = "형수" + j.ToString(),
                        CategoryNumber = 1,
                        Title = j.ToString(),
                        Content = (j + j).ToString(),
                        ViewCount = 0,
                        LikeCount = 0,
                        DislikeCount = 0,
                        HasNewComment = false,
                        CreateTime = DateTime.Now,
                        InitialContent = "",
                        LastModifiedTime = DateTime.MinValue,
                        IsDeleted = false,
                        DeletedTime = DateTime.MinValue,
                        BoardId = board.BoardId
                    });
                }
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./CRUD/TablePostCRUD/Index");
        }
    }
}
