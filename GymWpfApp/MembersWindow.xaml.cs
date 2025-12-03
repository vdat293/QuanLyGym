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
    public partial class MembersWindow : Window
    {
        private Member editingMember = null;
        private readonly IDataService<Member> _memberService;

        public MembersWindow()
        {
            InitializeComponent();

            // Dependency Injection - Resolve service from container
            _memberService = ServiceContainer.Instance.Resolve<IDataService<Member>>();
            gridMembers.ItemsSource = _memberService.GetAll();
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
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    cbGender.SelectedItem == null ||
                    cbPackage.SelectedItem == null ||
                    cbTiming.SelectedItem == null)
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

                if (editingMember != null)
                {
                    // UPDATE
                    editingMember.Name = name;
                    editingMember.Phone = phone;
                    editingMember.Gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
                    editingMember.Age = age;
                    editingMember.Package = (cbPackage.SelectedItem as ComboBoxItem)?.Content.ToString();
                    editingMember.Timing = (cbTiming.SelectedItem as ComboBoxItem)?.Content.ToString();

                    _memberService.Update(editingMember);
                    Logger.Write($"Cập nhật hội viên: {editingMember.Name}");

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
                    var mem = new Member
                    {
                        Name = name,
                        Phone = phone,
                        Gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Age = age,
                        Package = (cbPackage.SelectedItem as ComboBoxItem)?.Content.ToString(),
                        Timing = (cbTiming.SelectedItem as ComboBoxItem)?.Content.ToString()
                    };

                    _memberService.Add(mem);
                    Logger.Write($"Thêm mới: {mem.Name}");

                    MessageBox.Show(
                        AppConstants.Messages.SuccessAdded,
                        AppConstants.Messages.SuccessTitle,
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }

                ClearInput();
                gridMembers.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    AppConstants.Messages.ErrorTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                Logger.Write($"Lỗi xử lý hội viên: {ex.Message}");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gridMembers.SelectedItem is Member selectedMember)
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa {selectedMember.Name}?",
                    AppConstants.Messages.ConfirmTitle,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    _memberService.Remove(selectedMember);
                    Logger.Write($"Đã xóa: {selectedMember.Name}");
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
                var filtered = _memberService.GetAll().Where(m => m.Id == id).ToList();
                gridMembers.ItemsSource = filtered;
            }
            else
            {
                var filtered = _memberService.GetAll().Where(m => m.Name.ToLower().Contains(keyword)).ToList();
                gridMembers.ItemsSource = filtered;
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            gridMembers.ItemsSource = _memberService.GetAll();
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
    }
}
