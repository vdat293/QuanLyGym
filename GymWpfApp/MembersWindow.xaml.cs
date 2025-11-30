using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GymWpfApp
{
    public partial class MembersWindow : Window
    {
        private Member editingMember = null;

        public MembersWindow()
        {
            InitializeComponent();

            DataStore.Load();
            gridMembers.ItemsSource = DataStore.Members;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || cbGender.SelectedItem == null || cbPackage.SelectedItem == null || cbTiming.SelectedItem == null)
                {
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                }

                // Validate Name - không được chứa số
                if (txtName.Text.Any(char.IsDigit))
                {
                    throw new Exception("Họ tên không được chứa số!");
                }

                // Validate Phone - phải là 10 số
                if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    if (!IsNumeric(txtPhone.Text) || txtPhone.Text.Length != 10)
                    {
                        throw new Exception("Số điện thoại không hợp lệ!");
                    }
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
                // Removed Amount validation as it is now a ComboBox selection

                if (editingMember != null)
                {
                    // UPDATE
                    editingMember.Name = txtName.Text.Trim();
                    editingMember.Phone = txtPhone.Text.Trim();
                    editingMember.Gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
                    editingMember.Age = int.TryParse(txtAge.Text, out int a) ? a : 0;
                    editingMember.Package = (cbPackage.SelectedItem as ComboBoxItem)?.Content.ToString();
                    editingMember.Timing = (cbTiming.SelectedItem as ComboBoxItem)?.Content.ToString();

                    DataStore.Save();
                    Logger.Write($"Cập nhật hội viên: {editingMember.Name}");

                    MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // ADD NEW
                    var mem = new Member
                    {
                        Id = DataStore.NextId(),
                        Name = txtName.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Age = int.TryParse(txtAge.Text, out int a) ? a : 0,
                        Package = (cbPackage.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Timing = (cbTiming.SelectedItem as ComboBoxItem)?.Content.ToString()
                    };

                    DataStore.Members.Add(mem);
                    DataStore.Save();
                    Logger.Write($"Thêm mới: {mem.Name}");

                    MessageBox.Show("Thêm thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                ClearInput();
                gridMembers.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write($"Lỗi xử lý hội viên: {ex.Message}");
            }
        }

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
            var filtered = DataStore.Members.Where(m => m.Name.ToLower().Contains(keyword)).ToList();
            gridMembers.ItemsSource = filtered;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            gridMembers.ItemsSource = DataStore.Members;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        private void GridMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridMembers.SelectedItem is Member selected)
            {
                editingMember = selected;

                txtName.Text = selected.Name;
                txtPhone.Text = selected.Phone;
                txtAge.Text = selected.Age.ToString();
                
                // Set ComboBox Package
                foreach (ComboBoxItem item in cbPackage.Items)
                {
                    if (item.Content.ToString() == selected.Package)
                    {
                        cbPackage.SelectedItem = item;
                        break;
                    }
                }

                // Set ComboBox Gender
                foreach (ComboBoxItem item in cbGender.Items)
                {
                    if (item.Content.ToString() == selected.Gender)
                    {
                        cbGender.SelectedItem = item;
                        break;
                    }
                }

                // Set ComboBox Timing
                foreach (ComboBoxItem item in cbTiming.Items)
                {
                    if (item.Content.ToString() == selected.Timing)
                    {
                        cbTiming.SelectedItem = item;
                        break;
                    }
                }

                lblFormTitle.Text = "CẬP NHẬT";
                btnSave.Content = "CẬP NHẬT";
            }
        }

        private void ClearInput()
        {
            editingMember = null;
            txtName.Clear();
            txtPhone.Clear();
            txtAge.Clear();
            cbPackage.SelectedIndex = -1;
            cbGender.SelectedIndex = -1;
            cbTiming.SelectedIndex = -1;
            gridMembers.SelectedIndex = -1;

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
