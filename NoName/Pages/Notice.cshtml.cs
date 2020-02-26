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
        public class NoticeBoard
        {
            public int PostNumber { get; set; }
            public string UserId { get; set; }
            public int Category_Code { get; set; }
            public string Title { get; set; }
            public string Contents { get; set; }
            public int Views { get; set; }
            public int LikeCount { get; set; }
            public int DislikeCount { get; set; }
            public bool IsNewComment { get; set; }
            public DateTime CreateTime { get; set; }
            public DateTime LastModifiedTime { get; set; }
            public bool Deleted { get; set; }
            public DateTime DeletedTime { get; set; }
            public bool Initialization()
            {
                PostNumber = 1;
                UserId = "soohwan";
                Category_Code = 12;
                Title = "";
                Contents = "";
                Views = 123;
                LikeCount = 100;
                DislikeCount = 20;
                IsNewComment = true;
                CreateTime = DateTime.Now;
                LastModifiedTime = DateTime.Now;
                Deleted = false;
                DeletedTime = DateTime.Now;
                return true;
            }
        }


        private readonly ILogger<NoticeModel> _logger;

        public NoticeModel(ILogger<NoticeModel> logger)
        {
            _logger = logger;
        }

        public string Message { get; set; }
        public void OnGet()
        {

        }
        //Get이나 Post method 호출시 OnGet[Value]()와 OnPost[Value]() 형식으로 호출 됨. Value = Handler Name
        //함수 형태를 Async(비동기)로 변경가능.(await 사용)
        public IActionResult OnGetPost()
        {
            return RedirectToPage("Temp/Detail");
        }
        public void OnPost()
        {

        }
    }
}