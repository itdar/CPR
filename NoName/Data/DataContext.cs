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
        public DbSet<TablePopularBoard> PopularBoard { get; set; }
        public DbSet<TableMyBoard> MyBoard { get; set; }
        public DbSet<TablePost> Post { get; set; }
        public DbSet<TablePopularPost> PopularPost { get; set; }
        public DbSet<TableMyPost> MyPost { get; set; }
        public DbSet<TableComment> Comment { get; set; }
        public DbSet<TableSalary> Salary { get; set; }
        public DbSet<TableMessage> Message { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*********************************************JOB*********************************************/
            //Set Two PrimaryKey
            modelBuilder.Entity<TableDataJob>().HasKey(j => new { j.DataJobSeq, j.JobCode });
            //Set Auto-incresement
            modelBuilder.Entity<TableDataJob>().Property(j => j.DataJobSeq).ValueGeneratedOnAdd();

            /*********************************************BOARD*********************************************
             * TableBoard의 BoardNumber와 BoardId가 두 컬럼이 주키로 사용되기 때문에 직접설정(키가 두개인 경우는 'Key Attribute'사용 불가 like a [Key])
             * 또한 BoardId는 TablePost의 외래키로 사용되어 설정
             */
            //Set one of CompositeKey to ForeignKey
            modelBuilder.Entity<TableBoard>()
                .HasOne(b => b.Job)
                .WithMany(j => j.Boards)
                .HasForeignKey(b => b.JobCode)
                .HasPrincipalKey(j => j.JobCode);

            //Set one of CompositeKey to ForeignKey
            modelBuilder.Entity<TablePopularBoard>()
                .HasOne(b => b.Job)
                .WithMany(j => j.PopularBoards)
                .HasForeignKey(b => b.JobCode)
                .HasPrincipalKey(j => j.JobCode);

            //Set one of CompositeKey to ForeignKey
            modelBuilder.Entity<TableMyBoard>()
                .HasOne(b => b.Job)
                .WithMany(j => j.MyBoards)
                .HasForeignKey(b => b.JobCode)
                .HasPrincipalKey(j => j.JobCode);

            /*********************************************POST*********************************************/
            //Set Two PrimaryKey
            modelBuilder.Entity<TablePopularPost>().HasKey(p => new { p.PostSeq, p.PostNumber });
            //Set Auto-incresement
            modelBuilder.Entity<TablePopularPost>().Property(p => p.PostSeq).ValueGeneratedOnAdd();

            //Set Two PrimaryKey
            modelBuilder.Entity<TableMyPost>().HasKey(p => new { p.PostSeq, p.PostNumber });
            //Set Auto-incresement
            modelBuilder.Entity<TableMyPost>().Property(p => p.PostSeq).ValueGeneratedOnAdd();

            //Set one of CompositeKey to ForeignKey
            modelBuilder.Entity<TablePost>()
                .HasOne(p => p.Board)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BoardId)
                .HasPrincipalKey(b => b.BoardId);

            //Set one of CompositeKey to ForeignKey
            modelBuilder.Entity<TablePopularPost>()
                .HasOne(p => p.PopularBoard)
                .WithMany(b => b.PopularPosts)
                .HasForeignKey(h => h.BoardId)
                .HasPrincipalKey(b => b.BoardId);

            //Set one of CompositeKey to ForeignKey
            modelBuilder.Entity<TableMyPost>()
                .HasOne(p => p.MyBoard)
                .WithMany(b => b.MyPosts)
                .HasForeignKey(h => h.BoardId)
                .HasPrincipalKey(b => b.BoardId);

            /*********************************************COMMENT*********************************************/
            //Set Two PrimaryKey
            modelBuilder.Entity<TableComment>().HasKey(c => new { c.CommentSeq, c.PostNumber });
            //Set Auto-incresement
            modelBuilder.Entity<TableComment>().Property(c => c.CommentSeq).ValueGeneratedOnAdd();

            //Set one of CompositeKey to ForeignKey
            modelBuilder.Entity<TableComment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostNumber)
                .HasPrincipalKey(p => p.PostNumber);

            /*********************************************SALARY*********************************************/
            /*
             * Set One-to-One Relationship
             * Cuz The child/dependent side could not be determined for the one-to-one relationship
             * that was detected between '<entity1.property2>' and '<entity2.property1>'.
             */
            modelBuilder.Entity<TableSalary>()
                .HasOne(s => s.Job)
                .WithOne(j => j.Salary)
                .HasForeignKey<TableSalary>(s => s.JobCode)
                .HasPrincipalKey<TableDataJob>(j => j.JobCode);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DataDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        public DataContext()
        {
        }
    }
}
