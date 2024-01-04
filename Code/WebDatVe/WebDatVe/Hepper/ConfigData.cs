using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDatVe.Hepper
{
    public static class ConfigData
    {
        public static int OGheMax = 12;
        public static int VeGheThuong = 70000;
        public static int VeGheVip = 90000;
        public static int VeGheDoi = 200000;


        public static Dictionary<int, string> DSLoaiChieuPhim = new Dictionary<int, string>
        {
            {(int)LoaiChieuPhim.Loai2D, "2D"},
            {(int)LoaiChieuPhim.Loai3D, "3D" }
        };

        public static Dictionary<int, string> DSTrangThaiPhim = new Dictionary<int, string>
        {
            {(int)TrangThaiPhim.SapChieu, "Sắp chiếu" },
            {(int)TrangThaiPhim.DangChieu, "Đang chiếu" },
            {(int)TrangThaiPhim.NgungChieu, "Ngừng chiếu" }
        };

        public static Dictionary<int, string> DSTrangThaiDichVu = new Dictionary<int, string>
        {
            {(int)TrangThaiDichVu.DangBan, "Đang bán" },
            {(int)TrangThaiDichVu.NgungBan, "Ngừng bán" }
        };

        public static Dictionary<int, string> DSTrangThaiDonHang = new Dictionary<int, string>
        {
            {(int)TrangThaiDonHang.Moi, "Mới" },
            {(int)TrangThaiDonHang.DaNhanVe, "Đã nhận vé" },
            {(int)TrangThaiDonHang.Huy, "Hủy" }
        };

        public static Dictionary<int, string> DSTrangThaiThanhToan = new Dictionary<int, string>
        {
            {(int)TrangThaiThanhToan.ChuaThanhToan, "Chưa thanh toán" },
            {(int)TrangThaiThanhToan.DaThanhToan, "Đã thanh toán" }
        };

        public static Dictionary<string, string> DSHangGhe = new Dictionary<string, string>
        {
            {"G", "G" },
            {"F", "F" },
            {"E", "E" },
            {"D", "D" },
            {"C", "C" },
            {"B", "B" },
            {"A", "A" },
        };

        public static Dictionary<int, string> DSSoO = new Dictionary<int, string>
        {
            {1, "1" },
            {2, "2" },
            {3, "3" },
            {4, "4" },
            {5, "5" },
            {6, "6" },
            {7, "7" },
            {8, "8" },
            {9, "9" },
            {10, "10" },
            {11, "11" },
            {12, "12" }
        };

        public static Dictionary<int, string> DSLoaiGhe = new Dictionary<int, string>
        {
            {(int)LoaiGhe.GheThuong, "Ghế thường" },
            {(int)LoaiGhe.GheVip, "Ghế VIP" },
            {(int)LoaiGhe.GheDoi, "Ghế đôi" }
        };

        public static Dictionary<int, string> DSGio = new Dictionary<int, string>
        {
            {8, "08" },
            {9, "09" },
            {10, "10" },
            {11, "11" },
            {12, "12" },
            {13, "13" },
            {14, "14" },
            {15, "15" },
            {16, "16" },
            {17, "17" },
            {18, "18" },
            {19, "19" },
            {20, "20" },
            {21, "21" },
            {22, "22" },
            {23, "23" },
        };

        public static Dictionary<int, string> DSPhut = new Dictionary<int, string>
        {
            {0, "00" },
            {5, "05" },
            {10, "10" },
            {15, "15" },
            {20, "20" },
            {25, "25" },
            {30, "30" },
            {35, "35" },
            {40, "40" },
            {45, "45" },
            {50, "50" },
            {55, "55" }
        };
    }
}