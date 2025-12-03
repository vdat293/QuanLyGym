using GymWpfApp.Constants;
using GymWpfApp.Infrastructure;
using GymWpfApp.Interfaces;
using GymWpfApp.Models;
using GymWpfApp.Validators;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GymWpfApp
{
    public partial class EquipmentWindow : Window
    {
        private Equipment editingEquipment = null;
        private readonly IDataService<Equipment> _equipmentService;

        public EquipmentWindow()
        {
            InitializeComponent();

            // Dependency Injection - Resolve service from container
            _equipmentService = ServiceContainer.Instance.Resolve<IDataService<Equipment>>();
            gridEquipment.ItemsSource = _equipmentService.GetAll();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get values
                string code = txtCode.Text.Trim().ToUpper();
                string name = txtName.Text.Trim();
                string category = (cbCategory.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Khác";

                // Validate using EquipmentValidator
                var validationResult = EquipmentValidator.ValidateEquipment(code, name, category);
                if (!validationResult.IsValid)
                {
                    throw new Exception(validationResult.GetErrorMessage());
                }

                if (editingEquipment != null)
                {
                    // UPDATE
                    editingEquipment.Code = code;
                    editingEquipment.Name = name;
                    editingEquipment.Category = category;
                    editingEquipment.Location = (cbLocation.SelectedItem as ComboBoxItem)?.Content.ToString();
                    editingEquipment.Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Tốt";
                    editingEquipment.Notes = txtNotes.Text.Trim();

                    _equipmentService.Update(editingEquipment);
                    Logger.Write($"Cập nhật thiết bị: {editingEquipment.Code} - {editingEquipment.Name}");

                    MessageBox.Show(
                        AppConstants.Messages.SuccessUpdated,
                        AppConstants.Messages.SuccessTitle,
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }
                else
                {
                    // Kiểm tra mã trùng
                    if (_equipmentService.GetAll().Any(eq => eq.Code == code))
                    {
                        throw new Exception($"Mã thiết bị '{code}' đã tồn tại!");
                    }

                    // ADD NEW
                    var equipment = new Equipment
                    {
                        Code = code,
                        Name = name,
                        Category = category,
                        Location = (cbLocation.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Tốt",
                        Notes = txtNotes.Text.Trim()
                    };

                    _equipmentService.Add(equipment);
                    Logger.Write($"Thêm thiết bị: {equipment.Code} - {equipment.Name}");

                    MessageBox.Show(
                        AppConstants.Messages.SuccessAdded,
                        AppConstants.Messages.SuccessTitle,
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }

                ClearInput();
                gridEquipment.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    AppConstants.Messages.ErrorTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                Logger.Write($"Lỗi xử lý thiết bị: {ex.Message}");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gridEquipment.SelectedItem is Equipment selectedEquipment)
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa thiết bị {selectedEquipment.Code} - {selectedEquipment.Name}?",
                    AppConstants.Messages.ConfirmTitle,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    _equipmentService.Remove(selectedEquipment);
                    Logger.Write($"Đã xóa thiết bị: {selectedEquipment.Code} - {selectedEquipment.Name}");
                    ClearInput();

                    MessageBox.Show(
                        AppConstants.Messages.SuccessDeleted,
                        AppConstants.Messages.SuccessTitle,
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }
            }
            else
            {
                MessageBox.Show(
                    AppConstants.Messages.InfoNoSelection,
                    AppConstants.Messages.InfoTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();
            var filtered = _equipmentService.GetAll().Where(eq =>
                eq.Code.ToLower().Contains(keyword) ||
                eq.Name.ToLower().Contains(keyword)
            ).ToList();
            gridEquipment.ItemsSource = filtered;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            gridEquipment.ItemsSource = _equipmentService.GetAll();
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
                txtName.Text = selected.Name;
                txtNotes.Text = selected.Notes;

                // Set ComboBox Category
                foreach (ComboBoxItem item in cbCategory.Items)
                {
                    if (item.Content.ToString() == selected.Category)
                    {
                        cbCategory.SelectedItem = item;
                        break;
                    }
                }

                // Set ComboBox Location
                foreach (ComboBoxItem item in cbLocation.Items)
                {
                    if (item.Content.ToString() == selected.Location)
                    {
                        cbLocation.SelectedItem = item;
                        break;
                    }
                }

                // Set ComboBox Status
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
            txtName.Clear();
            txtNotes.Clear();
            cbCategory.SelectedIndex = -1;
            cbLocation.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            gridEquipment.SelectedIndex = -1;

            lblFormTitle.Text = "THÊM MỚI";
            btnSave.Content = "THÊM MỚI";
        }
    }
}
