using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.BackendClass.Paging;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Enumeration;

namespace NoName.Pages.Board
{
    public class PopularBoardModel : PageModel
    {
        private readonly DataDbManager manager;
        //private readonly ILogger<IndexModel> _logger;

        public PopularBoardModel()
        {
            manager = DataDbManager.GetInstance();
        }

        public Pagination<PostModel> Pagination { get; set; }

        public async Task<IActionResult> OnGetAsync(int? boardId)
        {
            Pagination = await Pagination<PostModel>.CreateAsync(manager.GetPopularPosts((int)boardId));
            return Page();
        }
        //Pagination���� ������ �������� ���ý� ȣ��Ǵ� �Լ�
        public async Task<IActionResult> OnGetPageAsync(int pages, int? boardId)
        {
            Pagination = await Pagination<PostModel>.CreateAsync(manager.GetPopularPosts((int)boardId), pages);
            return Page();
        }
        //Get�̳� Post method ȣ��� OnGet[Value]()�� OnPost[Value]() �������� ȣ�� ��. Value = Handler Name
        //�Լ� ���¸� Async(�񵿱�)�� ���氡��.(await ���)
        //���ü� ���� ó�� ����
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