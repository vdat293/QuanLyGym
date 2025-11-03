using QuanLyPhongGym.ViewModels;
using System.Windows;

namespace QuanLyPhongGym.Views
{
    public partial class MemberFormDialog : Window
    {
        private readonly AddMemberViewModel _viewModel;

        public MemberFormDialog(AddMemberViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.SaveAsync();

            if (_viewModel.DialogResult)
            {
                DialogResult = true;
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
