using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    public class TableMyPost : PostModel
    {
        [Key]
        public int PostSeq { get; set; }

        // TableBoard To TableHotPost => 1:n Relationship
        public int BoardId { get; set; }
        public TableMyBoard MyBoard { get; set; }

        // Information of Post
        public DateTime SelectionTime { get; set; }
    }
}
