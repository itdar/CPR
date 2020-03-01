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
        private TableSalary salary;

        public TableJobPage JobPage { get; set; }

        [Key]
        public int JobCode { get; set; }

        public string JobName { get; set; }

        public TableSalary Salary
        {
            get => salary;
            set => salary = value;
        }
    }
}
