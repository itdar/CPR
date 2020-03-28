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
            /*
             * TableBoard의 BoardNumber와 BoardId가 두 컬럼이 주키로 사용되기 때문에 직접설정(키가 두개인 경우는 'Key Attribute'사용 불가 like a [Key])
             * 또한 BoardId는 TablePost의 외래키로 사용되어 설정
             */
            //Set Two PrimaryKey
            modelBuilder.Entity<TableBoard>().HasKey(b => new { b.BoardNumber, b.BoardId });
            //Set Auto-incresement
            modelBuilder.Entity<TableBoard>().Property(b => b.BoardNumber).ValueGeneratedOnAdd();

            modelBuilder.Entity<TableBoard>()
                .HasOne(b => b.Job)
                .WithMany(j => j.Boards)
                .HasForeignKey(b => b.JobCode)
                .HasPrincipalKey(j => j.JobCode);

            //Set Alternate Key(BoardId) to ForeignKey And PrincipalKey
            modelBuilder.Entity<TablePost>()
                .HasOne(p => p.Board)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BoardId)
                .HasPrincipalKey(b => b.BoardId);

            //Set Two PrimaryKey
            modelBuilder.Entity<TableDataJob>().HasKey(j => new { j.Number, j.JobCode });
            modelBuilder.Entity<TableDataJob>().Property(j => j.Number).ValueGeneratedOnAdd();

            //Set a ForeignKey
            modelBuilder.Entity<TableMessage>()
                .HasOne(m => m.ApplicationUser)
                .WithMany(u => u.MyMessages)
                .HasForeignKey(m => m.SenderId)
                .HasPrincipalKey(u => u.Id);

            modelBuilder.Entity<TableSalary>()
                .HasOne(s => s.Job)
                .WithOne(j => j.Salary)
                .HasForeignKey<TableDataJob>(j => j.JobCode)
                .HasPrincipalKey<TableSalary>(s => s.JobCode);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DataDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        public DataContext()
        {
        }
    }
}
