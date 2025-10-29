using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WorkArea.App.WebUI.Helpers;
using WorkArea.Application.RequestModels;
using WorkArea.Application.Services;
using WorkArea.Application.Validation;

namespace WorkArea.App.WebUI.Controllers;

public class AuthController(UserService userService, SessionHelper sessionHelper) : Controller
{
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        var errors = new List<string>();
        var validCheck = new LoginValidation().Validate(model);
        if (!validCheck.IsValid)
        {
            validCheck.Errors.ForEach(x => errors.Add(x.ErrorMessage));
            return Json(new { isSucceed = false, message = "Eksik veya hatalı veri girişi", title = "Hay Aksi", errors, redirect = "" });
        }

        var isLogin = await userService.WebLogin(model);
        if (isLogin.IsSucceed)
        {
            var userSimple = await userService.GetUserSimpleInfo(isLogin.Instance.Id);
            sessionHelper.Set("UserInfo", userSimple);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, isLogin.Instance.Fullname),
                new Claim(ClaimTypes.NameIdentifier, isLogin.Instance.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.Now.AddHours(30),
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            
            return Json(new { isSucceed = true, message = "Giriş yapıldı", title = "Başarılı", errors, redirect = "/Home/Index" });
        }
        
        return Json(new { isSucceed = false, message = "Eksik veya hatalı veri girişi", title = "Hay Aksi", errors, redirect = "" });
    }
    
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestModel model)
    {
        var errors = new List<string>();
        var validCheck = new RegisterValidation().Validate(model);
        if (!validCheck.IsValid)
        {
            validCheck.Errors.ForEach(x => errors.Add(x.ErrorMessage));
            return Json(new { isSucceed = false, message = "Eksik veya hatalı veri girişi", title = "Hay Aksi", errors, redirect = "" });
        }

        var isLogin = await userService.WebRegister(model);
        if (isLogin.IsSucceed)
        {
            var userSimple = await userService.GetUserSimpleInfo(isLogin.Instance.Id);
            sessionHelper.Set("UserInfo", userSimple);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, isLogin.Instance.Fullname),
                new Claim(ClaimTypes.NameIdentifier, isLogin.Instance.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.Now.AddHours(30),
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            
            return Json(new { isSucceed = true, message = "Üyelik işlemi başarılı", title = "Başarılı", errors, redirect = "/Home/Index" });
        }
        
        return Json(new { isSucceed = false, message = "Eksik veya hatalı veri girişi", title = "Hay Aksi", errors, redirect = "" });
    }
    
    public IActionResult Logout()
    {
        if (User.Identity.IsAuthenticated)
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            sessionHelper.Set("UserInfo", null);
        }
        
        return RedirectToAction("Login", "Auth");
    }
}