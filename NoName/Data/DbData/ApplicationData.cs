using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

/**
 * 전체 어플리케이션에서 사용될 데이터 관련 테이블들
 */
namespace NoName.Data.DbData
{
    /*
     * 직업
     */
    public class Job
    {
        [Key]
        public int JobCode { get; set; }
        public string JobName { get; set; }
    }

    /*
     * 게시판
     */
    public class Board
    {
        /*
         * 전체웹에서의 게시판 고유번호?
         * 직업홈에서의 게시판 코드?
         * 게시판 한글이름
         * 게시판 연결된 JobCode (FK) >> Job 에서 다수 관계
         */
        [Key]
        public int BoardNumber { get; set; }
        public int BoardCode { get; set; }
        public string BoardName { get; set; }

        [ForeignKey("Job")]
        public virtual int JobCode { get; set; }
    }

    /*
     * 게시물
     */
    public class Post
    {
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
        public bool hasNewComment { get; set; }

        // 최초 생성 시간
        public DateTime CreateTime { get; set; }
        // 내용 (최초 업로드 내용)
        public string InitialContent { get; set; }
        // 최종 수정 시간
        public DateTime LastModifiedTime { get; set; }

        // 삭제된 글인지
        public bool isDeleted { get; set; }
        // 삭제 시각
        public DateTime DeletedTime { get; set; }
    }

    /*
     * 댓글/대댓글
     */
    public class Comment
    {
        [Key]
        public int CommentNumber { get; set; }

        /*
         * 분리되어있는 User DB 에서 맞춰질 댓글단 유저의 아이디
         */
        public string UserId { get; set; }

        [ForeignKey("Post")]
        public int PostNumber { get; set; }
        public string Content { get; set; }

        /*
         * 대댓글용 속성
         * >> 상위 댓글 번호
         * >> 하위 댓글 번호들
         */
        public int ParentCommentNumber { get; set; }
        //public List<int> SeqCommentNumber { get; set; }

    }

    /*
     * 직업별 연봉
     * >> 성별, 나이, 연차 등에 따라 나누려면
     * >> User 정보와 연관이 필요함
     */
    public class Salary
    {
        [Key]
        public int SalaryNumber { get; set; }
        public int Average { get; set; }

        /*
         * 남/여 연봉평균, 계산해서 넣을지 뺄 때 계산하고 없앨지
         */
        public int MaleAverage { get; set; }
        public int FemaleAverage { get; set; }

        public int Minimum { get; set; }
        public int Maximum { get; set; }

        [ForeignKey("Job")]
        public virtual int JobCode { get; set; }
    }

    /*
     * 쪽지
     */
    public class Message
    {
        [Key]
        public int MessageNumber { get; set; }
        public string Content { get; set; }

        /*
         * 보낸이/받는이 아이디는 DB에 저장되지만, 쪽지 자체에서 확인은 불가함? (익명)
         */
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }


}
