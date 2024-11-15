using System;
using System.Collections.Generic;
using System.Data;
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
            // Kiểm tra các điều kiện hợp lệ
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

            if (!dpBirthday.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int age = DateTime.Now.Year - dpBirthday.SelectedDate.Value.Year;
            if (age < 18)
            {
                MessageBox.Show("Tuổi của nhân viên phải từ 18 trở lên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (radMale.IsChecked == false && radFemale.IsChecked == false)
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (cbPhongBan.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHeSoLuong.Text) || !decimal.TryParse(txtHeSoLuong.Text, out decimal heSoLuong) || heSoLuong <= 0)
            {
                MessageBox.Show("Hệ số lương phải là một số thực lớn hơn 0.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Nếu tất cả dữ liệu hợp lệ
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
            dgvInfo.Items.Refresh();
        }

    }
}

