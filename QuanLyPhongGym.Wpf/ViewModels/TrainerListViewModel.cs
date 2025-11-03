using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuanLyPhongGym.Models;
using QuanLyPhongGym.Services;


namespace QuanLyPhongGym.ViewModels
{
    public class TrainerListViewModel : ViewModelBase
    {
        private readonly GymService _svc;
        public ObservableCollection<Trainer> Items { get; } = new();

        private Trainer? _selected;
        public Trainer? Selected { get => _selected; set => Set(ref _selected, value); }

        // Ô tìm kiếm
        private string? _searchIdText;
        public string? SearchIdText { get => _searchIdText; set => Set(ref _searchIdText, value); }

        private string? _searchNameText;
        public string? SearchNameText { get => _searchNameText; set => Set(ref _searchNameText, value); }

        public RelayCommand LoadCmd { get; }
        public RelayCommand AddCmd { get; }
        public RelayCommand SaveCmd { get; }      // để dành
        public RelayCommand DeleteCmd { get; }    // để dành
        public RelayCommand ExportCmd { get; }
        public RelayCommand ResetCmd { get; }

        public TrainerListViewModel(GymService svc)
        {
            _svc = svc;
            LoadCmd = new RelayCommand(async _ => await LoadAsync());
            AddCmd = new RelayCommand(async _ => await AddAsync()); // ✅
            SaveCmd = new RelayCommand(async _ => await SaveAsync(), _ => Selected != null);
            DeleteCmd = new RelayCommand(async _ => await DeleteAsync(), _ => Selected != null);
            ExportCmd = new RelayCommand(async _ => await ExportCsvAsync());
            ResetCmd = new RelayCommand(async _ => await ResetAsync());

            _ = LoadAsync(); // load ban đầu
        }

        private async Task AddAsync()
        {
            var t = new Trainer { FullName = "HLV mới", Phone = "" };
            await _svc.Trainers.AddAsync(t); // ✅ ghi DB ngay
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            Items.Clear();

            // parse ID
            int? id = null;
            if (int.TryParse(SearchIdText, out var parsed)) id = parsed;
            var key = string.IsNullOrWhiteSpace(SearchNameText) ? null : SearchNameText;

            // lấy danh sách + include lịch tập + member để đếm chính xác
            var list = await _svc.SearchTrainersAsync(id, key);
            foreach (var t in list) Items.Add(t);
        }

        private async Task ResetAsync()
        {
            SearchIdText = null;
            SearchNameText = null;
            await LoadAsync();
        }

        private async Task SaveAsync()
        {
            if (Selected == null) return;
            if (Selected.Id == 0) await _svc.Trainers.AddAsync(Selected);
            else await _svc.Trainers.UpdateAsync(Selected);
            await LoadAsync();
        }

        private async Task DeleteAsync()
        {
            if (Selected == null) return;
            await _svc.Trainers.DeleteAsync(Selected.Id);
            await LoadAsync();
        }

        private async Task ExportCsvAsync()
        {
            try
            {
                var desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                var file = Path.Combine(desktop, $"Trainers_{System.DateTime.Now:yyyyMMdd_HHmmss}.csv");

                var sb = new StringBuilder();
                sb.AppendLine("ID,Họ tên,Điện thoại,Số lượng hội viên");
                foreach (var t in Items)
                {
                    string Esc(string s) => s.Contains(",") ? $"\"{s}\"" : s;
                    sb.AppendLine($"{t.Id},{Esc(t.FullName)},{Esc(t.Phone ?? "")},{t.TraineeCount}");
                }
                await File.WriteAllTextAsync(file, sb.ToString(), Encoding.UTF8);
                MessageBox.Show($"Đã xuất: {file}", "Xuất CSV", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Xuất CSV thất bại:\n" + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
