using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    public class TableMyBoard : BoardModel
    {
        // TableMyBoard To TableMyPost => 1:n Relationship
        public ICollection<TableMyPost> MyPosts { get; set; }
    }
}
