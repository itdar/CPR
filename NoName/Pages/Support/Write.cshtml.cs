using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.Support
{
    public class WriteModel : PageModel
    {
        private readonly ILogger<WriteModel> _logger;
        private readonly IDataDbManager _manager;
        [BindProperty]
        public TablePost TablePost { get; set; }
        public WriteModel(IDataDbManager manager, ILogger<WriteModel> logger)
        {
            _manager = manager;
            _logger = logger;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _manager.AddPostAsync(TablePost);
            return RedirectToPage("./Notice");
        }
    }
}
