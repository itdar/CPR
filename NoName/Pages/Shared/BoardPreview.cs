using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.Data;
using NoName.Data.DbData;
using NoName.Enumeration;

namespace NoName.Pages.Shared
{
    /*
     * List of List => PostPreview of BoardPreview
     */
    public class PostPreview : List<PostModel>
    {
        public int BoardId { get; private set; }
        public string BoardName { get; private set; }

        public PostPreview(List<PostModel> items, int boardId, string boardName)
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
        public int ListNumber { get; private set; }

        public BoardPreview(int jobCode, int boardCount, int listNumber)
        {
            manager = DataDbManager.GetInstance();
            JobCode = jobCode;
            BoardCount = boardCount;
            ListNumber = listNumber;

            for (int i = 0; i < BoardCount; i++)
            {   
                var board = BoardType.GetAll<BoardType>().ElementAt(i);
                int boardId = manager.GetBoardId(JobCode, board.Code);
                this.Add(new PostPreview(manager.GetPosts(boardId, ListNumber).ToList(), boardId, board.Name));
            }
        }
        public static BoardPreview CreatePreviewList(int listNumber) //listNumber=> 나타낼 post 수
        {
            //usermanager에서 JobCode 가져와야함
            int jobCode = 1;
            //BoardType에서 정해진 Board들의 개수
            int boardCount = BoardType.GetUserBoardsCount<BoardType>();
            //JobCode에 따라 보드의 개수가 다른경우 DB에서 가져옴 (사이드바의 핫게가 포함됨)
            //int boardCount = DataDbManager.GetInstance().GetBoardCount(jobCode);
            return new BoardPreview(jobCode, boardCount, listNumber);
        }
    }
}