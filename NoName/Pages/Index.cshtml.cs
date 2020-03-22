using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.Data;

namespace NoName.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;

        public IndexModel(UserContext userContext, ILogger<IndexModel> _logger)
        {
            // Manager 동작하는지 확인하고 지우거나 해야함
            UserDbManager.SetContext(userContext);
            UserDbManager.GetInstance().GetAllUserJob();

            logger = _logger;
        }

        public void OnGet()
        {

        }
        public void OnPost()
        {

        }
    }
}
