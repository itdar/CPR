﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoName.Data;

namespace NoName.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "아이디")]
            public string userID { get; set; }
            [Required]
            [Phone]
            [Display(Name = "전화번호")]
            public string PhoneNumber { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "생년월일")]
            public DateTime DOB { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "성별")]
            public string Gender { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "SMS수신여부")]
            public string ReciveSMS { get; set; }

            //Need to Security considerations(20.02.19)
            //[Required]
            [DataType(DataType.Text)]
            [Display(Name = "직업인증")]
            public string Authentication { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                userID = user.userID,
                PhoneNumber = phoneNumber,
                DOB = user.DOB,
                Gender = user.Gender,
                ReciveSMS = user.ReciveSMS,
                Authentication = user.Authentication
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            if (Input.userID != user.userID)
            {
                user.userID = Input.userID;
            }

            if (Input.DOB != user.DOB)
            {
                user.DOB = Input.DOB;
            }

            if (Input.Gender != user.Gender)
            {
                user.Gender = Input.Gender;
            }

            if (Input.ReciveSMS != user.ReciveSMS)
            {
                user.ReciveSMS = Input.ReciveSMS;
            }

            if (Input.Authentication != user.Authentication)
            {
                user.Authentication = Input.Authentication;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}