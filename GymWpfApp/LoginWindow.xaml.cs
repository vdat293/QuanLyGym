using GymWpfApp.Constants;
using System;
using System.Windows;

namespace GymWpfApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            txtUsername.Focus();

            // Cho phép kéo thả window
            this.MouseLeftButtonDown += (s, e) => { if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed) this.DragMove(); };
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnForgotPassword_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show(
                AppConstants.Messages.InfoForgotPassword,
                AppConstants.Messages.InfoTitle,
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Password;

                // Validate input
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception(AppConstants.Messages.ErrorMissingInfo);
                }

                // Check credentials using constants
                if (username == AppConstants.Auth.DefaultUsername && password == AppConstants.Auth.DefaultPassword)
                {
                    Logger.Write($"Đăng nhập thành công: {username}");

                    // Mở MainMenu
                    MainMenuWindow mainMenu = new MainMenuWindow();
                    mainMenu.Show();

                    // Đóng LoginWindow
                    this.Close();
                }
                else
                {
                    throw new Exception(AppConstants.Messages.ErrorLoginFailed);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    AppConstants.Messages.ErrorTitle,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                Logger.Write($"Đăng nhập thất bại: {ex.Message}");
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}
