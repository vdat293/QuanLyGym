using QuanLyPhongGym.ViewModels;
using System.Windows;

namespace QuanLyPhongGym.Views
{
    public partial class AddMemberDialog : Window
    {
        private AddMemberViewModel ViewModel => (AddMemberViewModel)DataContext;

        public AddMemberDialog(AddMemberViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Execute the save command manually
            if (ViewModel.SaveCmd.CanExecute(null))
            {
                ViewModel.SaveCmd.Execute(null);

                // Wait a moment for async operation to complete
                await System.Threading.Tasks.Task.Delay(100);

                if (ViewModel.DialogResult)
                {
                    DialogResult = true;
                    Close();
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
