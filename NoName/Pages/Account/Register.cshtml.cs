using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using NoName.Data;

namespace NoName.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        int callCountOnGet = 0;
        int callCountOnPost = 0;
        int callCount = 0;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
            
        /*
         * Email / Password / PasswordConfirm / BirthDate / Gender / ReceiveSMS / Job
         */
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "이메일")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "비밀번호")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "비밀번호 재입력")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.DateTime)]
            [Display(Name = "생년월일")]
            public DateTime BirthDate { get; set; }

            [Display(Name = "성별")]
            public int Gender { get; set; }

            [Display(Name = "직업")]
            public int Job { get; set; }

            [Display(Name = "SMS 수신여부")]
            public bool ReceiveSMS { get; set; }
        }

        public async Task<IActionResult> OnGetMakeMockUser(string returnUrl = null)
        {
            System.Diagnostics.Debug.WriteLine("OnPostAsync()" + ++callCountOnPost);

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                for (var i = 0; i < 100; i++)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "hyungsoo" + i.ToString(),
                        Email = "hyungsoo" + i.ToString() + "@a.com",
                        DateOfBirth = DateTime.Now,
                        Gender = 1,
                        ReceiveSMS = true,
                        ManagerNumber = -1,
                        PermissionLevel = 0,
                        VisitCount = 1,
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(user, "Q!w2e3");

                    if (result.Succeeded)
                    {
                        System.Diagnostics.Debug.WriteLine("222");

                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        System.Diagnostics.Debug.WriteLine("333");

                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        System.Diagnostics.Debug.WriteLine("444");

                        await _emailSender.SendEmailAsync("hyungsoo" + i.ToString() + "@a.com", "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        System.Diagnostics.Debug.WriteLine("555");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            System.Diagnostics.Debug.WriteLine("666");

                            return RedirectToPage("RegisterConfirmation", new { email = "hyungsoo" + i.ToString() + "@a.com" });
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("777");

                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            System.Diagnostics.Debug.WriteLine("OnGetAsync()" + ++callCountOnGet);

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            System.Diagnostics.Debug.WriteLine("OnPostAsync()" + ++callCountOnPost);


            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = Input.Email,
                    Email = Input.Email,
                    DateOfBirth = Input.BirthDate,
                    Gender = Input.Gender,
                    ReceiveSMS = Input.ReceiveSMS,
                    ManagerNumber = -1,
                    PermissionLevel = 0,
                    VisitCount = 1,
                    EmailConfirmed = true
                };

                System.Diagnostics.Debug.WriteLine("111");

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    System.Diagnostics.Debug.WriteLine("222");

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    System.Diagnostics.Debug.WriteLine("333");

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    System.Diagnostics.Debug.WriteLine("444");

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    System.Diagnostics.Debug.WriteLine("555");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        System.Diagnostics.Debug.WriteLine("666");

                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("777");

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
