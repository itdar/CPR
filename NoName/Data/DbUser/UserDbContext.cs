using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NoName.Data
{
    /*
     * 씨샾 entity 클래스에서 DB 를 DbContext 로 표현함
     * 아래의 DbSet 에 들어가는 클래스들은 DB 하위의 Table 들 이다.
     */
    public class UserDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Authentication> Authentication { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<MyJob> MyJob { get; set; }

        public UserDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
