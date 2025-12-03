using GymWpfApp.Infrastructure;
using GymWpfApp.Utils;
using System;
using System.Windows;

namespace GymWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Initialize Dependency Injection Container
                Logger.Write("=== Application Starting ===");
                Logger.Write("Initializing ServiceContainer...");

                ServiceContainer.Instance.InitializeServices();

                Logger.Write("ServiceContainer initialized successfully");
                Logger.Write("All services loaded");
            }
            catch (Exception ex)
            {
                Logger.Write($"CRITICAL ERROR during startup: {ex.Message}");
                MessageBox.Show(
                    $"Lỗi khởi động ứng dụng:\n{ex.Message}",
                    "Lỗi nghiêm trọng",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Logger.Write("=== Application Shutting Down ===");
            base.OnExit(e);
        }
    }
}
