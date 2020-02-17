using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NoName.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string ID { get; set; }
        public string CellPhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string ReciveSMS { get; set; }
        public string Authentication { get; set; }
    }
}
