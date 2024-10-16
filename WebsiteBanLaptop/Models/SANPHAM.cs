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
    
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.CAUHINHs = new HashSet<CAUHINH>();
            this.CTGIOHANGs = new HashSet<CTGIOHANG>();
            this.DONHANGs = new HashSet<DONHANG>();
            this.FEEDBACKs = new HashSet<FEEDBACK>();
            this.PRODUCTIMAGEs = new HashSet<PRODUCTIMAGE>();
        }
    
        public int MASP { get; set; }
        public string TENSP { get; set; }
        public Nullable<double> GIA { get; set; }
        public Nullable<int> SLTON { get; set; }
        public Nullable<int> SLBAN { get; set; }
        public string MOTA { get; set; }
        public Nullable<int> DISCOUNT { get; set; }
        public Nullable<System.DateTime> THOIGIAN { get; set; }
        public Nullable<double> GIADAGIAM { get; set; }
        public string HINHANH { get; set; }
        public Nullable<int> MAQL { get; set; }
        public Nullable<int> MATH { get; set; }
        public Nullable<int> MAL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAUHINH> CAUHINHs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTGIOHANG> CTGIOHANGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FEEDBACK> FEEDBACKs { get; set; }
        public virtual LOAISP LOAISP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTIMAGE> PRODUCTIMAGEs { get; set; }
        public virtual QUANLY QUANLY { get; set; }
        public virtual THUONGHIEU THUONGHIEU { get; set; }
    }
}
