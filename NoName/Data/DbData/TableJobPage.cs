using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    /*
     * 직업 페이지
     */
    public class TableJobPage
    {
        private ICollection<TableBoard> boards;
        public ICollection<TableBoard> Boards
        {
            get => boards;
            set => boards = value;
        }

        [Key]
        public int JobPageId { get; set; }
    }
}
