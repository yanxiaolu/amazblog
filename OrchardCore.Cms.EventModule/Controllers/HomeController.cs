using Microsoft.AspNetCore.Mvc;

namespace OrchardCore.Cms.EventModule.Controllers;

public sealed class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
