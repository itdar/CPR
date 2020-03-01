using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace NoName.Pages
{
    public class ServiceAgreementModel : PageModel
    {
        private readonly ILogger<ServiceAgreementModel> _logger;

        public ServiceAgreementModel(ILogger<ServiceAgreementModel> logger)
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
