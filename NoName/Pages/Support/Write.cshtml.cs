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
        private readonly DataDbManager manager;
        [BindProperty]
        public TablePost TablePost { get; set; }
        public WriteModel(ILogger<WriteModel> logger)
        {
            manager = DataDbManager.GetInstance();
            _logger = logger;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await manager.AddPostAsync(TablePost);
            return RedirectToPage("./Notice");
        }
    }
}
