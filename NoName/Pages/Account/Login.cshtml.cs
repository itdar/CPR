using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NoName.Data;
using NoName.BackendClass.Account;

namespace NoName.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "이메일")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "비밀번호")]
            public string Password { get; set; }

            [Display(Name = "아이디 저장")]
            public bool RememberMe { get; set; }
        }

        public void OnGetFillMockLogin()
        {
            if (Input == null)
            {
                Input = new InputModel();
            }
            Input.Email = "noname0@noname.com";

            Input.RememberMe = true;
        }
       

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                                
                    // Main 이 되는 index page cs 에서,
            // 페이지 로딩될 때, 최초 로딩 또는 다른데에서 redirection 등
            // OnGet / OnPost 선택 호출하는 방법 확인해서 로그인 동작 시에만 호출 되는 것 만들어서 옮겨야함
            //var user = await _userManager.GetUserAsync(User);
            //if (user != null)
            //{
            //    var userId = await _userManager.GetUserIdAsync(user);
            //    var userName = await _userManager.GetUserNameAsync(user);
            //    var email = await _userManager.GetEmailAsync(user);
            //    var jobCodesQuery = UserDbManager.GetInstance().GetUserJobCodes(userId);
            //    var jobCodes = new List<int>();

            //    System.Diagnostics.Debug.WriteLine(jobCodesQuery.Count());

            //    foreach (var jobCode in jobCodesQuery)
            //    {
            //        System.Console.WriteLine("JobCode : " + jobCode.JobCode);
            //    }

            //    var userInfo = UserInformation.GetInstance();
            //    userInfo.SetInformation(userId, userName, email, jobCodes);

            //    System.Diagnostics.Debug.WriteLine("UserId : " + userInfo.UserId);
            //    System.Diagnostics.Debug.WriteLine("UserName : " + userInfo.UserName);
            //    System.Diagnostics.Debug.WriteLine("Email : " + userInfo.Email);
            //    for (int i = 0; i < userInfo.JobCodes.Count(); ++i)
            //    {
            //        System.Diagnostics.Debug.WriteLine("JobCode : {i}", userInfo.JobCodes.ElementAt(i));
            //    }
            //    System.Diagnostics.Debug.WriteLine(userInfo.JobCodes.ToString());
            //}
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine("user is null (It means 로그인 안되어있음)");
            //}

                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
