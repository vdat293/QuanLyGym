using System;
using System.Windows;
using System.Windows.Controls;

namespace GymWpfApp
{
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
        }

        private void BtnMembers_Click(object sender, RoutedEventArgs e)
        {
            // Ẩn MainMenu
            this.Hide();

            MembersWindow membersWin = new MembersWindow();
            membersWin.Owner = this; // Set owner để window con luôn nằm trên parent
            membersWin.ShowDialog();

            // Hiện lại MainMenu khi đóng MembersWindow
            this.Show();
        }

        private void BtnStaff_Click(object sender, RoutedEventArgs e)
        {
            // Ẩn MainMenu
            this.Hide();

            StaffWindow staffWin = new StaffWindow();
            staffWin.Owner = this;
            staffWin.ShowDialog();

            // Hiện lại MainMenu khi đóng StaffWindow
            this.Show();
        }

        private void BtnEquipment_Click(object sender, RoutedEventArgs e)
        {
            // Ẩn MainMenu
            this.Hide();

            EquipmentWindow equipmentWin = new EquipmentWindow();
            equipmentWin.Owner = this;
            equipmentWin.ShowDialog();

            // Hiện lại MainMenu khi đóng EquipmentWindow
            this.Show();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Logger.Write("Đăng xuất hệ thống");

                // Mở lại LoginWindow
                LoginWindow loginWin = new LoginWindow();
                loginWin.Show();

                // Đóng MainMenu
                this.Close();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Nếu đóng MainMenu mà không logout, thoát app
            if (Application.Current.Windows.Count == 1)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
