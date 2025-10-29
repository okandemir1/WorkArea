using Microsoft.AspNetCore.Mvc;

namespace WorkArea.App.WebUI.Views.ArchiveType.Components.ArchiveTypeForm
{
    public class ArchiveTypeFormViewComponent() : ViewComponent
    {
        public IViewComponentResult Invoke(string action, Domain.Entities.ArchiveType model)
        {
            ViewBag.Action = action;
            return View(model);
        }
    }
}
