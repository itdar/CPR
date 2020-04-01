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
        //Pagination���� ������ �������� ���ý� ȣ��Ǵ� �Լ�
        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            int JobCode = 100;
            Pagination = await Pagination<TablePost>.CreateAsync(manager.GetPosts(BoardType.Notice.GetBoardId(JobCode)), pages);
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