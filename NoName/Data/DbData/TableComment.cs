using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    /*
      * 댓글/대댓글
      */
    public class TableComment
    {
        [Key]
        public int CommentNumber { get; set; }

        /*
         * 분리되어있는 User DB 에서 맞춰질 댓글단 유저의 아이디
         */
        public string UserId { get; set; }

        public string Content { get; set; }

        /*
         * 대댓글용 속성
         * >> 상위 댓글 번호
         * >> 하위 댓글 번호들
         */
        public int ParentCommentNumber { get; set; }
        //public List<int> SeqCommentNumber { get; set; }

    }
}
