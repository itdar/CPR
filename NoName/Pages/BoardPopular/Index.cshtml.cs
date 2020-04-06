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
    /// TableBoard/TablePost�� �ٸ� ���̺��� �����͸� �����;� ������ Index page�� ���� ����.
    /// Detail page�� ������.
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

        //���� ������ ���ý� ȣ��
        public async Task<IActionResult> OnGetBoardAsync(int boardId)
        {
            Pagination = await Pagination<PostModel>.CreateAsync(manager.GetPopularPosts(boardId));
            return Page();
        }
        //Pagination���� ������ �������� ���ý� ȣ��Ǵ� �Լ�
        public async Task<IActionResult> OnGetPageAsync(int pages, int boardId)
        {
            Pagination = await Pagination<PostModel>.CreateAsync(manager.GetPopularPosts(boardId), pages);
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