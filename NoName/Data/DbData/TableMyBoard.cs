using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    public class TableMyBoard
    {
        [Key]
        public int BoardSeq { get; set; }

        // TableDataJob to TableMyBoard => 1:n Relationship
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


        // TableMyBoard To TablePopularPost => 1:n Relationship
        public ICollection<TableMyPost> MyPosts { get; set; }
    }
}
