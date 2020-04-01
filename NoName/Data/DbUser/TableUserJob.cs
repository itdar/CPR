using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NoName.Data.DbData;

namespace NoName.Data.DbUser
{
    public class TableUserJob
    {
        [Key]
        public int UserJobSeq { get; set; }

        // ApplicationUser to TableUserJob => 1:n Relationship
        [ForeignKey("Id")]
        public ApplicationUser ApplicationUser { get; set; }

        public int JobCode { get; set; }
        public int Salary { get; set; }
    }
}
