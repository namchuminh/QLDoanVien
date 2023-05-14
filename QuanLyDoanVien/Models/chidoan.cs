using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoanVien.Models
{
    internal class chidoan
    {
        private string _MaChiDoan;
        private string _TenChiDoan;
        private string _NgayThanhLap;
        private string _KhuVuc;
        private int _SoDoanVien;

        public string MaChiDoan { get => _MaChiDoan; set => _MaChiDoan = value; }
        public string TenChiDoan { get => _TenChiDoan; set => _TenChiDoan = value; }
        public string NgayThanhLap { get => _NgayThanhLap; set => _NgayThanhLap = value; }
        public string KhuVuc { get => _KhuVuc; set => _KhuVuc = value; }
        public int SoDoanVien { get => _SoDoanVien; set => _SoDoanVien = value; }
    }
}
