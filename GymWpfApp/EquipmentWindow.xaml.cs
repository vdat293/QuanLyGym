using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GymWpfApp
{
    public partial class EquipmentWindow : Window
    {
        private Equipment editingEquipment = null;

        public EquipmentWindow()
        {
            InitializeComponent();

            EquipmentDataStore.Load();
            gridEquipment.ItemsSource = EquipmentDataStore.EquipmentList;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
                {
                    throw new Exception("Vui lòng nhập mã và tên thiết bị!");
                }

                if (editingEquipment != null)
                {
                    // UPDATE
                    editingEquipment.Code = txtCode.Text.Trim().ToUpper();
                    editingEquipment.Name = txtName.Text.Trim();
                    editingEquipment.Category = (cbCategory.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Khác";
                    editingEquipment.Location = (cbLocation.SelectedItem as ComboBoxItem)?.Content.ToString();
                    editingEquipment.Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Tốt";
                    editingEquipment.Notes = txtNotes.Text.Trim();

                    EquipmentDataStore.Save();
                    Logger.Write($"Cập nhật thiết bị: {editingEquipment.Code} - {editingEquipment.Name}");

                    MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Kiểm tra mã trùng
                    string newCode = txtCode.Text.Trim().ToUpper();
                    if (EquipmentDataStore.EquipmentList.Any(eq => eq.Code == newCode))
                    {
                        throw new Exception($"Mã thiết bị '{newCode}' đã tồn tại!");
                    }

                    // ADD NEW
                    var equipment = new Equipment
                    {
                        Id = EquipmentDataStore.NextId(),
                        Code = newCode,
                        Name = txtName.Text.Trim(),
                        Category = (cbCategory.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Khác",
                        Location = (cbLocation.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Tốt",
                        Notes = txtNotes.Text.Trim()
                    };

                    EquipmentDataStore.EquipmentList.Add(equipment);
                    EquipmentDataStore.Save();
                    Logger.Write($"Thêm thiết bị: {equipment.Code} - {equipment.Name}");

                    MessageBox.Show("Thêm thiết bị thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                ClearInput();
                gridEquipment.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write($"Lỗi xử lý thiết bị: {ex.Message}");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gridEquipment.SelectedItem is Equipment selectedEquipment)
            {
                var result = MessageBox.Show($"Bạn có chắc muốn xóa thiết bị {selectedEquipment.Code} - {selectedEquipment.Name}?",
                    "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    EquipmentDataStore.EquipmentList.Remove(selectedEquipment);
                    EquipmentDataStore.Save();
                    Logger.Write($"Đã xóa thiết bị: {selectedEquipment.Code} - {selectedEquipment.Name}");
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
                var filtered = EquipmentDataStore.EquipmentList.Where(eq => eq.Id == id).ToList();
                gridEquipment.ItemsSource = filtered;
            }
            else
            {
                var filtered = EquipmentDataStore.EquipmentList.Where(eq =>
                    eq.Name.ToLower().Contains(keyword) ||
                    eq.Code.ToLower().Contains(keyword)).ToList();
                gridEquipment.ItemsSource = filtered;
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            gridEquipment.ItemsSource = EquipmentDataStore.EquipmentList;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        private void GridEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridEquipment.SelectedItem is Equipment selected)
            {
                editingEquipment = selected;

                txtCode.Text = selected.Code;
                txtCode.IsEnabled = false; // Không cho sửa mã khi update
                txtName.Text = selected.Name;
                txtNotes.Text = selected.Notes;

                foreach (ComboBoxItem item in cbCategory.Items)
                {
                    if (item.Content.ToString() == selected.Category)
                    {
                        cbCategory.SelectedItem = item;
                        break;
                    }
                }

                foreach (ComboBoxItem item in cbLocation.Items)
                {
                    if (item.Content.ToString() == selected.Location)
                    {
                        cbLocation.SelectedItem = item;
                        break;
                    }
                }

                foreach (ComboBoxItem item in cbStatus.Items)
                {
                    if (item.Content.ToString() == selected.Status)
                    {
                        cbStatus.SelectedItem = item;
                        break;
                    }
                }

                lblFormTitle.Text = "CẬP NHẬT";
                btnSave.Content = "CẬP NHẬT";
            }
        }

        private void ClearInput()
        {
            editingEquipment = null;
            txtCode.Clear();
            txtCode.IsEnabled = true; // Cho phép nhập mã khi thêm mới
            txtName.Clear();
            txtNotes.Clear();
            cbCategory.SelectedIndex = -1;
            cbLocation.SelectedIndex = -1;
            cbStatus.SelectedIndex = 0; // Mặc định "Tốt"
            gridEquipment.SelectedIndex = -1;

            lblFormTitle.Text = "THÊM MỚI";
            btnSave.Content = "THÊM MỚI";
        }
    }
}
