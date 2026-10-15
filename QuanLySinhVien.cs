using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03_QuanLySinhVienOOP
{
    /// <summary>Quản lý List&lt;SinhVien&gt;; Program không thao tác trực tiếp lên danh sách.</summary>
    public class QuanLySinhVien
    {
        private readonly List<SinhVien> _danhSach = new List<SinhVien>();

        public bool TonTai(string ma) => TimTheoMa(ma) != null;

        /// <summary>Thêm sinh viên. Trả về false nếu mã đã tồn tại.</summary>
        public bool Them(SinhVien sv)
        {
            if (sv == null || TonTai(sv.MaSinhVien)) return false;
            _danhSach.Add(sv);
            return true;
        }

        /// <summary>Sửa điểm trung bình. Trả về false nếu không tìm thấy mã.</summary>
        public bool Sua(string ma, double diemMoi)
        {
            var sv = TimTheoMa(ma);
            if (sv == null) return false;
            sv.DiemTrungBinh = diemMoi;
            return true;
        }

        public bool Xoa(string ma)
        {
            var sv = TimTheoMa(ma);
            return sv != null && _danhSach.Remove(sv);
        }

        public SinhVien TimTheoMa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma)) return null;
            return _danhSach.FirstOrDefault(sv =>
                string.Equals(sv.MaSinhVien, ma.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa)) return new List<SinhVien>();
            return _danhSach
                .Where(sv => sv.HoTen.Contains(tuKhoa.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<SinhVien> SapXepTheoDiem() =>
            _danhSach.OrderByDescending(sv => sv.DiemTrungBinh).ThenBy(sv => sv.HoTen).ToList();

        public List<SinhVien> LocDat() =>
            _danhSach.Where(sv => sv.DiemTrungBinh >= 5).ToList();

        public List<SinhVien> LayDanhSach() => new List<SinhVien>(_danhSach);
    }
}
