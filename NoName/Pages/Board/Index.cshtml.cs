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
            //정상 호출시 직업 게시판 타이틀 하단에 Message => Message + DateTime.Now 출력
            Message += $" Server time is { DateTime.Now }";

        }
    }
}
