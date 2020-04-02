using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.BackendClass.Paging;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.Board
{
    public class FindResultModel : PageModel
    {
        private readonly ILogger<FindResultModel> _logger;
        private readonly DataDbManager manager;
        public Pagination<TablePost> Pagination { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public FindResultModel(ILogger<FindResultModel> logger)
        {
            manager = DataDbManager.GetInstance();
            _logger = logger;
        }
        /*public async Task<IActionResult> OnGetAsync()
        {
            SearchString = Request.Form["searchString"];
            //�ܷ�Ű ���� �Ұ� -> ���� ��������� �� *20.03.11
            Pagination = await Pagination<TablePost>.CreateAsync(_context.Post, 1);
            return Page();
        }*/
        //User�� JobCode�� �´� �Խ��ǿ��� �˻��ϴ� ��� �߰� �ʿ� *20.03.18
        public async Task<IActionResult> OnGetPageAsync(int pages)
        {
            SearchString = Request.Form["searchString"];
            Pagination = await Pagination<TablePost>.CreateAsync(manager.SearchInBoth(SearchString), pages);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            SearchString = Request.Form["searchString"];
            Pagination = await Pagination<TablePost>.CreateAsync(manager.SearchInBoth(SearchString), 1);
            return Page();
        }
    }
}
