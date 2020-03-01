using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NoName.Pages.Board
{
    public class DetailModel : PageModel
    {

        public void OnGetPost(int postNumber)
        {

            //postNumber의 매개변수는 asp-rout-[value] 의 value와 동일
            //postNumber에 해당 글의 id 가 들어오고 ViewData["postTitle"]이런식으로 cshtml 에서 보여주면 될듯

            ViewData["PostId"] = postNumber;
        }

        public void OnGet()
        {
            ViewData["PostId"] = "basic";
        }
    }
}
