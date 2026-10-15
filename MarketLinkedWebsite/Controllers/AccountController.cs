using MarketLink.Admin.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace MarketLink.Admin.Controllers;

[AllowAnonymous]
public class AccountController(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Dashboard");

        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password, bool rememberMe = false, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(password))
        {
            ViewData["Error"] = "Enter your email and password.";
            return View();
        }

        var result = await signInManager.PasswordSignInAsync(email.Trim(), password, rememberMe, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            ViewData["Error"] = "Email or password is incorrect.";
            return View();
        }

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return LocalRedirect(returnUrl);

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string fullName, string email, string password, string confirmPassword)
    {
        string? error = null;

        if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            error = "Fill in all the fields.";
        else if (!Regex.IsMatch(email.Trim(), @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            error = "Enter a valid email address.";
        else if (password.Length < 8)
            error = "Password must be at least 8 characters.";
        else if (password != confirmPassword)
            error = "Passwords do not match.";

        if (error is not null)
        {
            ViewData["Error"] = error;
            return View();
        }

        var user = new ApplicationUser
        {
            UserName = email.Trim(),
            Email = email.Trim(),
            FullName = fullName.Trim()
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            ViewData["Error"] = result.Errors.Any(e => e.Code is "DuplicateUserName" or "DuplicateEmail")
                ? "An account with that email already exists."
                : string.Join(" ", result.Errors.Select(e => e.Description));
            return View();
        }

        TempData["Success"] = "Account created. Sign in to continue.";
        return RedirectToAction(nameof(Login));
    }

    [Authorize, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
}
