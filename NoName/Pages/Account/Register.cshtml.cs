﻿using System;
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
using NoName.Data.DbUser;

namespace NoName.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
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

        public string DoneMockData { get; set; }

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
            System.Diagnostics.Debug.WriteLine("OnGetMakeMockUser()");

            returnUrl = returnUrl ?? Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                for (var i = 0; i < 100; i++)
                {
                    System.Diagnostics.Debug.WriteLine(i);

                    var myJobCodes = new List<TableUserJob>();
                    var jobCode = new TableUserJob
                    {
                        JobCode = i
                    };
                    myJobCodes.Add(jobCode);

                    var user = new ApplicationUser
                    {
                        UserName = "noname" + i + "@noname.com",
                        Email = "noname" + i + "@noname.com",
                        DateOfBirth = DateTime.Now,
                        Gender = 1,
                        ReceiveSMS = true,
                        ManagerNumber = -1,
                        PermissionLevel = 0,
                        VisitCount = 1,
                        EmailConfirmed = true,
                        MyJobCodes = myJobCodes,
                        Manager = new TableManager
                        {
                            AppointedDate = DateTime.Now
                        }
                    };

                    var result = await _userManager.CreateAsync(user, "Noname1234!@");

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
            System.Diagnostics.Debug.WriteLine("OnGetAsync()");

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            System.Diagnostics.Debug.WriteLine("OnPostAsync()");

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var myJobCodes = new List<TableUserJob>();
                var jobCode = new TableUserJob();
                //{
                //    JobCode = -1
                //};
                //myJobCodes.Add(jobCode);

                var user = new ApplicationUser {
                    UserName = Input.Email,
                    Email = Input.Email,
                    DateOfBirth = Input.BirthDate,
                    Gender = Input.Gender,
                    ReceiveSMS = Input.ReceiveSMS,
                    ManagerNumber = -1,
                    PermissionLevel = -1,
                    VisitCount = 0,
                    EmailConfirmed = true,
                    MyJobCodes = myJobCodes,
                    Manager = new TableManager
                    {
                        AppointedDate = DateTime.Now
                    }
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
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
