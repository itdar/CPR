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
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public string UserId { get; set; }
        public string Email { get; set; }
        public List<int> JobCodes { get; set; }

        private UserInformation(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        private UserInformation()
        {
            
        }

        public static UserInformation GetInstance()
        {
            if (instance == null)
                instance = new UserInformation();
            return instance;
        }

        public void SetInformation(string userId, string email, List<int> jobCodes)
        {
            UserId = userId;
            Email = email;
            JobCodes = jobCodes;
        }
    }
}
