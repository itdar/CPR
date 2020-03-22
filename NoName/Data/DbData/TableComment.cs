using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int ParentCommentNumber { get; set; }
        public int PostNumber { get; set; }
        [ForeignKey("PostNumber")]
        public TablePost TablePost { get; set; }
        public string Content { get; set; }

        public DateTime CreatedTime { get; set; }
        public int LikeCount { get; set; }


        /*
         * 대댓글용 속성
         * >> 상위 댓글 번호
         * >> 하위 댓글 번호들
         */
        //public List<int> SeqCommentNumber { get; set; }
        public string UserId { get; set; }

    }
}
