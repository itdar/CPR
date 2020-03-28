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
        public  List<BoardPreview> PreviewBoards { get; set; }
        public void OnGet(int numberOfBoard=7)
        {
            PreviewBoards = new List<BoardPreview>();
            int boardId;
            for (boardId = 1; boardId < 4; boardId++)
            {
                var postlist = manager.GetPosts(boardId, 2);
                var Preview = BoardPreview.CreatePreviewList(postlist, boardId, manager.GetBoardName(boardId));
                PreviewBoards.Add(Preview);
            }
            for (int boardid = 4; boardid <= numberOfBoard; boardid++)
            {
                var postlist = manager.GetPosts(boardid,4);
                var Preview= BoardPreview.CreatePreviewList(postlist, boardid, manager.GetBoardName(boardid));
                PreviewBoards.Add(Preview);
            }
        }
        public void OnPost()
        {

        }
    }
}
