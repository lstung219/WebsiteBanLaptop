using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanLaptop.Models;
using WebsiteBanLaptop.Models.ViewModel;

namespace WebsiteBanLaptop.Areas.Admin.Controllers
{
    public class QuanLyController : Controller
    {
        // GET: Admin/Admin
        db_LAPTOPSTORESEntities db = new db_LAPTOPSTORESEntities();
        public ActionResult Index()
        {
            var userCount = db.KHACHHANGs.Count();
            int orderCount = db.DONHANGs.Count();
            int feedbackCount = db.FEEDBACKs.Count();
            double totalIncome = db.DONHANGs.Sum(o => o.GIA ?? 0);
            var recentOrders = db.DONHANGs
                                 .OrderByDescending(o => o.NGAYDAT)
                                 .Take(8)
                                 .ToList();
            var customers = db.KHACHHANGs
                              .OrderByDescending(c => c.NGAYDK)
                              .Take(8)
                              .ToList();

            ViewBag.UserCount = userCount;
            ViewBag.OrderCount = orderCount;
            ViewBag.FeedbackCount = feedbackCount;
            ViewBag.TotalIncome = totalIncome;

            // Create the ViewModel and assign the fetched data
            var model = new DashboardViewModel
            {
                RecentOrders = recentOrders,
                RecentCustomers = customers
            };

            // Pass the ViewModel to the view
            return View(model);
        }


        public ActionResult _PartialNav()
        {


            return PartialView();
        }
        public ActionResult _PartialTopbar()
        {
            return PartialView();
        }
        public ActionResult Product()
        {
            var products = db.SANPHAMs.ToList();  // Assuming you're fetching products from a database
            return View(products);
        }
    }
}