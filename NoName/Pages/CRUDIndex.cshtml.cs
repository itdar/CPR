using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Enumeration;


//참고 사이트 : https://docs.microsoft.com/ko-kr/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types


namespace NoName.Pages
{
    public class CRUDIndexModel : PageModel
    {
        private readonly DataContext _context;
        private readonly DataDbManager manager;
        [BindProperty]
        public TableDataJob TableDataJob { get; set; }
        public IList<TablePost> TablePost { get; set; }
        public IEnumerable<BoardType> TableBoard { get; set; }
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
            var  existedJob = _context.Job.Where(j => j.JobCode == TableDataJob.JobCode).FirstOrDefault();
            //Create a new TableJob
            if(existedJob == null)
            {
                _context.Job.Add(TableDataJob);
                _context.SaveChanges();
            }
            int postNumber;
            if (_context.Post.Find(1) == null)
                postNumber = 1;
            else
                postNumber = _context.Post.OrderByDescending(p => p.PostNumber).Last().PostNumber + 1;
            /***
             * 기본 게시판 => in BoardType Class
             ***/
            var TableBoard = Enumerator.GetAll<BoardType>();
            int length = TableBoard.Count();
            for (var i = 0; i < length; i++)
            {
                TableBoard board;
                if(existedJob == null)
                {
                    board = new TableBoard
                    {
                        BoardId = TableBoard.ElementAt(i).GetBoardId(TableDataJob.JobCode),
                        BoardName = TableBoard.ElementAt(i).Name,
                        JobCode = TableDataJob.JobCode
                    };
                    _context.Board.Add(board);
                    _context.SaveChanges();
                }
                else
                {
                    board = _context.Board.Where(b => b.JobCode == TableDataJob.JobCode).OrderBy(b => b.BoardSeq).ToList()[i];
                }
                //Create 100 Posts
                int ten = postNumber + 10;
                for (int j = postNumber; j < ten; j++)
                {
                    _context.Post.Add(new TablePost
                    {
                        //UserId 임시
                        Id = "형수" + j.ToString(),
                        CategoryNumber = 1,
                        Title = board.BoardName + postNumber.ToString(),
                        Content = board.BoardName + "의" + postNumber.ToString() + "째 게시물 입니다.",
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
                    postNumber++;
                    _context.SaveChanges();
                }
            }
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./CRUD/TablePostCRUD/Index");
        }
    }
}
