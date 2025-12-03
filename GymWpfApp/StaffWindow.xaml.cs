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
    public partial class StaffWindow : Window
    {
        private Staff editingStaff = null;
        private readonly IDataService<Staff> _staffService;

        public StaffWindow()
        {
            InitializeComponent();

            // Dependency Injection - Resolve service from container
            _staffService = ServiceContainer.Instance.Resolve<IDataService<Staff>>();
            gridStaff.ItemsSource = _staffService.GetAll();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(txtName.Text) || cbGender.SelectedItem == null)
                {
                    throw new Exception(AppConstants.Messages.ErrorMissingInfo);
                }

                // Get values
                string name = txtName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                int age = int.TryParse(txtAge.Text, out int a) ? a : 0;

                // Validate using PersonValidator
                var validationResult = PersonValidator.ValidatePersonFields(name, phone, age);
                if (!validationResult.IsValid)
                {
                    throw new Exception(validationResult.GetErrorMessage());
                }

                if (editingStaff != null)
                {
                    // UPDATE
                    editingStaff.Name = name;
                    editingStaff.Phone = phone;
                    editingStaff.Gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
                    editingStaff.Age = age;
                    editingStaff.Position = (cbPosition.SelectedItem as ComboBoxItem)?.Content.ToString();
                    editingStaff.Shift = (cbShift.SelectedItem as ComboBoxItem)?.Content.ToString();

                    _staffService.Update(editingStaff);
                    Logger.Write($"Cập nhật nhân viên: {editingStaff.Name}");

                    MessageBox.Show(
                        AppConstants.Messages.SuccessUpdated,
                        AppConstants.Messages.SuccessTitle,
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }
                else
                {
                    // ADD NEW
                    var staff = new Staff
                    {
                        Name = name,
                        Phone = phone,
                        Gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Age = age,
                        Position = (cbPosition.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Shift = (cbShift.SelectedItem as ComboBoxItem)?.Content.ToString()
                    };

                    _staffService.Add(staff);
                    Logger.Write($"Thêm nhân viên: {staff.Name}");

                    MessageBox.Show(
                        AppConstants.Messages.SuccessAdded,
                        AppConstants.Messages.SuccessTitle,
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }

                ClearInput();
                gridStaff.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    AppConstants.Messages.ErrorTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                Logger.Write($"Lỗi xử lý nhân viên: {ex.Message}");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gridStaff.SelectedItem is Staff selectedStaff)
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa {selectedStaff.Name}?",
                    AppConstants.Messages.ConfirmTitle,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    _staffService.Remove(selectedStaff);
                    Logger.Write($"Đã xóa nhân viên: {selectedStaff.Name}");
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
            if (int.TryParse(keyword, out int id))
            {
                var filtered = _staffService.GetAll().Where(s => s.Id == id).ToList();
                gridStaff.ItemsSource = filtered;
            }
            else
            {
                var filtered = _staffService.GetAll().Where(s => s.Name.ToLower().Contains(keyword)).ToList();
                gridStaff.ItemsSource = filtered;
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            gridStaff.ItemsSource = _staffService.GetAll();
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

                // Set ComboBox Gender
                foreach (ComboBoxItem item in cbGender.Items)
                {
                    if (item.Content.ToString() == selected.Gender)
                    {
                        cbGender.SelectedItem = item;
                        break;
                    }
                }

                // Set ComboBox Position
                foreach (ComboBoxItem item in cbPosition.Items)
                {
                    if (item.Content.ToString() == selected.Position)
                    {
                        cbPosition.SelectedItem = item;
                        break;
                    }
                }

                // Set ComboBox Shift
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
    }
}
