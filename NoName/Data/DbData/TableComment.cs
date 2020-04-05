using Microsoft.AspNetCore.Identity;
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
        public int CommentSeq { get; set; }

        // TablePost to TableCommment => 1:n Relationship
        [ForeignKey("Post")]
        public int PostNumber { get; set; }
        public TablePost Post { get; set; }

        public int ParentCommentNumber { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public int LikeCount { get; set; }

        public string userId { get; set; }
        /*
         * 대댓글용 속성
         * >> 상위 댓글 번호
         * >> 하위 댓글 번호들
         */
        //public List<int> SeqCommentNumber { get; set; }
    }
}
