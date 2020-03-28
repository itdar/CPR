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
        //ForeignKey
        [Key]
        public string Id { get; set; }
        //ForeignKey
        public int JobCode { get; set; }    
        public int Salary { get; set; }

        /*
         * ApplicationUser 와 FK 연결, 하지만 이건 1:1 이고, 
         * 1:다수 (ApplicationUser 에서 IEnumeration<MyJob> 써야함)
         */
        //public ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("Id")]
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("JobCode")]
        public TableDataJob Job { get; set; }
    }
}
