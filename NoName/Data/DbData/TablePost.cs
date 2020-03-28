using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    /*
     * 게시물
     */
    public class TablePost
    {
        // TableBoard to TablePost => 1:n Relationship
        public int BoardId { get; set; }
        public TableBoard Board { get; set; }

        [Key]
        public int PostNumber { get; set; }
        public string Id { get; set; }
        public int CategoryNumber { get; set; }

        // 제목
        public string Title { get; set; }
        // 내용 (최종 수정된 내용)
        public string Content { get; set; }

        //조회수
        public int? ViewCount { get; set; } = 0;
        //좋아요
        public int? LikeCount { get; set; } = 0;
        //싫어요
        public int? DislikeCount { get; set; } = 0;
        //새로운 댓글 달렸는지 여부
        public bool? HasNewComment { get; set; } = false;

        // 최초 생성 시간
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        // 내용 (최초 업로드 내용)
        public string InitialContent { get; set; }
        // 최종 수정 시간 initial value = null
        public DateTime? LastModifiedTime { get; set; }

        // 삭제된 글인지
        public bool? IsDeleted { get; set; } = false;
        // 삭제 시각 initial value = null
        public DateTime? DeletedTime { get; set; }


        // TablePost to TableCommment => 1:n Relationship
        private ICollection<TableComment> comments;
        public ICollection<TableComment> Comments
        {
            get => comments;
            set => comments = value;
        }
    }
}
