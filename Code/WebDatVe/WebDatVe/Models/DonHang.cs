//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDatVe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonHang
    {
        public DonHang()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            this.DonHangDichVus = new HashSet<DonHangDichVu>();
        }
    
        public int MaDonHang { get; set; }
        public int MaKhachHang { get; set; }
        public System.DateTime NgayDat { get; set; }
        public int TrangThai { get; set; }
        public int ThanhTien { get; set; }
    
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<DonHangDichVu> DonHangDichVus { get; set; }
    }
}