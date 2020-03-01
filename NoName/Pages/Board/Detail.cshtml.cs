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

            //postNumber�� �Ű������� asp-rout-[value] �� value�� ����
            //postNumber�� �ش� ���� id �� ������ ViewData["postTitle"]�̷������� cshtml ���� �����ָ� �ɵ�

            ViewData["PostId"] = postNumber;
        }

        public void OnGet()
        {
            ViewData["PostId"] = "basic";
        }
    }
}
