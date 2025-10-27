using Microsoft.AspNetCore.Mvc;
using WorkArea.App.WebUI.Authorize;
using WorkArea.App.WebUI.Helpers;

namespace WorkArea.App.WebUI.Controllers;

[Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}