using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    /*
     * Copy PostNumber of TablePost
     */
    public class TableHotPost
    {
        [Key]
        public int HotPostSeq { get; set; }

        // TableBoard To TableHotPost => 1:n Relationship
        public int BoardId { get; set; }
        public TableBoard Board { get; set; }

        public int PostNumber { get; set; }
    }
}
