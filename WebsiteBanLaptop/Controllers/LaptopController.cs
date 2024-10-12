using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanLaptop.Models;

namespace WebsiteBanLaptop.Controllers
{
    public class LaptopController : Controller
    {
        // GET: Laptop
        db_LAPTOPSTORESEntities db = new db_LAPTOPSTORESEntities();
        private List<SANPHAM> LayBanNhieu(int count)
        {
            return db.SANPHAMs.OrderByDescending(a => a.SLBAN).Take(count).ToList();
        }
        private List<SANPHAM> LaySPMoi(int count)
        {
            return db.SANPHAMs.OrderByDescending(a => a.THOIGIAN).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var listSachBanNhieu = LayBanNhieu(8);
            return View(listSachBanNhieu);
        }
        public ActionResult PartialSPMoi()
        {
            var listSPMoi = LaySPMoi(8);
            return PartialView(listSPMoi);
        }

        public ActionResult Product(int? page)
        {
            int pageSize = 8; // Số sản phẩm trên mỗi trang
            int pageNumber = page ?? 1; // Nếu không có tham số 'page', thì mặc định là trang 1

            // Lấy tổng số sản phẩm
            var totalProducts = db.SANPHAMs.Count();

            // Lấy danh sách sản phẩm cho trang hiện tại
            var sanPhams = db.SANPHAMs
                            //.Include(sp => sp.THUONGHIEU)
                            .OrderBy(sp => sp.TENSP)
                            .Skip((pageNumber - 1) * pageSize) // Bỏ qua các sản phẩm của trang trước
                            .Take(pageSize) // Lấy số sản phẩm của trang hiện tại
                            .ToList();

            // Tính tổng số trang
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(sanPhams);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult _PartialHeader()
        {
            return PartialView();
        }
        public ActionResult _PartialFooter() { 
            return PartialView();
        }
        public ActionResult Login() { 
            return View();
        }
        public ActionResult Register() { 
            return View();
        }
        public ActionResult ChiTietSP(int id)
        {
            var sp = from s in db.SANPHAMs where s.MASP == id select s;
            return View(sp.Single());
        }

    }
}