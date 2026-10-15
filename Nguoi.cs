using System;

namespace Lab03_QuanLySinhVienOOP
{
    /// <summary>Class cha: thông tin chung của một người.</summary>
    public class Nguoi
    {
        private string _hoTen = string.Empty;

        public string HoTen
        {
            get => _hoTen;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Họ tên không được để trống.");
                _hoTen = value.Trim();
            }
        }

        public DateTime NgaySinh { get; set; }

        public Nguoi(string hoTen, DateTime ngaySinh)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
        }

        public virtual string LayThongTin()
        {
            return $"Họ tên: {HoTen}, Ngày sinh: {NgaySinh:dd/MM/yyyy}";
        }
    }
}
