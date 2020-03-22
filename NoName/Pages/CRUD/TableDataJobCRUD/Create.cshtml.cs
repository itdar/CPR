using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.CRUD.TableDataJobCRUD
{
    public class CreateModel : PageModel
    {
        private readonly NoName.Data.DataContext _context;

        public CreateModel(NoName.Data.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TableDataJob TableDataJob { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Job.Add(TableDataJob);
            //Job생성시 기본 게시판 생성
            //핫게=1,실시간 인기글2, 주간 인기글 3, 자유게시판 4, 비밀게시판 5, 정보게시판 6 자유/홍보/정보/비밀
            var defaultBoard = new string[] { "HOT게시판", "실시간 인기글", "주간 인기글", "자유게시판", "비밀게시판", "정보게시판", "비밀게시판" };
            for (var i = 0; i < 6; i++)
            {
                await _context.Board.AddAsync(new TableBoard
                {
                    //PK값임으로 자동입력 BoardId = i+1,
                    JobCode = TableDataJob.JobCode,
                    BoardName = defaultBoard[i]
                });
            }



            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
