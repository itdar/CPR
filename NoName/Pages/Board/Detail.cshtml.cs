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

        private readonly NoName.Data.DbData.DataContext _context;

        public DetailModel(NoName.Data.DbData.DataContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TablePost TablePost { get; set; }

        public List<TableComment> CommentList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? postNumber)
        {
            if (postNumber == null)
            {
                return NotFound();
            }

            TablePost = await _context.Post.FirstOrDefaultAsync(m => m.PostNumber == postNumber);

            CommentList =await _context.Comment.Where(i => i.PostNumber == TablePost.PostNumber).ToListAsync();
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
