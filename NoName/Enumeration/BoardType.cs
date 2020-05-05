using NoName.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NoName.Enumeration
{
    public abstract class Enumerator : IComparable
    {
        private readonly DataDbManager manager;
        public string Name { get; private set; }
        public int Code { get; private set; }
        public int Id { get; private set; }

        protected Enumerator(int code, string name)
        {
            manager = DataDbManager.GetInstance();
            Code = code;
            Name = name;
            //Need to take a jobCode in userinformation
            int jobCode = 1;
            Id = manager.GetBoardCount(jobCode) > 0 ? manager.GetBoardId(jobCode, Code) : -1;
        }
        public override string ToString() => Name;

        public int Length<T>() => typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly).Length;
        public static IEnumerable<T> GetAll<T>() where T : Enumerator
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }
        public static int GetUserBoardsCount<T>() where T : Enumerator
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>().Count() - 1;
        }
        public int CompareTo(object other) => Code.CompareTo(((Enumerator)other).Code);
    }
    public class BoardType : Enumerator
    {
        //Writer is User
        public static BoardType Free = new BoardType(1, "자유게시판");
        public static BoardType Secret = new BoardType(2, "비밀게시판");
        public static BoardType Info = new BoardType(3, "정보게시판");
        public static BoardType PR = new BoardType(4, "홍보게시판");
        public static BoardType Best = new BoardType(5, "Best게시판");

        //Writer is Administrator
        public static BoardType Notice = new BoardType(99, "공지사항");

        public BoardType(int code, string name) : base(code, name) { }
    }
    public class PopularBoardType : Enumerator
    {
        public static PopularBoardType Hot = new PopularBoardType(1, "Hot게시판");
        //public static PopularBoardType RealTime = new PopularBoardType(2, "실시간인기글");
        public static PopularBoardType Weekly = new PopularBoardType(2, "주간인기글");

        public PopularBoardType(int code, string name) : base(code, name) { }
    }
    public class MyBoardType : Enumerator
    {
        public static MyBoardType MyPost = new MyBoardType(1, "내가 쓴 글");
        public static MyBoardType MyComment = new MyBoardType(2, "내가 쓴 댓글");
        public static MyBoardType MyScrap = new MyBoardType(3, "내 스크랩");
        public static MyBoardType QnA = new MyBoardType(4, "문의사항");

        public MyBoardType(int code, string name) : base(code, name) { }
    }
}
