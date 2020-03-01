using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace NoName.Pages
{
    public class TermsOfServiceModel : PageModel
    {
        private readonly ILogger<TermsOfServiceModel> _logger;

        public TermsOfServiceModel(ILogger<TermsOfServiceModel> logger)
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
