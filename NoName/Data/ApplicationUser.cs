using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NoName.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string userID { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
        [PersonalData]
        public string Gender { get; set; }
        [PersonalData]
        public string ReciveSMS { get; set; }
    }
}
