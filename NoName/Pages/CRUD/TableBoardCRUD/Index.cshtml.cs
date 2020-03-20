using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.CRUD.TableBoardCRUD
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;

        public IndexModel(DataContext context)
        {
            _context = context;
        }
        public IList<TableBoard> TableBoard { get; set; }

        public async Task OnGetAsync()
        {

            TableBoard = await _context.Board.ToListAsync();
        }

        public async Task<IActionResult> OnGetMakeBoardAsync()
        {
            //TableBoardCode boardCode = new TableBoardCode
            //{
            //    BoardKindName = "자유게시판"
            //};
            //_context.BoardCode.Add(boardCode);

            for (var j = 0; j < 4; j++)
            {
                var moc = new TableBoard
                {
                    BoardCode = j+1,
                    JobCode = 1
                };
                await _context.Board.AddAsync(moc);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetJobIdAsync()
        {
            TableDataJob job = new TableDataJob
            {
                JobName = "프로그래머"
            };
            _context.Job.Add(job);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

        }
    }
}
