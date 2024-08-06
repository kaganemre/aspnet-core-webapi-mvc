using GuitarShopApp.Application.Interfaces.Services;
using GuitarShopApp.Application.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShopApp.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IEmailService _emailService;

    public AccountController(UserManager<IdentityUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            SignInManager<IdentityUser> signInManager,
                            IEmailService emailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _signInManager.AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                await _signInManager.SignOutAsync();

                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "Verify your account.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    await _userManager.SetLockoutEndDateAsync(user, null);

                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                    var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                    ModelState.AddModelError("", $"Your account has been locked. Try after {timeLeft.Minutes} minutes.");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong password.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect email.");
            }
        }

        return View(model);
    }


    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new { user.Id, token });

                await _emailService.SendEmailAsync(user.Email, "Account Verification", $"<a href='http://localhost:5164{url}'>Please click on the link to confirm your email account</a>");


                TempData["message"] = "Click on the confirmation email sent to your account.";
                return RedirectToAction("Login", "Account");
            }

            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }

        return View(model);
    }

    public async Task<IActionResult> ConfirmEmail(string Id, string token)
    {
        if (Id == null || token == null)
        {
            TempData["message"] = "Invalid verification key.";
            return View();
        }

        var user = await _userManager.FindByIdAsync(Id);

        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                TempData["message"] = "Your account has been confirmed.";
                return RedirectToAction("Login", "Account");
            }
        }

        TempData["message"] = "User not found.";
        return View();
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
    public IActionResult AccessDenied()
    {
        return View();
    }
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string Email)
    {
        if (string.IsNullOrEmpty(Email))
        {
            TempData["message"] = "Please enter your email address.";
            return View();
        }

        var user = await _userManager.FindByEmailAsync(Email);

        if (user == null)
        {
            TempData["message"] = "Your email address is not registered in our system";
            return View();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var url = Url.Action("ResetPassword", "Account", new { user.Id, token });

        await _emailService.SendEmailAsync(Email, "Reset Password", $"<a href='http://localhost:5164{url}'>Click on the link to reset your password</a>");

        TempData["message"] = "You can reset your password with the link sent to your e-mail address";

        return View();
    }

    public IActionResult ResetPassword(string Id, string token)
    {
        if (Id == null || token == null)
        {
            return RedirectToAction("Login");
        }

        var model = new ResetPasswordModel { Token = token };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["message"] = "There are no users matching this email address.";
                return RedirectToAction("Login");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
            {
                TempData["message"] = "Your password has been changed.";
                return RedirectToAction("Login");
            }

            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }
        return View(model);
    }

}