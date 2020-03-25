using Microsoft.EntityFrameworkCore;
using NoName.Data.DbData;

//namespace NoName.Data로 경로에 맞게 변경하는게 좋음.20.3.14
namespace NoName.Data
{
    /*
     * 씨샾 entity 클래스에서 DB 를 DbContext 로 표현함
     * 아래의 DbSet 에 들어가는 클래스들은 DB 하위의 Table 들 이다.
     */
    public class DataContext : DbContext
    {
        public DbSet<TableDataJob> Job { get; set; }
        public DbSet<TableBoard> Board { get; set; }
        public DbSet<TablePost> Post { get; set; }
        public DbSet<TableComment> Comment { get; set; }
        public DbSet<TableSalary> Salary { get; set; }
        public DbSet<TableMessage> Message { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableDataJob>().ToTable("TableDataJob");
            modelBuilder.Entity<TableBoard>().ToTable("TableBoard");
            modelBuilder.Entity<TablePost>().ToTable("TablePost");
            modelBuilder.Entity<TableComment>().ToTable("TableComment");
            modelBuilder.Entity<TableSalary>().ToTable("TableSalary");
            modelBuilder.Entity<TableMessage>().ToTable("TableMessage");

            //Set Primary Key
            modelBuilder.Entity<TableBoard>().HasKey(c => new { c.BoardNumber, c.BoardId });
            //Set Auto-incresement
            modelBuilder.Entity<TableBoard>().Property(f => f.BoardNumber).ValueGeneratedOnAdd();

            //Set Alternate Key(BoardId) to ForeignKey
            modelBuilder.Entity<TablePost>()
                .HasOne(p => p.Board)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BoardId)
                .HasPrincipalKey(b => b.BoardId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DataDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        public DataContext()
        {
        }
    }
}
