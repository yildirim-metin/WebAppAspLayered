using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppAspLayered.BLL.Services;
using WebAppAspLayered.DL.Entities;
using WebAppAspLayered.Mappers;
using WebAppAspLayered.Models.Users;

namespace WebAppAspLayered.Controllers;

public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Register()
    {
        return View(new RegisterFormDto());
    }

    [HttpPost]
    public IActionResult Register([FromForm] RegisterFormDto form)
    {
        if (!ModelState.IsValid)
        {
            form.Password = "";
            form.ConfirmPassword = "";
            return View(form);
        }

        try
        {
            _userService.Register(form.ToUser());
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Error", ex.Message);
            form.Password = "";
            return View(form);
        }

        return RedirectToAction("Login", "User");
    }

    public IActionResult Login()
    {
        return View(new LoginFormDto());
    }

    [HttpPost]
    public IActionResult Login([FromForm] LoginFormDto form)
    {
        if (!ModelState.IsValid)
        {
            form.Password = "";
            return View(form);
        }

        try
        {
            User? user = _userService.Login(form.Email, form.Password);

            ClaimsPrincipal claims = new(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            ], CookieAuthenticationDefaults.AuthenticationScheme));

            HttpContext.SignInAsync(claims);
            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims, new AuthenticationProperties
            //{
            //    IsPersistent = false
            //});
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Error", ex.Message);
            form.Password = "";
            return View(form);
        }

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        return RedirectToAction("Login", "User");
    }
}
