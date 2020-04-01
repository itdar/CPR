using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Enumeration;

namespace NoName.Pages.Support
{
    public class NoticeModel : PageModel
    {
        private readonly DataDbManager manager;
        //private readonly ILogger<IndexModel> _logger;

        public NoticeModel()
        {
            manager = DataDbManager.GetInstance();
        }

        public Pagination<TablePost> Pagination { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int JobCode = 100;
            Pagination = await Pagination<TablePost>.CreateAsync(manager.GetPosts(BoardType.Notice.GetBoardId(JobCode)));
            return Page();
        }
        //Pagination으로 생성된 페이지들 선택시 호출되는 함수
        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            int JobCode = 100;
            Pagination = await Pagination<TablePost>.CreateAsync(manager.GetPosts(BoardType.Notice.GetBoardId(JobCode)), pages);
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