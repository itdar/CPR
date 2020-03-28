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
        [Key]
        public int Number { get; set; }
        [Key]
        public int JobCode { get; set; }
        public string JobName { get; set; }


        // TableDataJob to TableBoard => 1:n Relationship
        private ICollection<TableBoard> boards;
        public ICollection<TableBoard> Boards
        {
            get => boards;
            set => boards = value;
        }

        // TableDataJob to TableSalary => 1:1 Relationship
        public virtual TableSalary Salary { get; set; }
    }
}
