using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoName.Data.DbData;

namespace NoName.Pages.Support
{
    public class NoticeModel : PageModel
    {
        private readonly DataContext _context;
        private readonly ILogger<NoticeModel> _logger;
        //public IList<TablePost> TablePost { get; set; }
        //Test용 Message
        public string Message { get; set; }
        public Pagination<TablePost> Pagination { get; set; }
        //Constructor
        public NoticeModel(DataContext context, ILogger<NoticeModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            //외래키 참조 불가 -> 따로 설정해줘야 함 *20.03.11
            //TablePost = await _context.Post.ToListAsync();
            if (Pagination == null)
            {
                Pagination = await Pagination<TablePost>.CreateAsync(_context.Post, 1);
            }
            return Page();
        }
        public async Task<IActionResult> OnGetPage(int currentPage)
        {
            if (Pagination == null)
            {
                Pagination = await Pagination<TablePost>.CreateAsync(_context.Post, 1);
            }
            return Page();
        }
        //Get이나 Post method 호출시 OnGet[Value]()와 OnPost[Value]() 형식으로 호출 됨. Value = Handler Name
        //함수 형태를 Async(비동기)로 변경가능.(await 사용)
        //동시성 예외 처리 검토
        public IActionResult OnGetPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}