using System;

namespace Lab03_QuanLySinhVienOOP
{
    /// <summary>Sinh viên kế thừa từ Nguoi.</summary>
    public class SinhVien : Nguoi
    {
        private string _maSinhVien = string.Empty;
        private double _diemTrungBinh;

        public string MaSinhVien
        {
            get => _maSinhVien;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Mã sinh viên không được để trống.");
                _maSinhVien = value.Trim();
            }
        }

        /// <summary>Chỉ nhận giá trị trong khoảng 0-10.</summary>
        public double DiemTrungBinh
        {
            get => _diemTrungBinh;
            set
            {
                if (double.IsNaN(value) || value < 0 || value > 10)
                    throw new ArgumentOutOfRangeException(nameof(DiemTrungBinh), "Điểm phải nằm trong khoảng 0-10.");
                _diemTrungBinh = value;
            }
        }

        public string MaLop { get; set; }

        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop?.Trim() ?? string.Empty;
            DiemTrungBinh = diemTrungBinh;
        }

        public string XepLoai()
        {
            if (DiemTrungBinh >= 8.5) return "Xuat sac";
            if (DiemTrungBinh >= 8.0) return "Gioi";
            if (DiemTrungBinh >= 6.5) return "Kha";
            if (DiemTrungBinh >= 5.0) return "Trung binh";
            return "Yeu";
        }

        public override string LayThongTin()
        {
            return $"{MaSinhVien,-10} | {HoTen,-25} | {NgaySinh:dd/MM/yyyy} | {MaLop,-8} | {DiemTrungBinh,5:0.0#} | {XepLoai()}";
        }
    }
}
