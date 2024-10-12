using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanLaptop.Models;
using static WebsiteBanLaptop.Models.GIOHANG;

namespace WebsiteBanLaptop.Controllers
{
    public class GioHangController : Controller
    {
        db_LAPTOPSTORESEntities db = new db_LAPTOPSTORESEntities();
        // GET: GioHang
        public ActionResult GioHang()
        {
            List<GioHang> gioHangs = LayGioHang();

            //if (gioHangs.Count == 0)
            //{
            //    return RedirectToAction("GioHang", "GioHang");
            //}

            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(gioHangs);
        }

        private List<GioHang> LayGioHang()
        {
            List<GioHang> lstgioHangs = Session["GioHang"] as List<GioHang>;

            if (lstgioHangs == null)
            {
                lstgioHangs = new List<GioHang>();
                Session["GioHang"] = lstgioHangs;
            }

            return lstgioHangs;
        }

        public ActionResult ThemGioHang(int id, string redirectUrl)
        {
            List<GioHang> gioHangs = LayGioHang();
            GioHang gioHang = gioHangs.Find(g => g.MASP == id);

            if (gioHang == null)
            {
                gioHang = new GioHang(id);
                gioHangs.Add(gioHang);
            }
            else
            {
                gioHang.iSoLuong++;
            }

            return Redirect(redirectUrl);
        }

        private int TongSoLuong()
        {
            List<GioHang> gioHangs = Session["GioHang"] as List<GioHang>;

            return gioHangs != null ? gioHangs.Sum(g => g.iSoLuong) : 0;
        }

        private double TongTien()
        {
            List<GioHang> gioHangs = Session["GioHang"] as List<GioHang>;

            return gioHangs != null ? gioHangs.Sum(g => g.dThanhTien) : 0;
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return PartialView();
        }

        public ActionResult XoaSanPham(int masp)
        {
            List<GioHang> gioHangs = LayGioHang();
            GioHang gioHang = gioHangs.SingleOrDefault(g => g.MASP == masp);

            if (gioHang != null)
            {
                gioHangs.Remove(gioHang);

                //if (gioHangs.Count == 0)
                //{
                //    return RedirectToAction("Index", "Laptop");
                //}
            }

            return RedirectToAction("GioHang", "GioHang");
        }

        public ActionResult XoaGioHang()
        {
            List<GioHang> gioHangs = LayGioHang();
            gioHangs.Clear();

            return RedirectToAction("GioHang", "GioHang");
        }

        public ActionResult CapNhatGioHang(int masp, FormCollection f)
        {
            List<GioHang> gioHangs = LayGioHang();
            GioHang gioHang = gioHangs.SingleOrDefault(g => g.MASP == masp);

            if (gioHang != null)
            {
                gioHang.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }

            return RedirectToAction("GioHang", "GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "User");
            }

            if (Session["GioHang"] == null)
            {
                return RedirectToAction("GioHang", "GioHang");
            }

            List<GioHang> gioHangs = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(gioHangs);
        }

        //[HttpPost]
        //public ActionResult DatHang(FormCollection f)
        //{
        //    DONDATHANG ddh = new DONDATHANG();
        //    KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
        //    List<GioHang> lstCart = LayGioHang();
        //    ddh.MaKH = kh.MaKH;
        //    ddh.NgayDat = DateTime.Now;

        //    var NgayGiao = String.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
        //    ddh.NgayGiao = DateTime.Parse(NgayGiao);
        //    ddh.TinhTrangGiaoHang = 1;
        //    ddh.DaThanhToan = false;

        //    db.DONDATHANGs.Add(ddh);
        //    db.SaveChanges();

        //    foreach (var item in lstCart)
        //    {
        //        CHITIETDATHANG ctdh = new CHITIETDATHANG();
        //        ctdh.MaDonHang = ddh.MaDonHang;
        //        ctdh.MASP = item.MASP;
        //        ctdh.SoLuong = item.iSoLuong;
        //        ctdh.DonGia = (decimal)item.GIA;

        //        db.CHITIETDATHANGs.Add(ctdh);
        //    }

        //    db.SaveChanges();
        //    Session["GioHang"] = null;
        //    return RedirectToAction("XacNhanDonHang", "GioHang");
        //}
    }
}