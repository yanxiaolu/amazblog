using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrchardCore.Cms.BookingModule.Controllers;

public sealed class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}