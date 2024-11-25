using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace ThuongXuyen2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<NhanVien> danhSachNhanVien = new List<NhanVien>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNhap_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int age = DateTime.Now.Year - dpBirthday.SelectedDate.Value.Year;
            if (age < 18)
            {
                MessageBox.Show("Tuổi của nhân viên phải từ 18 trở lên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHeSoLuong.Text) || !double.TryParse(txtHeSoLuong.Text, out double heSoLuong) || heSoLuong <= 0)
            {
                MessageBox.Show("Hệ số lương phải là một số thực lớn hơn 0.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Dữ liệu hợp lệ. Thông tin đã được nhập thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            NhanVien nv = new NhanVien
            {
                MaNhanVien = txtMaNhanVien.Text,
                HoTen = txtHoTen.Text,
                NgaySinh = dpBirthday.SelectedDate.Value,
                GioiTinh = radMale.IsChecked == true ? "Nam" : "Nữ",
                PhongBan = cbPhongBan.Text,
                HeSoLuong = heSoLuong,
                Tuoi = age
            };
            danhSachNhanVien.Add(nv);
            dgvInfo.ItemsSource = null;
            dgvInfo.ItemsSource = danhSachNhanVien;
            //dgvInfo.Items.Refresh();
        }

        private void btnOpenWindow_Click(object sender, RoutedEventArgs e)
        {
            if(danhSachNhanVien.Count <= 0)
            {
                MessageBox.Show("Chưa có nhân viên nào!");
            }
            else
            {
                int maxAge = danhSachNhanVien.Max(nv => nv.Tuoi);
                var nhanVienMaxTuoi = danhSachNhanVien.Where(nv => nv.Tuoi == maxAge).ToList();

                Window1 window = new Window1();
                window.dgvInfo.ItemsSource = nhanVienMaxTuoi;
                window.Show();
            }
            
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            dpBirthday.SelectedDate = DateTime.Now;
        }
    }
}

