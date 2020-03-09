using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Web;

namespace NoName.Pages.Board
{
    public class IndexModel : PageModel
    {
        public string Message { get; private set; } = "PageModel in C#";


        public void OnGet()
        {

        }
        public void OnPost()
        {
            //���� ȣ��� ���� �Խ��� Ÿ��Ʋ �ϴܿ� Message => Message + DateTime.Now ���
            Message += $" Server time is { DateTime.Now }";

        }
    }
}
