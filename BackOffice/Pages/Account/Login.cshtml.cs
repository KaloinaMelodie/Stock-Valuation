using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BackOffice.ViewModels;
using BackOffice.Models;

namespace BackOffice.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public LoginModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
            ErrorMessage = null;
            ViewData["Layout"] = "Login";
            LoginViewModel = new LoginViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["Layout"] = "Login";
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(LoginViewModel.EmailAddress);
            if (user == null)
            {
                ErrorMessage = "Email address not found. Please try again.";
                return Page();
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, LoginViewModel.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, LoginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToPage("/Produit/Index"); // Replace with actual Razor page
                }
            }

            ErrorMessage = "Password for the email is wrong. Please try again.";
            return Page();
        }
        
    }
}
