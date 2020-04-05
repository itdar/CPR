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
        public TableDataJob DataJob { get; set; }
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
            var  existedJob = _context.Job.Where(j => j.JobCode == DataJob.JobCode).FirstOrDefault();
            //Create a new TableJob
            if(existedJob == null)
            {
                //JobNumber의 Code화 1000단위
                DataJob.JobCode *= 1000;
                _context.Job.Add(DataJob);
                _context.SaveChanges();
            }

            // 기존에 있는 Job에 Post를 이어 입력하기 위해 Last postNumber 구하기
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
            for (int i = 0; i < length; i++)
            {
                TableBoard board;
                if(existedJob == null)
                {
                    board = new TableBoard
                    {
                        // BoardId 코드화
                        BoardId = TableBoard.ElementAt(i).GetBoardId(DataJob.JobCode),
                        BoardName = TableBoard.ElementAt(i).Name,
                        JobCode = DataJob.JobCode
                    };
                    _context.Board.Add(board);
                    _context.SaveChanges();
                }
                else
                {
                    board = _context.Board.Where(b => b.JobCode == DataJob.JobCode).OrderBy(b => b.BoardSeq).ToList()[i];
                }
                //Create 100 Posts
                int ten = postNumber + 10;
                for (int j = postNumber; j < ten; j++)
                {
                    _context.Post.Add(new TablePost
                    {
                        //UserId 임시
                        Id = "noname0@noname.com",
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
                    _context.SaveChanges();
                    for (int k=1;k<3;k++)
                    {
                        _context.Comment.Add(new TableComment
                        {
                            userId = "noname0@noname.com",
                            Content = board.BoardName+" " + j + "번째 글" + k+" 댓글",
                            CreatedTime = DateTime.Now,
                            PostNumber=postNumber,
                            ParentCommentNumber=0
                        });
                        _context.SaveChanges();

                    }
                    postNumber++;
                    _context.SaveChanges();
                }
            }
            /***
             * 인기 게시판 => in PopularBoardType Class
             ***/
            var popularTableBoard = Enumerator.GetAll<PopularBoardType>();
            int pLength = popularTableBoard.Count();
            for (int i = 0; i < pLength; i++)
            {
                TablePopularBoard board;
                if (existedJob == null)
                {
                    board = new TablePopularBoard
                    {
                        BoardId = popularTableBoard.ElementAt(i).GetBoardId(DataJob.JobCode),
                        BoardName = popularTableBoard.ElementAt(i).Name,
                        JobCode = DataJob.JobCode
                    };
                    _context.PopularBoard.Add(board);
                    _context.SaveChanges();

                    await _context.SaveChangesAsync();
                }
                //Add Post to PopularBoard
                var post = _context.Post.ToList();
                foreach(var item in post)
                {
                    if(item.PostNumber %10 == 1)
                    {
                        _context.PopularPost.Add(new TablePopularPost
                        {
                            //UserId 임시
                            Id = item.Id,
                            CategoryNumber = item.CategoryNumber,
                            Title = item.Title,
                            Content = item.Content,
                            ViewCount = item.ViewCount,
                            LikeCount = item.LikeCount,
                            DislikeCount = item.DislikeCount,
                            HasNewComment = item.HasNewComment,
                            CreateTime = item.CreateTime,
                            InitialContent = item.InitialContent,
                            LastModifiedTime = item.LastModifiedTime,
                            IsDeleted = item.IsDeleted,
                            DeletedTime = item.DeletedTime,
                            BoardId = popularTableBoard.ElementAt(i).GetBoardId(DataJob.JobCode),
                            SelectionTime = DateTime.Now
                        });
                        _context.SaveChanges();
                    }
                }
            }
            /***
             * 스크랩 게시판 => in MyBoardType Class
             ***/
            var myTableBoard = Enumerator.GetAll<MyBoardType>();
            int mLength = myTableBoard.Count();
            for (int i = 0; i < mLength; i++)
            {
                TableMyBoard board;
                if (existedJob == null)
                {
                    board = new TableMyBoard
                    {
                        BoardId = TableBoard.ElementAt(i).GetBoardId(DataJob.JobCode),
                        BoardName = myTableBoard.ElementAt(i).Name,
                        JobCode = DataJob.JobCode
                    };
                    _context.MyBoard.Add(board);
                    _context.SaveChanges();

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./CRUD/TablePostCRUD/Index");
        }
    }
}
