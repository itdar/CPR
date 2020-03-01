using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.Data.DbUser
{
    public class TableUserJob
    {
        [Key]
        public int JobCode { get; set; }
        /*
         * ApplicationUser 와 FK 연결, 하지만 이건 1:1 이고, 
         * 1:다수 (ApplicationUser 에서 IEnumeration<MyJob> 써야함)
         */
        //public ApplicationUser ApplicationUser { get; set; }

    }
}
