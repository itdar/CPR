using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace NoName.Pages
{
    public class NoticeModel : PageModel
    {
        private readonly ILogger<NoticeModel> _logger;

        public NoticeModel(ILogger<NoticeModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {

        }
        public void OnPost()
        {

        }
    }
}
