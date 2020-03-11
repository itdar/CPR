using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    /*
     * 게시물
     */
    public class TablePost
    {
        private ICollection<TableComment> comments;
        public ICollection<TableComment> Comments
        {
            get => comments;
            set => comments = value;
        }

        [Key]
        public int PostNumber { get; set; }

        public string UserId { get; set; }
        public int CategoryNumber { get; set; }

        // 제목
        public string Title { get; set; }
        // 내용 (최종 수정된 내용)
        public string Content { get; set; }

        //조회수
        public int ViewCount { get; set; }
        //좋아요
        public int LikeCount { get; set; }
        //싫어요
        public int DislikeCount { get; set; }
        //새로운 댓글 달렸는지 여부
        public bool HasNewComment { get; set; }

        // 최초 생성 시간
        public DateTime CreateTime { get; set; }
        // 내용 (최초 업로드 내용)
        public string InitialContent { get; set; }
        // 최종 수정 시간
        public DateTime LastModifiedTime { get; set; }

        // 삭제된 글인지
        public bool IsDeleted { get; set; }
        // 삭제 시각
        public DateTime DeletedTime { get; set; }
    }
}
