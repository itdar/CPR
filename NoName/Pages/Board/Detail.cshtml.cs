using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data.DbData;

namespace NoName.Pages.Board
{
    public class DetailModel : PageModel
    {

        private readonly NoName.Data.DataContext _context;

        public DetailModel(NoName.Data.DataContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TablePost TablePost { get; set; }

        public List<TableComment> CommentList { get; set; }
        [BindProperty]
        public List<TableComment> ParentCommentList { get; set; }
        [BindProperty]
        public List<TableComment> ChildCommentList { get; set; }

        // public int ParentCommentCount { get; set; }
        public async Task<IActionResult> OnGetAsync(int? postNumber)
        {
            if (postNumber == null)
            {
                return NotFound();
            }

            TablePost = await _context.Post.FirstOrDefaultAsync(m => m.PostNumber == postNumber);

            ParentCommentList = _context.Comment.Where(comment => comment.PostNumber==postNumber &&comment.ParentCommentNumber==0).
                OrderBy(comment=>comment.CreatedTime).ToList();
            ChildCommentList = _context.Comment.Where(comment => comment.PostNumber==postNumber &&comment.ParentCommentNumber!=0).
                OrderBy(comment=>comment.ParentCommentNumber).ThenBy(comment=>comment.CreatedTime).ToList();

            //CommentList = _context.Comment.FromSqlRaw("SELECT * FROM dbo.Comment WHERE PostNumber= :postNumber").ToList();

            //IF((ParentNumber = 0), CommentNumber, ParentCommentNumber),"ORDER BY CreatedTime")
            if (TablePost == null)
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
            TableComment.PostNumber = TablePost.PostNumber;
           // TableComment.ParentCommentNumber = ParentCommentNumber;

            _context.Comment.Add(TableComment);
            await _context.SaveChangesAsync();
            return RedirectToPage(TableComment.PostNumber);
        }

       
    }
}
