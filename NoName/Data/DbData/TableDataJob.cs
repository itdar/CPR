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

        [Key]
        public int JobCode { get; set; }

        private TableSalary salary;

        public string JobName { get; set; }

        public TableSalary Salary
        {
            get => salary;
            set => salary = value;
        }
    }
}
