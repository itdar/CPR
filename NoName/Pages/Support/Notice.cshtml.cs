using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.Support
{
    public class NoticeModel : PageModel
    {
        
        private readonly DataContext _context;
        //public NoticeModel(DataContext context, ILogger<NoticeModel> logger)
        //{
        //    _context = context;
        //    //_logger = logger;
        //}
        
        //private readonly ILogger<NoticeModel> _logger;

        private readonly DataDbManager _manager = DataDbManager.GetInstance();
        public Pagination<TablePost> Pagination { get; set; }

        //Constructor
        //public NoticeModel(ILogger<NoticeModel> logger)
        //{
        //    _logger = logger;
        //}
        public async Task<IActionResult> OnGetAsync()
        {
            //외래키 참조 불가 -> 따로 설정해줘야 함 *20.03.11 CreateAsync(source, currentPage=1, pageSize =10)
            Pagination = await Pagination<TablePost>.CreateAsync(_manager.GetPosts(1));
            return Page();
        }
        //Pagination으로 생성된 페이지들 선택시 호출되는 함수
        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            //var manager = DataDbManager.GetInstance();
            Pagination = await Pagination<TablePost>.CreateAsync(_manager.GetPosts(1), pages);
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