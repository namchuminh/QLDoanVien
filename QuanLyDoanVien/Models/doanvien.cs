using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanVien.Models
{
    internal class doanvien
    {
        private string _MaDoanVien;
        private string _TenDoanVien;
        private string _NgaySinh;
        private string _NgayVaoDoan;
        private string _ChucVu;
        private string _ChiDoan;
        public string MaDoanVien { get => _MaDoanVien; set => _MaDoanVien = value; }
        public string TenDoanVien { get => _TenDoanVien; set => _TenDoanVien = value; }
        public string NgaySinh { get => _NgaySinh; set => _NgaySinh = value; }
        public string NgayVaoDoan { get => _NgayVaoDoan; set => _NgayVaoDoan = value; }
        public string ChucVu { get => _ChucVu; set => _ChucVu = value; }
        public string ChiDoan { get => _ChiDoan; set => _ChiDoan = value; }
    }
}
