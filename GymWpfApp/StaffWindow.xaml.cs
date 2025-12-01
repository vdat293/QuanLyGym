using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GymWpfApp
{
    public partial class StaffWindow : Window
    {
        private Staff editingStaff = null;

        public StaffWindow()
        {
            InitializeComponent();

            StaffDataStore.Load();
            gridStaff.ItemsSource = StaffDataStore.StaffList;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || cbGender.SelectedItem == null)
                {
                    throw new Exception("Vui lòng nhập tên và chọn giới tính!");
                }

                if (txtName.Text.Any(char.IsDigit))
                {
                    throw new Exception("Tên không được chứa số!");
                }

                if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    if (!IsNumeric(txtPhone.Text) || txtPhone.Text.Length != 10)
                    {
                        throw new Exception("Số điện thoại không hợp lệ!");
                    }
                }

                if (!string.IsNullOrWhiteSpace(txtAge.Text))
                {
                    if (!int.TryParse(txtAge.Text, out int age))
                    {
                        throw new Exception("Tuổi phải là số nguyên!");
                    }
                    if (age < 18 || age > 70)
                    {
                        throw new Exception("Tuổi nhân viên phải từ 18 đến 70!");
                    }
                }

                if (editingStaff != null)
                {
                    // UPDATE
                    editingStaff.Name = txtName.Text.Trim();
                    editingStaff.Phone = txtPhone.Text.Trim();
                    editingStaff.Gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
                    editingStaff.Age = int.TryParse(txtAge.Text, out int a) ? a : 0;
                    editingStaff.Position = (cbPosition.SelectedItem as ComboBoxItem)?.Content.ToString();
                    editingStaff.Shift = (cbShift.SelectedItem as ComboBoxItem)?.Content.ToString();

                    StaffDataStore.Save();
                    Logger.Write($"Cập nhật nhân viên: {editingStaff.Name}");

                    MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // ADD NEW
                    var staff = new Staff
                    {
                        Id = StaffDataStore.NextId(),
                        Name = txtName.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Age = int.TryParse(txtAge.Text, out int a) ? a : 0,
                        Position = (cbPosition.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Shift = (cbShift.SelectedItem as ComboBoxItem)?.Content.ToString()
                    };

                    StaffDataStore.StaffList.Add(staff);
                    StaffDataStore.Save();
                    Logger.Write($"Thêm nhân viên: {staff.Name}");

                    MessageBox.Show("Thêm nhân viên thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                ClearInput();
                gridStaff.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write($"Lỗi xử lý nhân viên: {ex.Message}");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gridStaff.SelectedItem is Staff selectedStaff)
            {
                var result = MessageBox.Show($"Bạn có chắc muốn xóa nhân viên {selectedStaff.Name}?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    StaffDataStore.StaffList.Remove(selectedStaff);
                    StaffDataStore.Save();
                    Logger.Write($"Đã xóa nhân viên: {selectedStaff.Name}");
                    ClearInput();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();
            if (int.TryParse(keyword, out int id))
            {
                var filtered = StaffDataStore.StaffList.Where(s => s.Id == id).ToList();
                gridStaff.ItemsSource = filtered;
            }
            else
            {
                var filtered = StaffDataStore.StaffList.Where(s => s.Name.ToLower().Contains(keyword)).ToList();
                gridStaff.ItemsSource = filtered;
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            gridStaff.ItemsSource = StaffDataStore.StaffList;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        private void GridStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridStaff.SelectedItem is Staff selected)
            {
                editingStaff = selected;

                txtName.Text = selected.Name;
                txtPhone.Text = selected.Phone;
                txtAge.Text = selected.Age.ToString();

                foreach (ComboBoxItem item in cbGender.Items)
                {
                    if (item.Content.ToString() == selected.Gender)
                    {
                        cbGender.SelectedItem = item;
                        break;
                    }
                }

                foreach (ComboBoxItem item in cbPosition.Items)
                {
                    if (item.Content.ToString() == selected.Position)
                    {
                        cbPosition.SelectedItem = item;
                        break;
                    }
                }

                foreach (ComboBoxItem item in cbShift.Items)
                {
                    if (item.Content.ToString() == selected.Shift)
                    {
                        cbShift.SelectedItem = item;
                        break;
                    }
                }

                lblFormTitle.Text = "CẬP NHẬT";
                btnSave.Content = "CẬP NHẬT";
            }
        }

        private void ClearInput()
        {
            editingStaff = null;
            txtName.Clear();
            txtPhone.Clear();
            txtAge.Clear();
            cbGender.SelectedIndex = -1;
            cbPosition.SelectedIndex = -1;
            cbShift.SelectedIndex = -1;
            gridStaff.SelectedIndex = -1;

            lblFormTitle.Text = "THÊM MỚI";
            btnSave.Content = "THÊM MỚI";
        }

        private bool IsNumeric(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return input.All(char.IsDigit);
        }
    }
}
