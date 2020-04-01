using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

/*
 * IEnumerable<T> is read-only
 * You can add and remove items to an ICollection<T>
 * You can do random access (by index) to a List<T>
 */
namespace NoName.Data.DbData
{
    /*
     * 게시판
     */
    public class TableBoard
    {

        /*
         * 전체웹에서의 게시판 고유번호?
         * 직업홈에서의 게시판 코드?
         * 게시판 한글이름
         * 게시판 연결된 JobCode (FK) >> Job 에서 다수 관계
         */
        [Key]
        public int BoardSeq { get; set; }

        // TableDataJob to TableBoard => 1:n Relationship
        public int JobCode { get; set; }
        public TableDataJob Job { get; set; }

        /*
         * JobCode에 따라 BoardId가 결정되어야함
         * ex) JobCode 규칙을 100*n이라 하면 100*1 + 1(BoardId) = 101 => JobCode 100의 자유게시판
         * 그래야만 BoardId로 Post를 식별 할 수 있음 그렇지 않으면 BoardId와 JobCode가 항상 붙어다녀야함, 하지만 JobCode는 외래키
         */
        [Key]
        public int BoardId { get; set; }
        public string BoardName { get; set; }


        // TableBoard To TablePost => 1:n Relationship
        public ICollection<TablePost> Posts { get; set; }
        
        // TableBoard To TableHotPost => 1:n Relationship
        public ICollection<TableHotPost> HotPosts { get; set; }
    }
}
