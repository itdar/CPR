﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    /*
     * Copy PostNumber of TablePost
     * 모든 게시판에서의 선별게시물이라면 DataJob 아래로 들어가야함.
     * 현재는 각 게시판에서의 선별게시물로 db를 짜놓음.
     */
    public class TablePopularPost : PostModel
    {
        [Key]
        public int PostSeq { get; set; }

        // TableBoard To TableHotPost => 1:n Relationship
        public int BoardId { get; set; }
        public TablePopularBoard PopularBoard { get; set; }

        // Information of Post
        public DateTime SelectionTime { get; set; }
    }
}
