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
    public abstract class BoardModel
    {
        /*
         * 전체웹에서의 게시판 고유번호?
         * 직업홈에서의 게시판 코드?
         * 게시판 한글이름
         * 게시판 연결된 JobCode (FK) >> Job 에서 다수 관계
         */
        [Key]
        public int BoardId { get; set; }

        // TableDataJob to TableBoard => 1:n Relationship
        public int JobCode { get; set; }
        public TableDataJob Job { get; set; }

        public int BoardCode { get; set; }
        public string BoardName { get; set; }
    }
    public class TableBoard : BoardModel
    {
        // TableBoard To TablePost => 1:n Relationship
        public ICollection<TablePost> Posts { get; set; }
    }
}
