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
    /*
     * List of List => PostPreview of BoardPreview
     */
    public class PostPreview : List<TablePost>
    {
        public int BoardId { get; private set; }
        public string BoardName { get; private set; }

        public PostPreview(List<TablePost> items, int boardId, string boardName)
        {
            BoardId = boardId;
            BoardName = boardName;
            this.AddRange(items);
        }
    }
    public class BoardPreview : List<PostPreview>
    {
        private readonly DataDbManager manager;

        public int JobCode { get; private set; }
        //보드의 개수
        public int BoardCount { get; private set; }
        //Preview할 Post개수
        public int PostCount { get; private set; }

        public BoardPreview(int jobCode, int boardCount, int postCount)
        {
            manager = DataDbManager.GetInstance();
            JobCode = jobCode;
            BoardCount = boardCount;
            PostCount = postCount;

            for (int i = 0; i < BoardCount; i++)
            {
                int id = i + 1;
                this.Add(new PostPreview(manager.GetPosts(id, PostCount).ToList(), id, manager.GetBoardName(id)));
            }
        }
        public static BoardPreview CreatePreviewList(int listNumber) //listNumber=> 나타낼 post 수
        {
            DataDbManager manager = DataDbManager.GetInstance();
            //usermanager에서 JobCode 가져와야함
            int jobCode = 1;
            //공지사항(id=4444)을 제외한 모든 게시판
            int boardCount = manager.GetBoardCount(jobCode) - 1;
            return new BoardPreview(jobCode, boardCount, listNumber);
        }
    }
}