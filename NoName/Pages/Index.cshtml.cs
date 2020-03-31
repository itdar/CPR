using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Pages.Shared;

namespace NoName.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataDbManager manager;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            manager= DataDbManager.GetInstance();
        }
        [BindProperty]
        public BoardPreview Preview { get; set; }
        public void OnGet()
        {
            int listNumber = 4;
            Preview = BoardPreview.CreatePreviewList(listNumber);
        }
        public void OnPost()
        {

        }
    }
}
