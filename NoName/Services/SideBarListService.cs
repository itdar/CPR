using NoName.Data;
using NoName.Data.DbData;
using NoName.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace NoName.Services
{
    public class SideBarListService
    {
        int jobCode = 100;
        private int listNumber;
        public int ListNumber
        {
            get => listNumber;
            set => listNumber = value;
        }
        public List<TablePost> ListHotPosts()
        {
            DataDbManager manager = DataDbManager.GetInstance();
            return new List<TablePost>(manager.GetPosts(PopularBoardType.Hot.GetBoardId(jobCode), ListNumber).ToList());
        }
        public List<TablePost> ListRealTimePosts()
        {
            DataDbManager manager = DataDbManager.GetInstance();
            return new List<TablePost>(manager.GetPosts(PopularBoardType.RealTime.GetBoardId(jobCode), ListNumber).ToList());
        }
        public List<TablePost> ListWeeklyPosts()
        {
            DataDbManager manager = DataDbManager.GetInstance();
            return new List<TablePost>(manager.GetPosts(PopularBoardType.Weekly.GetBoardId(jobCode), ListNumber).ToList());
        }
    }
}
