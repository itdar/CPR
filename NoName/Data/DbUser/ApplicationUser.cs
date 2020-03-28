using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NoName.Data.DbData;
using NoName.Data.DbUser;

/**
 * 유저와 관련된 각각의 Table 클래스들
 */
namespace NoName.Data
{
    /*
     * 다른 유저 내용 (Profile) 등은 ApplicationUser 안으로 합쳤으나,
     * 직업의 경우는 User 가 다수 직업코드를 가지고 있을 수 있어서
     * 추후에 foreign key 로드 확인해서 1:다수 연관관계로 저장/로드 해야함
     */


    /**
     * 로그인 관련 페이지를 Scaffolding 해서 identity 관련 페이지를 사용하기 때문에
     * 이미 뷰에서 보여지고 처리되는 데이터가 코딩되어 있어서 필요한 테이블 정보가 정해져 있음
     */
    public class ApplicationUser : IdentityUser
    {
        /*
         * myJobs property
         * 해당 유저가 갖고 있는 직업들 코드번호
        */
        public ICollection<TableUserJob> MyJobCodes
        {
            get => MyJobCodes;
            set => MyJobCodes = value;
        }
        public ICollection<TablePost> MyPosts
        {
            get => MyPosts;
            set => MyPosts = value;
        }
        public ICollection<TableComment> MyComments
        {
            get => MyComments;
            set => MyComments = value;
        }
        public ICollection<TableMessage> MyMessages
        {
            get => MyMessages;
            set => MyMessages = value;
        }


        /*
         * 계정 생성 날짜
         */
        public DateTime CreateDateTime { get; set; }

        /*
         * 하루에 올라갈 수 있는 방문수 최대는 1
         */
        public int VisitCount { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Gender { get; set; }

        /*
         * SMS 관련 수신 여부 (필요한지?)
         */
        public bool ReceiveSMS { get; set; }
        

        /*
         * 인증 서류 업로드된 파일들 서버에서의 경로
         * 인증 서류 업로드 날짜
         * 인증 서류 확인통과 날짜
         */
        public string AuthenticationFilePath { get; set; }
        public DateTime AuthenticationFileUploadDate { get; set; }
        public DateTime AuthenticationFileConfirmDate { get; set; }

        /*
         * 직업 인증서류 몇단계인지, 접근권한 레벨
         * e.g. 0 >> 안됨, 1 >> 재직증명, 2 >> 원천징수 ..
         * 재직증명까지는 해당 직업 게시판만 오픈
         * 원천징수까지 증명해야 해당 직업 연차별 연봉 공개 등
         */
        public int PermissionLevel { get; set; }

        /*
         * 해당 직업의 홈에서 접근 가능한 몇번째 게시판의 매니저인지 확인 번호 
         * (요청되어 만들어진 커스텀 게시판)
         * 매니저 테이블을 만들어서 관리하는것이 나을듯 생각됩니다. -SooHwan 20.03.27
         */
        public int ManagerNumber { get; set; }

        /*
         * Job을 관리할 컬럼
         * 
         * 
        public int? MainJob { get; set; }
        public int? SubJob { get; set; }
        public int? ThirdJob { get; set; }
        */


        /*
         * 위의 IdentityUser 에 이미 있어서 따로 안만들고 사용하는 속성들 및 세부내용
         * EmailAddress, PhoneNumber, EmailConfirmed, Id(PK)
         * 
         * 
        //     Gets or sets the date and time, in UTC, when any user lockout ends.
        // Remarks:
        //     A value in the past means the user is not locked out.
        public virtual DateTimeOffset? LockoutEnd { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if two factor authentication is enabled for this
        //     user.
        [PersonalData]
        public virtual bool TwoFactorEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if a user has confirmed their telephone address.
        [PersonalData]
        public virtual bool PhoneNumberConfirmed { get; set; }
        //
        // Summary:
        //     Gets or sets a telephone number for the user.
        [ProtectedPersonalData]
        public virtual string PhoneNumber { get; set; }
        //
        // Summary:
        //     A random value that must change whenever a user is persisted to the store
        public virtual string ConcurrencyStamp { get; set; }
        //
        // Summary:
        //     A random value that must change whenever a users credentials change (password
        //     changed, login removed)
        public virtual string SecurityStamp { get; set; }
        //
        // Summary:
        //     Gets or sets a salted and hashed representation of the password for this user.
        public virtual string PasswordHash { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if a user has confirmed their email address.
        [PersonalData]
        public virtual bool EmailConfirmed { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized email address for this user.
        public virtual string NormalizedEmail { get; set; }
        //
        // Summary:
        //     Gets or sets the email address for this user.
        [ProtectedPersonalData]
        public virtual string Email { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized user name for this user.
        public virtual string NormalizedUserName { get; set; }
        //
        // Summary:
        //     Gets or sets the user name for this user.
        [ProtectedPersonalData]
        public virtual string UserName { get; set; }
        //
        // Summary:
        //     Gets or sets the primary key for this user.
        [PersonalData]
        public virtual TKey Id { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if the user could be locked out.
        public virtual bool LockoutEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets the number of failed login attempts for the current user.
        public virtual int AccessFailedCount { get; set; }
         * 
         */
    }
}
