using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDatVe.Hepper
{
    public enum LoaiChieuPhim
    {
        Loai2D = 1,
        Loai3D = 2
    }

    public enum TrangThaiPhim
    {
        SapChieu = 1,
        DangChieu = 2,
        NgungChieu = 3
    }

    public enum TrangThaiDichVu
    {
        DangBan = 1,
        NgungBan = 2
    }

    public enum TrangThaiDonHang
    {
        Moi = 1,
        DaNhanVe = 2,
        Huy = 3
    }

    public enum TrangThaiThanhToan
    {
        ChuaThanhToan = 1,
        DaThanhToan = 2
    }

    public enum LoaiGhe
    {
        GheThuong = 1,
        GheVip = 2,
        GheDoi = 3
    }
}