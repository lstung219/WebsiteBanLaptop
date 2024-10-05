using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanLaptop.Models.ViewModel
{
    public class DashboardViewModel
    {
        public IEnumerable<DONHANG> RecentOrders { get; set; }
        public IEnumerable<KHACHHANG> RecentCustomers { get; set; }
    }
}