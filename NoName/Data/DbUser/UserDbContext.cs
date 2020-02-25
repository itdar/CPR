using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NoName.Data
{
    /*
     * 씨샾 entity 클래스에서 DB 를 DbContext 로 표현함
     * 아래의 DbSet 에 들어가는 클래스들은 DB 하위의 Table 들 이다.
     * 
     * 프로필 등 user 세부 내용 테이블로 빼려고 헀던 것들은 ApplicationUser로 통합되었고,
     * 다수 연관관계가 필요한 MyJob (직업코드들) 테이블만 별도로 살려두었다.
     */
    public class UserDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<MyJob> MyJob { get; set; }

        /*
         * 다대다 관계 (User - Job) 를 위한 커넥터 역할 Table
         */
        public DbSet<UserJobConnector> Connector {get;set;}


        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
    }
}
