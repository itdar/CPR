using Microsoft.AspNetCore.Identity;
using NoName.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoName.BackendClass.Login
{
    /*
     * Logged in user information
     */
    public class UserInformation
    {
        private static UserInformation instance;

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<int> JobCodes { get; set; }

        private UserInformation()
        {
            
        }

        public static UserInformation GetInstance()
        {
            if (instance == null)
                instance = new UserInformation();
            return instance;
        }

        public void SetInformation(string userId, string userName, string email, List<int> jobCodes)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            JobCodes = jobCodes;
        }

        public void ReleaseInformation()
        {
if (UserId == null)
                UserId = null;
            if (UserName == null)
                UserName = null;
            if (Email == null)
                Email = null;
            if (JobCodes != null)
            {
                JobCodes.Clear();
                JobCodes = null;
            }
        }
    }
}
