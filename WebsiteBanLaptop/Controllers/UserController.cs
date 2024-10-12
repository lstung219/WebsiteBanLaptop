using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebsiteBanLaptop.Models;

namespace WebsiteBanLaptop.Controllers
{
    public class UserController : Controller

    {
        private readonly string MAIL_USER = "lstung2004@gmail.com";
        private readonly string MAIL_PASSWORD = "nphq zrjn hagf pbtc";
        db_LAPTOPSTORESEntities db = new db_LAPTOPSTORESEntities();
        public ActionResult DangNhap()
        {
            // Check if there's a success message from previous actions
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.DKTC = TempData["SuccessMessage"];
            }

            return View();
        }

        // POST: User Login
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            // Retrieve username and password from the form collection
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];

            // Validate input fields
            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập"; // Username not provided
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu"; // Password not provided
            }
            else
            {
                // Fetch the customer (KHACHHANG) record matching the username and password
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.USERNAME == sTenDN && n.PASS == sMatKhau);

                if (kh != null)
                {
                    // If login is successful
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công"; // Login success message
                    Session["TaiKhoan"] = kh; // Store user in session
                    Session["TenKH"] = kh.HOTEN; // Store customer name in session
                    TempData["SuccessMessage"] = "Chúc mừng đăng nhập thành công"; // Set success message in TempData
                    return RedirectToAction("Index", "Laptop"); // Redirect to Laptop index after successful login
                }
                else
                {
                    // Login failed, incorrect username or password
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng"; // Error message
                }
            }

            return View(); // If login fails, return to the login view
        }
        public ActionResult Logout()
        {
            Session["TaiKhoan"] = null;
            Session["TenKH"] = null;
            return RedirectToAction("Index", "Laptop");
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var sHoTen = collection["HOTEN"];
            var sTenDN = collection["USERNAME"];
            var sMatKhau = collection["PASS"];
            var sMatKhauNhapLai = collection["MatKhauNL"];
            var sDiaChi = collection["DIACHI"];
            var sEmail = collection["GMAIL"];
            var sDienThoai = collection["SDT"];
            var dNgaySinh = String.Format("{0:dd/MM/yyyy}", collection["NGAYSINH"]);

            // Kiểm tra mật khẩu và mật khẩu nhập lại trước
            if (String.IsNullOrEmpty(sMatKhauNhapLai))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (!sMatKhau.Equals(sMatKhauNhapLai)) // Sử dụng Equals để so sánh chuỗi
            {
                ViewData["err4"] = "Mật khẩu nhập lại không khớp";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.USERNAME == sTenDN) != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập đã tồn tại";
            }
            else if (db.KHACHHANGs.SingleOrDefault(n => n.GMAIL == sEmail) != null)
            {
                ViewBag.ThongBao = "Email đã được sử dụng";
            }
            else if (ModelState.IsValid)
            {
                // Gán giá trị cho đối tượng được tạo mới (kh)
                kh.HOTEN = sHoTen;
                kh.USERNAME = sTenDN;
                kh.PASS = sMatKhau;
                kh.GMAIL = sEmail;
                kh.DIACHI = sDiaChi;
                kh.SDT = sDienThoai;
                kh.NGAYSINH = DateTime.Parse(dNgaySinh);
                kh.NGAYDK = DateTime.Now;

                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                sendMail(
                    kh.GMAIL,
                    kh.HOTEN,
                    kh.USERNAME,
                    kh.SDT,
                   "Tạo tài khoản thành công"
                );
                TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập!";
                return RedirectToAction("DangNhap");
            }

            return this.DangKy(); // Trả về lại trang đăng ký với thông báo lỗi
        }


        private void sendMail(string email, string tenTaiKhoan, string tenDangNhap, string soDienThoai, string subject)
        {

            string filePath = HostingEnvironment.MapPath("~/FormMail/DangKi.html");

            string mailTemplate = System.IO.File.ReadAllText(filePath);


            SmtpClient smtp = new SmtpClient()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(MAIL_USER, MAIL_PASSWORD),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };


            mailTemplate = mailTemplate.Replace("*|USER_EMAIL|*", email);
            mailTemplate = mailTemplate.Replace("*|USER_PHONE|*", soDienThoai);
            mailTemplate = mailTemplate.Replace("*|USER_ACCOUNT|*", tenDangNhap);
            mailTemplate = mailTemplate.Replace("*|USER_NAME|*", tenTaiKhoan);


            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailTemplate, Encoding.UTF8, MediaTypeNames.Text.Html);


            MailMessage message = new MailMessage();
            message.From = new MailAddress(MAIL_USER);
            message.ReplyToList.Add(MAIL_USER);
            message.To.Add(new MailAddress(email));
            message.Subject = subject;


            message.AlternateViews.Add(htmlView);
            message.IsBodyHtml = true;

            smtp.Send(message);
        }
    }
}