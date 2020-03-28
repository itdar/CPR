using Microsoft.AspNetCore.Identity;
using NoName.Data.DbData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbUser
{
    public class TableManager
    {
        [Key]
        //ForeignKey
        public int BaordId { get; set; }
        //ForeignKey
        public string Id { get; set; }
        public DateTime AppointedDate { get; set; }


        [ForeignKey("BoardId")]
        public TableBoard TableBoard { get; set; }
        [ForeignKey("Id")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
