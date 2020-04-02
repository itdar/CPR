using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NoName.Enumeration
{
    public abstract class Enumerator : IComparable
    {
        public string Name { get; private set; }

        private int Id { get; set; }

        protected Enumerator(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public override string ToString() => Name;
        public int GetBoardId(int jobCode)
        {
            return jobCode + Id;
        }

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
        // range : 1~50/51~70/71~99
        public static IEnumerable<T> GetPart<T>(int startId , int range) where T : Enumerator
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>().Where(f => f.Id >= startId && f.Id < startId + range);
        }
        public int CompareTo(object other) => Id.CompareTo(((Enumerator)other).Id);
    }
    public class BoardType : Enumerator
    {
        //반대로 Job을 1~999로 잡고 BoardId를 1000~으로 잡아서 결정해도 됨.
        /*
         * 설명 : 유저 관련 게시판 
         * 번호 : 1 ~ 50
         */
        public static BoardType Free = new BoardType(1, "자유게시판");
        public static BoardType Secret = new BoardType(2, "비밀게시판");
        public static BoardType Info = new BoardType(3, "정보게시판");
        public static BoardType PR = new BoardType(4, "홍보게시판");
        public static BoardType Best = new BoardType(5, "Best게시판");

        /*
         * 설명 : 게시된 글들의 정보만 가져와 보여주는 형태의 게시판
         * 번호 : 51 ~ 70
         */
        public static BoardType Hot = new BoardType(51, "Hot게시판");
        public static BoardType RealTime = new BoardType(52, "실시간인기글");
        public static BoardType Weekly = new BoardType(53, "주간인기글");

        /*
         * 설명 : 관리자 관련 게시판
         * 번호 : 71 ~ 99
         */
        public static BoardType Notice = new BoardType(99, "공지사항");

        public BoardType(int id, string name) : base(id, name) { }
    }
}
