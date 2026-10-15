using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lab03_QuanLySinhVienOOP
{
    internal class Program
    {
        private static readonly QuanLySinhVien Service = new QuanLySinhVien();

        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            try
            {
                ChayChuongTrinh();
            }
            catch (System.IO.EndOfStreamException)
            {
                // Het du lieu dau vao: thoat nhe nhang.
            }
            Console.WriteLine("Tam biet!");
        }

        private static void ChayChuongTrinh()
        {
            while (true)
            {
                HienMenu();
                string chon = Console.ReadLine()?.Trim();
                if (chon == null || chon == "0") break; // null: hết input (Ctrl+Z / pipe)

                switch (chon)
                {
                    case "1": ThemSinhVien(); break;
                    case "2": XuatDanhSach(Service.LayDanhSach(), "Danh sach rong."); break;
                    case "3": TimTheoMa(); break;
                    case "4": TimTheoTen(); break;
                    case "5": SuaDiem(); break;
                    case "6": XoaSinhVien(); break;
                    case "7": XuatDanhSach(Service.SapXepTheoDiem(), "Danh sach rong."); break;
                    case "8": XuatDanhSach(Service.LocDat(), "Khong co sinh vien dat."); break;
                    default: Console.WriteLine("Lua chon khong hop le."); break;
                }
            }
        }

        private static void HienMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== QUAN LY SINH VIEN =====");
            Console.WriteLine("1. Them sinh vien");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim sinh vien theo ma");
            Console.WriteLine("4. Tim sinh vien theo ten");
            Console.WriteLine("5. Sua diem trung binh");
            Console.WriteLine("6. Xoa sinh vien");
            Console.WriteLine("7. Sap xep theo diem giam dan");
            Console.WriteLine("8. Loc sinh vien dat");
            Console.WriteLine("0. Thoat");
            Console.Write("Chon chuc nang: ");
        }

        // ---------- Chức năng ----------
        private static void ThemSinhVien()
        {
            string ma = NhapChuoi("Ma sinh vien: ");
            if (Service.TonTai(ma))
            {
                Console.WriteLine("Ma sinh vien da ton tai.");
                return;
            }
            string hoTen = NhapChuoi("Ho ten: ");
            DateTime ngaySinh = NhapNgay("Ngay sinh (dd/MM/yyyy): ");
            string maLop = NhapChuoi("Ma lop: ");
            double diem = NhapDiem("Diem trung binh (0-10): ");

            Console.WriteLine(Service.Them(new SinhVien(ma, hoTen, ngaySinh, maLop, diem))
                ? "Them thanh cong."
                : "Ma sinh vien da ton tai.");
        }

        private static void TimTheoMa()
        {
            var sv = Service.TimTheoMa(NhapChuoi("Nhap ma sinh vien: "));
            if (sv == null) Console.WriteLine("Khong tim thay sinh vien.");
            else XuatDanhSach(new List<SinhVien> { sv }, "");
        }

        private static void TimTheoTen()
        {
            XuatDanhSach(Service.TimTheoTen(NhapChuoi("Nhap tu khoa ho ten: ")), "Khong tim thay sinh vien.");
        }

        private static void SuaDiem()
        {
            string ma = NhapChuoi("Nhap ma sinh vien: ");
            if (!Service.TonTai(ma))
            {
                Console.WriteLine("Khong tim thay sinh vien.");
                return;
            }
            double diem = NhapDiem("Diem trung binh moi (0-10): ");
            Console.WriteLine(Service.Sua(ma, diem) ? "Cap nhat thanh cong." : "Khong tim thay sinh vien.");
        }

        private static void XoaSinhVien()
        {
            Console.WriteLine(Service.Xoa(NhapChuoi("Nhap ma sinh vien: "))
                ? "Xoa thanh cong."
                : "Khong tim thay sinh vien.");
        }

        private static void XuatDanhSach(List<SinhVien> ds, string thongBaoRong)
        {
            if (ds.Count == 0)
            {
                Console.WriteLine(thongBaoRong);
                return;
            }
            Console.WriteLine($"{"Ma SV",-10} | {"Ho ten",-25} | {"Ngay sinh",-10} | {"Lop",-8} | {"Diem",5} | Xep loai");
            Console.WriteLine(new string('-', 86));
            foreach (var sv in ds) Console.WriteLine(sv.LayThongTin());
        }

        // ---------- Hàm nhập dữ liệu (không crash khi nhập sai) ----------
        private static string NhapChuoi(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string s = Console.ReadLine();
                if (s == null) throw new System.IO.EndOfStreamException();
                if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
                Console.WriteLine("Khong duoc de trong. Vui long nhap lai.");
            }
        }

        private static DateTime NhapNgay(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string s = Console.ReadLine();
                if (s == null) throw new System.IO.EndOfStreamException();
                if (DateTime.TryParseExact(s.Trim(), new[] { "dd/MM/yyyy", "d/M/yyyy" },
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngay)
                    && ngay <= DateTime.Today)
                    return ngay;
                Console.WriteLine("Ngay sinh khong hop le (dinh dang dd/MM/yyyy). Vui long nhap lai.");
            }
        }

        private static double NhapDiem(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string s = Console.ReadLine();
                if (s == null) throw new System.IO.EndOfStreamException();
                s = s.Trim().Replace(',', '.');
                if (!double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double diem))
                    Console.WriteLine("Diem phai la so. Vui long nhap lai.");
                else if (diem < 0 || diem > 10)
                    Console.WriteLine("Diem khong hop le (phai tu 0 den 10). Vui long nhap lai.");
                else
                    return diem;
            }
        }
    }
}
