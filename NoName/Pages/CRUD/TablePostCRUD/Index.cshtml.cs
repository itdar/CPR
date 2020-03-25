using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data.DbData;

namespace NoName.Pages.CRUD.TablePostCRUD
{
    public class IndexModel : PageModel
    {
        private readonly NoName.Data.DataContext _context;

        public IndexModel(NoName.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int IdOfBoard { get; set; }
        public IList<TablePost> TablePost { get; set; }

        public async Task OnGetAsync()
        {
            TablePost = await _context.Post.ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //var board = from b in _context.Board
            //            where b.BoardCode.CompareTo(CodeOfBoard) == 0
            //            select b;
            //if (board.Count() == 0)
            //{
            //    var newBoard = new TableBoard
            //    {
            //        BoardName = "프로그래머" + CodeOfBoard.ToString(),
            //        BoardCode = CodeOfBoard
            //    };
            //    _context.Board.Add(newBoard);
            //    _context.SaveChanges();
            //}
            for (int j = 1; j < 8; j++)
            {
                for (int i = 1; i <=120; i++)
                {
                    var mock = new TablePost
                    {
                        UserId = "형수" + i.ToString(),
                        CategoryNumber = 1,
                        Title =j+"번째 게시판" +i + "번째 글",
                        Content = j + "번째 게시판" + i + "번째 글",
                        ViewCount = 0,
                        LikeCount = 0,
                        DislikeCount = 0,
                        HasNewComment = false,
                        CreateTime = DateTime.Now,
                        InitialContent = "",
                        LastModifiedTime = DateTime.MinValue,
                        IsDeleted = false,
                        DeletedTime = DateTime.MinValue,
                        BoardId = j
                    };
                    _context.Post.Add(mock);
                    await _context.SaveChangesAsync();

                }
            }

            return RedirectToPage("./Index");
        }
    }
}
