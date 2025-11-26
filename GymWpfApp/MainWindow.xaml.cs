using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GymWpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Nạp dữ liệu và gắn vào Grid
            DataStore.Load();
            gridMembers.ItemsSource = DataStore.Members;

            Logger.Write("WPF App khởi động.");
        }

        // 1. Thêm thành viên
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate các trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtName.Text) || cbGender.SelectedItem == null)
                {
                    throw new Exception("Vui lòng nhập tên và chọn giới tính!");
                }

                // Validate Name - không được là số
                if (IsNumeric(txtName.Text))
                {
                    throw new Exception("Họ tên không được là số!");
                }

                // Validate Phone - không được là chữ
                if (!string.IsNullOrWhiteSpace(txtPhone.Text) && !IsNumeric(txtPhone.Text))
                {
                    throw new Exception("Số điện thoại không được chứa chữ!");
                }

                // Validate Age - không được là chữ
                if (!string.IsNullOrWhiteSpace(txtAge.Text))
                {
                    if (!int.TryParse(txtAge.Text, out int age))
                    {
                        throw new Exception("Tuổi phải là số nguyên!");
                    }
                    if (age < 0 || age > 150)
                    {
                        throw new Exception("Tuổi phải từ 0 đến 150!");
                    }
                }

                // Validate Amount - không được là chữ
                if (!string.IsNullOrWhiteSpace(txtAmount.Text))
                {
                    if (!decimal.TryParse(txtAmount.Text, out decimal amount))
                    {
                        throw new Exception("Phí hàng tháng phải là số!");
                    }
                    if (amount < 0)
                    {
                        throw new Exception("Phí hàng tháng không được âm!");
                    }
                }

                // Tạo đối tượng Member sau khi đã validate
                var mem = new Member
                {
                    Id = DataStore.NextId(),
                    Name = txtName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Age = int.TryParse(txtAge.Text, out int a) ? a : 0,
                    Amount = decimal.TryParse(txtAmount.Text, out decimal m) ? m : 0,
                    Timing = (cbTiming.SelectedItem as ComboBoxItem)?.Content.ToString()
                };

                // Thêm vào List -> WPF tự động cập nhật lên Grid nhờ ObservableCollection
                DataStore.Members.Add(mem);
                DataStore.Save(); // Lưu JSON
                Logger.Write($"Thêm mới: {mem.Name}");

                ClearInput();
                MessageBox.Show("Thêm thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write($"Lỗi thêm thành viên: {ex.Message}");
            }
        }

        // Hàm kiểm tra chuỗi có phải toàn số không
        private bool IsNumeric(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.All(char.IsDigit);
        }

        // 2. Xóa thành viên
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gridMembers.SelectedItem is Member selectedMember)
            {
                var result = MessageBox.Show($"Bạn có chắc muốn xóa {selectedMember.Name}?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    DataStore.Members.Remove(selectedMember);
                    DataStore.Save();
                    Logger.Write($"Đã xóa: {selectedMember.Name}");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }
        }

        // 3. Tìm kiếm
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();
            // Lọc dữ liệu hiển thị (không xóa dữ liệu gốc)
            var filtered = DataStore.Members.Where(m => m.Name.ToLower().Contains(keyword)).ToList();
            gridMembers.ItemsSource = filtered;
        }

        // 4. Làm mới (Hủy tìm kiếm)
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            gridMembers.ItemsSource = DataStore.Members;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        private void ClearInput()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtAge.Clear();
            txtAmount.Clear();
            cbGender.SelectedIndex = -1;
            cbTiming.SelectedIndex = -1;
        }
    }
}