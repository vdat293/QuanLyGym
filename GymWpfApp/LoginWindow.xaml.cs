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
            MessageBox.Show("Liên hệ email 24050129@student.bdu.edu.vn để lấy lại mật khẩu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    throw new Exception("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!");
                }

                // Check credentials
                if (username == "admin" && password == "123456")
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
                    throw new Exception("Tên đăng nhập hoặc mật khẩu không đúng!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi đăng nhập", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write($"Đăng nhập thất bại: {ex.Message}");
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}
