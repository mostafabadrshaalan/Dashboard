using AutoMapper;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using PL.Helpers;
using PL.Models.Account;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Sign Up
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = registerViewModel.Email,
                    IsAgree = registerViewModel.IsAgree,
                    UserName = registerViewModel.Email.Split('@')[0]
                };

                var result = await userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                    return RedirectToAction("SignIn");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(registerViewModel);
        }
        #endregion

        #region Sign In
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginViewModel.Email);
                if (user is not null)
                {
                    var passwordValid = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (passwordValid)
                    {
                        var result = await signInManager.PasswordSignInAsync(user,
                                                                             loginViewModel.Password,
                                                                             loginViewModel.RememberMe,
                                                                             false);
                        if (result.Succeeded)
                            return RedirectToAction("index", "Home");
                        else
                            ModelState.AddModelError(string.Empty, "Sign-in failed. Please try again.");
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Invalid password. Please try again.");
                }
                else
                    ModelState.AddModelError(string.Empty, "Invalid email. Please try again.");
            }
            return View(loginViewModel);
        }


        #endregion

        #region SignOut
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
        #endregion

        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (user is not null)
                {
                    var token=await userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordLink= Url.Action("ResetPassword","Account",new { Email=forgetPasswordViewModel.Email,Token=token},Request.Scheme);
                    var email = new Email()
                    {
                        Title="Reset Password",
                        Body = resetPasswordLink,
                        To=forgetPasswordViewModel.Email
                    };

                    EmailSettings.Send(email);
                    return RedirectToAction("CompleteForgetPassword");
                }
                else
                 ModelState.AddModelError("", "Invalid Email");
            }
            return View(forgetPasswordViewModel);
        }
        public IActionResult CompleteForgetPassword()
        {
            return View();
        }
        #endregion

        #region ResetPassword
        public IActionResult ResetPassword(string email, string token)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user is not null)
                {
                    var result = await userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token, resetPasswordViewModel.Password);

                    if (result.Succeeded)
                        return RedirectToAction("ResetPasswordDone");
                    else
                        foreach (var error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                }
                else
                    ModelState.AddModelError("", "Invalid Email");
            }

            return View(resetPasswordViewModel);
        }
        public IActionResult ResetPasswordDone()
        {
            return View();
        }
        #endregion
    }
}
