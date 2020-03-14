using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.Board
{
    public class FindResultModel : PageModel
    {
        public Pagination<TablePost> Pagination { get; set; }
        private readonly DataContext _context;
        private readonly ILogger<FindResultModel> _logger;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public FindResultModel(DataContext context, ILogger<FindResultModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        /*public async Task<IActionResult> OnGetAsync()
        {
            SearchString = Request.Form["searchString"];
            //외래키 참조 불가 -> 따로 설정해줘야 함 *20.03.11
            Pagination = await Pagination<TablePost>.CreateAsync(_context.Post, 1);
            return Page();
        }*/
        //Pagination으로 생성된 페이지들 선택시 호출되는 함수
        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            SearchString = Request.Form["searchString"];
            Pagination = await Pagination<TablePost>.CreateAsync(_context.Post, pages);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            SearchString = Request.Form["searchString"];
            Pagination = await Pagination<TablePost>.CreateAsync(_context.Post, 1);
            return Page();
        }
    }
}
