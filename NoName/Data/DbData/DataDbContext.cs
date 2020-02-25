using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    /*
     * 씨샾 entity 클래스에서 DB 를 DbContext 로 표현함
     * 아래의 DbSet 에 들어가는 클래스들은 DB 하위의 Table 들 이다.
     */
    public class DataDbContext : DbContext
    {
        public DbSet<Job> Job { get; set; }
        public DbSet<JobPage> JobPage { get; set; }
        public DbSet<Board> Board { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Salary> Salary { get; set; }
        public DbSet<Message> Message { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
        }

    }
}
