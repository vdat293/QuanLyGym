using QuanLyPhongGym.Models;
using QuanLyPhongGym.Services;
using QuanLyPhongGym.Views;
// using QuanLyPhongGym.Utils;   // ❌ bỏ
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyPhongGym.ViewModels
{
    public class MemberListViewModel : ViewModelBase
    {
        private readonly GymService _svc;
        public ObservableCollection<Member> Items { get; } = new();
        public ObservableCollection<MembershipPlan> Plans { get; } = new();

        private Member? _selected;
        public Member? Selected
        {
            get => _selected;
            set
            {
                Set(ref _selected, value);            // ✅ Set không trả về bool
                DeleteCmd?.RaiseCanExecuteChanged();  // ✅ enable/disable nút
                SaveCmd?.RaiseCanExecuteChanged();
            }
        }

        private string? _searchIdText;
        public string? SearchIdText { get => _searchIdText; set => Set(ref _searchIdText, value); }

        private string? _searchNameText;
        public string? SearchNameText { get => _searchNameText; set => Set(ref _searchNameText, value); }

        public RelayCommand LoadCmd { get; }
        public RelayCommand AddCmd { get; }
        public RelayCommand SaveCmd { get; }
        public RelayCommand DeleteCmd { get; }
        public RelayCommand ExportCmd { get; }
        public RelayCommand ResetCmd { get; }

        public MemberListViewModel(GymService svc)
        {
            _svc = svc;
            LoadCmd = new RelayCommand(async _ => await LoadAsync());
            AddCmd = new RelayCommand(async _ => await AddAsync());
            SaveCmd = new RelayCommand(async _ => await SaveAsync(), _ => Selected != null);
            DeleteCmd = new RelayCommand(async _ => await DeleteAsync(), _ => Selected != null);
            ExportCmd = new RelayCommand(async _ => await ExportCsvAsync());
            ResetCmd = new RelayCommand(async _ => await ResetAsync());
            _ = LoadAsync();
        }

        private async Task AddAsync()
        {
            var viewModel = new AddMemberViewModel(_svc);
            var dialog = new AddMemberDialog(viewModel);
            var result = dialog.ShowDialog();

            if (result == true)
            {
                // Reload the list after successful add
                await LoadAsync();
            }
        }

        private async Task ResetAsync()
        {
            SearchIdText = null;
            SearchNameText = null;
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            Items.Clear();
            Plans.Clear();
            foreach (var p in await _svc.Plans.GetAllAsync()) Plans.Add(p);

            int? id = null;
            if (int.TryParse(SearchIdText, out var parsed)) id = parsed;

            var list = await _svc.SearchMembersAsync(id, SearchNameText);
            foreach (var m in list) Items.Add(m);
        }

        private async Task SaveAsync()
        {
            if (Selected == null) return;
            if (Selected.Id == 0) await _svc.Members.AddAsync(Selected);
            else await _svc.Members.UpdateAsync(Selected);
            await LoadAsync();
        }

        private async Task DeleteAsync()
        {
            if (Selected == null) return;

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa hội viên '{Selected.FullNameDisplay}' không?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes) return;

            // Không gọi _svc.Workouts... nếu Service không có → rely vào Cascade
            await _svc.Members.DeleteAsync(Selected.Id);
            await LoadAsync();
        }

        private async Task ExportCsvAsync()
        {
            try
            {
                var desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var file = Path.Combine(desktop, $"GymMembers_{DateTime.Now:yyyyMMdd_HHmmss}.csv");

                var sb = new StringBuilder();
                sb.AppendLine("ID,Họ,Tên đệm,Tên,Điện thoại,Gói tập,Ngày bắt đầu,Ngày kết thúc,Tổng ngày tập,Trạng thái PT");
                foreach (var m in Items)
                {
                    var planName = m.MembershipPlan?.Name ?? "";
                    string Esc(string s) => s.Contains(",") ? $"\"{s}\"" : s;
                    sb.AppendLine(string.Join(",",
                        m.Id,
                        Esc(m.FamilyName),
                        Esc(m.MiddleName ?? ""),
                        Esc(m.GivenName),
                        Esc(m.Phone ?? ""),
                        Esc(planName),
                        m.JoinDate.ToString("dd/MM/yyyy"),
                        m.EndDate.ToString("dd/MM/yyyy"),
                        m.TotalTrainingDays,
                        m.PtStatus.ToString()
                    ));
                }
                await File.WriteAllTextAsync(file, sb.ToString(), Encoding.UTF8);
                MessageBox.Show($"Đã xuất: {file}", "Xuất CSV", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xuất CSV thất bại:\n" + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
