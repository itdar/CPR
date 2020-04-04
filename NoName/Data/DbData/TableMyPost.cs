using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    public class TableMyPost
    {
        [Key]
        public int PostSeq { get; set; }

        // TableBoard To TableHotPost => 1:n Relationship
        public int BoardId { get; set; }
        public TableMyBoard MyBoard { get; set; }

        // Information of Post
        public int PostNumber { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public DateTime SelectionTime { get; set; }
    }
}
