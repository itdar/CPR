using NoName.Data.DbUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    /*
     * 직업
     */
    public class TableDataJob
    {
        private ICollection<TableBoard> boards;
        public ICollection<TableBoard> Boards
        {
            get => boards;
            set => boards = value;
        }
        public ICollection<TableUserJob> UserJob
        {
            get => UserJob;
            set => UserJob = value;
        }

        //PrimaryKey
        public int Number { get; set; }
        //PrimaryKey
        public int JobCode { get; set; }
        public string JobName { get; set; }


        public TableSalary Salary { get; set; }
    }
}
