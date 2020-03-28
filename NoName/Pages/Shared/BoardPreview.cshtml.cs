using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.Data;
using NoName.Data.DbData;

namespace NoName.Pages.Shared
{
    public class BoardPreview
    {
        public BoardPreview(List<TablePost> items,int boardId,string boardName){
            PostList= items;
            BoardId = boardId;
            BoardName = boardName;
        }

        [BindProperty]
        public int  BoardId { get; set; }
        [BindProperty]
        public List<TablePost> PostList { get; set; }
        [BindProperty]
        public string BoardName { get; set; }

        public static BoardPreview CreatePreviewList(IQueryable<TablePost> source,int boardId,string boardName) //listNumber=> 나타낼 post 수
        {
            return new BoardPreview(source.ToList(), boardId,boardName);
        }
    }
}
