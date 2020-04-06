using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.BackendClass.Paging;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Enumeration;

namespace NoName.Pages.BoardPopular
{
    /// <summary>
    /// TableBoard/TablePost과 다른 테이블에서 데이터를 가져와야 함으로 Index page를 따로 놓음.
    /// Detail page은 공유함.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly DataDbManager manager;
        //private readonly ILogger<IndexModel> _logger;

        public Pagination<PostModel> Pagination { get; set; }

        public IndexModel()
        {
            manager = DataDbManager.GetInstance();
        }

        //최초 페이지 선택시 호출
        public async Task<IActionResult> OnGetBoardAsync(int boardId)
        {
            Pagination = await Pagination<PostModel>.CreateAsync(manager.GetPopularPosts(boardId));
            return Page();
        }
        //Pagination으로 생성된 페이지들 선택시 호출되는 함수
        public async Task<IActionResult> OnGetPageAsync(int pages, int boardId)
        {
            Pagination = await Pagination<PostModel>.CreateAsync(manager.GetPopularPosts(boardId), pages);
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