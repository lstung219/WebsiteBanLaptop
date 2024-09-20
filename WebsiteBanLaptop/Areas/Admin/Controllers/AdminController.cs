using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteBanLaptop.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult _PartialFooter()
        {
            return PartialView();
        }
    }
}