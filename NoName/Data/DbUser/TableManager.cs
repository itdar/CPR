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
        public int ManagerSeq { get; set; }

        // ApplicationUser to TableManager => 1:1 Relationship
        [ForeignKey("Id")]
        public ApplicationUser ApplicationUser { get; set; }

        public int BaordId { get; set; }
        public DateTime AppointedDate { get; set; }
    }
}
