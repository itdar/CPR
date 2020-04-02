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
using NoName.BackendClass.Login;

namespace NoName.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserContext _userContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginModel> logger,
            UserContext userContext)
        {
            _userContext = userContext;
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
                    //_userContext.Add
                    UserDbManager.GetInstance().GetAllUserJob();



                    //var user = await _userManager.GetUserAsync(HttpContext.User);
                    //Task<ApplicationUser> appUser = _userManager.FindByNameAsync(Input.Email);
                    //System.Diagnostics.Debug.WriteLine(appUser.Id);
                    //System.Diagnostics.Debug.WriteLine(appUser.);
                    //System.Diagnostics.Debug.WriteLine(appUser.Id);
                    //System.Diagnostics.Debug.WriteLine(appUser.Id);
                    //System.Diagnostics.Debug.WriteLine(appUser.Id);

                    //UserInformation.GetInstance().SetInformation("test", Input.Email, tempJobCodes);

                    //System.Diagnostics.Debug.WriteLine(user.Id);
                    //System.Diagnostics.Debug.WriteLine(user.Email);
                    //System.Diagnostics.Debug.WriteLine(user.ManagerNumber);
                    //System.Diagnostics.Debug.WriteLine(user.PermissionLevel);
                    //System.Diagnostics.Debug.WriteLine(user.PhoneNumber);
                    //System.Diagnostics.Debug.WriteLine(user.ReceiveSMS);
                    System.Diagnostics.Debug.WriteLine(UserInformation.GetInstance().UserId);
                    System.Diagnostics.Debug.WriteLine(UserInformation.GetInstance().Email);
                    //System.Diagnostics.Debug.WriteLine(UserInformation.GetInstance().JobCodes.ToString());

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
