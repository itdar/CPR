using NoName.Data;
using NoName.Data.DbData;
using NoName.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace NoName.Services
{
    public class SideBarListService
    {
        int jobCode = 1000;
        private int listNumber;
        public int ListNumber
        {
            get => listNumber;
            set => listNumber = value;
        }
        public List<PostModel> ListHotPosts()
        {
            DataDbManager manager = DataDbManager.GetInstance();
            return new List<PostModel>(manager.GetPopularPosts(PopularBoardType.Hot.GetBoardId(jobCode), ListNumber).ToList());
        }
        public List<PostModel> ListRealTimePosts()
        {
            DataDbManager manager = DataDbManager.GetInstance();
            return new List<PostModel>(manager.GetPopularPosts(PopularBoardType.RealTime.GetBoardId(jobCode), ListNumber).ToList());
        }
        public List<PostModel> ListWeeklyPosts()
        {
            DataDbManager manager = DataDbManager.GetInstance();
            return new List<PostModel>(manager.GetPopularPosts(PopularBoardType.Weekly.GetBoardId(jobCode), ListNumber).ToList());
        }
    }
}
