using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbData
{
    public class TableBoardCode
    {
        [Key]
        public int BoardCodeNumber { get; set; }

        public string BoardKindName { get; set; }
    }
}
