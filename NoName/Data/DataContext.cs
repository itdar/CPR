﻿using Microsoft.EntityFrameworkCore;
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
    public class DataContext : DbContext
    {
        public DbSet<TableDataJob> Job { get; set; }
        public DbSet<TableJobPage> JobPage { get; set; }
        public DbSet<TableBoard> Board { get; set; }
        public DbSet<TablePost> Post { get; set; }
        public DbSet<TableComment> Comment { get; set; }
        public DbSet<TableSalary> Salary { get; set; }
        public DbSet<TableMessage> Message { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext()
        {
        }
    }
}