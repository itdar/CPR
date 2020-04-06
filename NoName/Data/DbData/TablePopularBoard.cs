using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    public class TablePopularBoard : BoardModel
    {
        // TablePopularBoard To TablePopularPost => 1:n Relationship
        public ICollection<TablePopularPost> PopularPosts { get; set; }
    }
}
