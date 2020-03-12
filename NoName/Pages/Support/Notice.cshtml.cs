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
        //Test�� Message
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
            //�ܷ�Ű ���� �Ұ� -> ���� ��������� �� *20.03.11
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