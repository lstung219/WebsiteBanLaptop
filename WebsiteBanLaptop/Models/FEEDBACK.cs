//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteBanLaptop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FEEDBACK
    {
        public int MAFB { get; set; }
        public Nullable<System.DateTime> NGAYFB { get; set; }
        public string NOIDUNG { get; set; }
        public Nullable<int> RATING { get; set; }
        public Nullable<int> MAKH { get; set; }
        public Nullable<int> MAQL { get; set; }
        public Nullable<int> MASP { get; set; }
    
        public virtual KHACHHANG KHACHHANG { get; set; }
        public virtual QUANLY QUANLY { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
