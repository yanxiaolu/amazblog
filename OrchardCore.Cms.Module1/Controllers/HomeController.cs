using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrchardCore.Cms.Module1.Controllers;

public sealed class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}