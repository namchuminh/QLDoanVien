using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanVien.Models
{
    internal class xeploai
    {
        private string _MaDoanVien;
        private string _TenDoanVien;
        private string _NgaySinh;
        private string _ChiDoan;
        private string _XepLoai;
        private string _ThoiGian;
        private string _NhanXet;
        public string MaDoanVien { get => _MaDoanVien; set => _MaDoanVien = value; }
        public string TenDoanVien { get => _TenDoanVien; set => _TenDoanVien = value; }
        public string NgaySinh { get => _NgaySinh; set => _NgaySinh = value; }
        public string ChiDoan { get => _ChiDoan; set => _ChiDoan = value; }
        public string XepLoai { get => _XepLoai; set => _XepLoai = value; }
        public string ThoiGian { get => _ThoiGian; set => _ThoiGian = value; }
        public string NhanXet { get => _NhanXet; set => _NhanXet = value; }
    }
}
