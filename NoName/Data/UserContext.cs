using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoName.Data.DbData;
using NoName.Data.DbUser;
using System;

namespace NoName.Data
{
    /*
     * 씨샾 entity 클래스에서 DB 를 DbContext 로 표현함
     * 아래의 DbSet 에 들어가는 클래스들은 DB 하위의 Table 들 이다.
     * 
     * 프로필 등 user 세부 내용 테이블로 빼려고 헀던 것들은 ApplicationUser로 통합되었고,
     * 다수 연관관계가 필요한 MyJob (직업코드들) 테이블만 별도로 살려두었다.
     */
    public class UserContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<TableUserJob> UserJob { get; set; }
        public DbSet<TableManager> Manager { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UserDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        public UserContext()
        {
        }

        public static implicit operator UserContext(DataContext v)
        {
            throw new NotImplementedException();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer("UserDb");
    }
}
