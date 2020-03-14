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
        /*
        private readonly DataContext _context;
        public NoticeModel(DataContext context, ILogger<NoticeModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        */
        private readonly ILogger<NoticeModel> _logger;
        private readonly IDataDbManager _manager;
        public Pagination<TablePost> Pagination { get; set; }
        //Constructor
        public NoticeModel(IDataDbManager manager, ILogger<NoticeModel> logger)
        {
            _manager = manager;
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            //�ܷ�Ű ���� �Ұ� -> ���� ��������� �� *20.03.11
            Pagination = await Pagination<TablePost>.CreateAsync(_manager.GetPosts(1), 1);
            return Page();
        }
        //Pagination���� ������ �������� ���ý� ȣ��Ǵ� �Լ�
        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            //var manager = DataDbManager.GetInstance();
            Pagination = await Pagination<TablePost>.CreateAsync(_manager.GetPosts(1), pages);
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