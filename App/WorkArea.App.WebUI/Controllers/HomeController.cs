using Microsoft.AspNetCore.Mvc;

namespace WorkArea.App.WebUI.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}