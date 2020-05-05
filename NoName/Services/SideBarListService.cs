using NoName.Data;
using NoName.Data.DbData;
using NoName.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace NoName.Services
{
    public class SideBarListService
    {
        //임시
        int jobCode = 1;
        public int HotBoardId { get; private set; }
        //public int RealTimeBoardId { get; private set; }
        public int WeeklyBoardId { get; private set; }

        private int listNumber;
        public int ListNumber
        {
            get => listNumber;
            set => listNumber = value;
        }
        public List<PostModel> ListHotPosts()
        {
            DataDbManager manager = DataDbManager.GetInstance();
            HotBoardId = manager.GetPopularBoardId(jobCode, PopularBoardType.Hot.Code);
            return new List<PostModel>(manager.GetPopularPosts(HotBoardId, ListNumber).ToList());
        }
        public List<PostModel> ListRealTimePosts()
        {
            //실시간 인기글은 DB에 두지 않기 때문에 DB에서 가져오지 않고 사이클마다 실시간으로 구해서 입력해주는 형식으로 변경
            DataDbManager manager = DataDbManager.GetInstance();
            HotBoardId = manager.GetPopularBoardId(jobCode, PopularBoardType.Hot.Code);
            return new List<PostModel>(manager.GetPopularPosts(HotBoardId, ListNumber).ToList());
        }
        public List<PostModel> ListWeeklyPosts()
        {
            DataDbManager manager = DataDbManager.GetInstance();
            WeeklyBoardId = manager.GetPopularBoardId(jobCode, PopularBoardType.Weekly.Code);
            return new List<PostModel>(manager.GetPopularPosts(WeeklyBoardId, ListNumber).ToList());
        }
    }
}
