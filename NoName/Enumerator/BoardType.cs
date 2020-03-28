using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NoName.Enumerator
{
    public abstract class Enumerator : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumerator(int id, string name)
        {
            Id = id;
            Name = name;
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
        public int CompareTo(object other) => Id.CompareTo(((Enumerator)other).Id);
    }
    public class BoardType : Enumerator
    {
        //1. HOT게시판
        //2. 실시간 인기글
        //3. 주간 인기글
        //4. 자유게시판
        //5. 비밀게시판
        //6. 정보게시판
        //7. 홍보게시판
        //4444. 공지사항
        public static BoardType Hot = new BoardType(1, "Hot게시판");
        public static BoardType RealTime = new BoardType(2, "실시간 인기글");
        public static BoardType WeekTime = new BoardType(3, "주간 인기글");
        public static BoardType Free = new BoardType(4, "자유게시판");
        public static BoardType Secret = new BoardType(5, "비밀게시판");
        public static BoardType Info = new BoardType(6, "정보게시판");
        public static BoardType PR = new BoardType(7, "홍보게시판");
        public static BoardType Notice = new BoardType(4444, "공지사항");

        public BoardType(int id, string name) : base(id, name) { }
    }
}
